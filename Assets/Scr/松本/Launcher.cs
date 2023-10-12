using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; //弾のプレハブ
    [SerializeField] private float bulletSpeed = 5f; //弾の速度
    [SerializeField] private int numberOfBullets = 8; //最初の弾の数
    [SerializeField] private float spreadAngle = 45f; //最初の放射状の角度
    [SerializeField] private float bulletSpacing = 0.0f; //弾の間隔
    [SerializeField] private int bulletAmount = 1; //一回の弾の増加量
    [SerializeField] private float createBullet = 5.0f;//弾を増やす時間
    [SerializeField] private float timeAngle = 15f;//角度を増やす時間間隔
    [SerializeField] private float yimespreadAngle = 10f;//増加角度

    private float elaTime;//経過時間
    private int curbullet;//現在の弾
    void Update()
    {
        ShootNWayBullets();
    }

    private void ShootNWayBullets()
    {
        float angleStep = spreadAngle / (numberOfBullets - 1);
        float initialAngle = transform.eulerAngles.z - (spreadAngle / 2);
        bulletSpacing += Time.deltaTime;
        if (bulletSpacing > 1.0f)
        {
            for (int i = 0; i < numberOfBullets; i++)
            {
                // 弾を生成して、初期位置を設定
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                float bulletAngle = initialAngle + (i * angleStep);

                // 弾の向きをカスタマイズするために、弾の角度を変更
                bullet.transform.rotation = Quaternion.Euler(0, 0, bulletAngle);

                Vector2 bulletDirection = Quaternion.Euler(0, 0, bulletAngle) * Vector2.up;
                rb.velocity = bulletDirection * bulletSpeed;
            }
            bulletSpacing = 0.0f;
        }
            
    }
}
