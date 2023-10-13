using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingLauncher : MonoBehaviour
{
    [SerializeField,Header("射撃地点1")]
    GameObject Fire1;
    [SerializeField,Header("射撃地点2")]
    GameObject Fire2;
    [SerializeField,Header("ホーミング弾")]
    GameObject HomingBullet;
    [SerializeField,Header("発射間隔")]
    float fireInterval = 1.0f;

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
        if (firePoint != null && HomingBullet != null)
        {
            // ホーミング弾のインスタンスを生成
            GameObject homingBullet = Instantiate(HomingBullet, firePoint.transform.position, Quaternion.identity);
        }
    }
}
