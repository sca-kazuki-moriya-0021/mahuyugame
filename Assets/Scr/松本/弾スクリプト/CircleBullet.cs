using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : MonoBehaviour
{
    [SerializeField] float speed; // 弾の速度
    [SerializeField] float rotationSpeed; // 回転速度
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int numberOfBullets;
    [SerializeField] float spreadAngle;
    private Player player;
    private Vector2 direction;
    private float timer;
    private float destroyTimer;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        destroyTimer += Time.deltaTime;
        // 弾の位置を更新
        transform.Translate(direction * speed * Time.deltaTime);

        // 弾を回転
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        if(timer >= 2f)
        {
            float startAngle = -spreadAngle / 2;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = startAngle + i * (spreadAngle / (numberOfBullets));

                // 弾を発射
                Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
                Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                bulletRigidbody.velocity = direction * speed;
            }
            timer = 0f;
        }

        if (player.BulletSeverFlag == true)
        {
            float startAngle = -spreadAngle / 2;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = startAngle + i * (spreadAngle / (numberOfBullets));

                // 弾を発射
                Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
                Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                bulletRigidbody.velocity = direction * speed;
            }
            Destroy(this.gameObject);
        }

        if(destroyTimer >= 4f)
        {
            Destroy(this.gameObject);
        }
    }

    // 弾の方向を設定するメソッド
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    // 弾の回転速度を設定するメソッド
    public void SetRotationSpeed(float newRotationSpeed)
    {
        rotationSpeed = newRotationSpeed;
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
