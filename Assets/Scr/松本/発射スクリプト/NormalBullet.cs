using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab; // 弾のプレハブ
    [SerializeField] float spawnInterval = 1.0f; // 弾の生成間隔
    [SerializeField] float spreadAngle = 45.0f; // 扇形の角度
    [SerializeField] private float bulletSpeed;
    private BossCollder bossCollder;
    private Player player;

    private void Start()
    {
        bossCollder = FindObjectOfType<BossCollder>();
        player = FindObjectOfType<Player>();
        // 指定した間隔で弾を生成
        InvokeRepeating("SpawnBullet", 0f, spawnInterval);
    }

    private void Update()
    {
        if (bossCollder.BossDeathFlag == true)
        {
            Destroy(this.gameObject);
        }
    }

    void SpawnBullet()
    {
        if(player.BulletSeverFlag == false)
        {
            Vector3 spawnPosition = transform.position;

            // ランダムな角度を生成
            float randomAngle = Random.Range(-spreadAngle / 2, spreadAngle / 2);

            // 角度をラジアンに変換
            float radianAngle = Mathf.Deg2Rad * randomAngle;

            // 弾の向きを設定
            Quaternion rotation = Quaternion.Euler(0f, 0f, radianAngle);

            // 弾を生成
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            // 弾の速度ベクトルを設定して、指定された角度に飛ばす
            Vector2 bulletDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.left;
            rb.velocity = bulletDirection * bulletSpeed;
        }   
    }
}
