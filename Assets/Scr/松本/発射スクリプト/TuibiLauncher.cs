using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuibiLauncher : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab; // 弾のプレハブ
    [SerializeField] Transform firePoint; // 弾の発射位置
    [SerializeField] float maxCount;
    [SerializeField] private float shotDelay = 0.3f; // 弾の発射間隔
    [SerializeField]private float intervalDuration = 0.7f; // インターバルの長さ
    private float nextFireTime = 0; // 次に発射可能な時間
    private int shotsFired = 0; // 発射回数をカウント
    private bool isInterval = false; // インターバル中かどうかを示すフラグ

    void Update()
    {
        if (isInterval && Time.time >= nextFireTime)
        {
            isInterval = false;
            shotsFired = 0; // 発射回数をリセット
        }

        if (!isInterval && Time.deltaTime >= nextFireTime)
        {
            Shoot();
            shotsFired++;

            if (shotsFired >= maxCount)
            {
                isInterval = true;
                nextFireTime = Time.time + intervalDuration;
            }
            else
            {
                nextFireTime = Time.time + shotDelay;
            }
        }
    }

    void Shoot()
    {
        // 弾のプレハブから新しい弾を生成
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 生成した弾を発射
        Rigidbody2D bulletRB = newBullet.GetComponent<Rigidbody2D>();
    }
}
