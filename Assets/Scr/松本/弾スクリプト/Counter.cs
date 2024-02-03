using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    private float Speed = 5f;
    private int numberOfBullets = 20;
    private Player player;
    private float destroyTimer;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        destroyTimer += Time.deltaTime;

        if (player.BulletSeverFlag == true)
        {
            float startAngle = -360 / 2;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = startAngle + i * (360 / (numberOfBullets));

                // ’e‚ð”­ŽË
                Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
                Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                bulletRigidbody.velocity = direction * Speed;
            }
            Destroy(gameObject);
        }

        if (destroyTimer >= 1f)
        {
            float startAngle = -360 / 2;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = startAngle + i * (360 / (numberOfBullets));

                // ’e‚ð”­ŽË
                Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
                Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                bulletRigidbody.velocity = direction * Speed;
            }
            Destroy(this.gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
