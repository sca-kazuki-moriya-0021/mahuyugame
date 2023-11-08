using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : MonoBehaviour
{
    public float speed = 5.0f; // 弾の速度
    public float rotationSpeed = 45.0f; // 回転速度

    private Player player;
    private Vector2 direction;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        // 弾の位置を更新
        transform.Translate(direction * speed * Time.deltaTime);

        // 弾を回転
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyBullet") && player.BulletSeverFlag)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyBullet") && player.BulletSeverFlag)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
