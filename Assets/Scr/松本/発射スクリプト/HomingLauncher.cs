using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingLauncher : MonoBehaviour
{
    [SerializeField]GameObject Fire1;
    [SerializeField]GameObject Fire2;
    [SerializeField]GameObject homingBullet;
    [SerializeField]float fireInterval = 1.0f;

    private float nextFireTime = 0.0f;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            // 発射間隔が経過したらホーミング弾を発射
            ShootHomingBullet(Fire1);
            ShootHomingBullet(Fire2);
            nextFireTime = Time.time + fireInterval;
        }
    }

    void ShootHomingBullet(GameObject firePoint)
    {
        if (firePoint != null && homingBullet != null)
        {
            // ホーミング弾のインスタンスを生成
            GameObject bullet = Instantiate(homingBullet, firePoint.transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        }
    }
}
