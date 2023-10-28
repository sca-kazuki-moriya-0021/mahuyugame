using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField, Range(0, 360)] private float launchAngle = 45.0f;
    [SerializeField] private float fireTime;

    private float bulletTime = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        bulletTime += Time.deltaTime;
        if(bulletTime > fireTime)
        {
            ShootBulletWithCustomDirection();
            bulletTime = 0.0f;
        }
    }

    private void ShootBulletWithCustomDirection()
    {
        // 弾を生成して、初期位置を設定
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // 弾の向きをカスタマイズするために、弾の角度を変更
        bullet.transform.rotation = Quaternion.Euler(0, 0, launchAngle);

        // 弾の速度ベクトルを設定して、指定された角度に飛ばす
        Vector2 bulletDirection = Quaternion.Euler(0, 0, launchAngle) * Vector2.up;
        rb.velocity = bulletDirection * bulletSpeed;
    }
}
