using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField, Header("弾のプレハブ")] private GameObject bulletPrefab;
    [SerializeField, Header("弾の速度")] private float bulletSpeed;
    [SerializeField, Header("最初の弾の数")] private int numberOfBullets;
    [SerializeField, Header("最初の放射状の角度")] private float spreadAngle;
    [SerializeField, Header("発射間隔")] private float bulletSpacing;
    [SerializeField, Header("変更用発射間隔")] private float maxbulletSpacing;
    [SerializeField, Header("一回の弾の増加量")] private int bulletAmount;
    [SerializeField, Header("弾を増やす時間")] private float createBullet;
    [SerializeField, Header("角度を増やす時間")] private float timeAngle;
    [SerializeField, Header("増加角度")] private float yimespreadAngle;
    [SerializeField, Header("最大弾数")] private int maxBullet;

    private float bulletsTime; // 弾経過時間
    private float elaTime; // 角度経過時間
    private int curBullet; // 現在の弾
    private float curAngle; // 現在の角度

    private Transform player; // プレイヤーのTransformコンポーネント

    void Start()
    {
        curBullet = numberOfBullets;
        curAngle = spreadAngle;

        // プレイヤーのTransformを取得
        player = GameObject.FindWithTag("Player").transform; // プレイヤーのタグに応じて変更
    }

    void Update()
    {
        bulletsTime += Time.deltaTime;
        elaTime += Time.deltaTime;
        if (curBullet < maxBullet && bulletsTime >= createBullet)
        {
            curBullet += bulletAmount;
            curAngle += yimespreadAngle;
            bulletsTime = 0.0f;
        }
        if(curBullet == maxBullet)
        {
            maxbulletSpacing = 0.5f;
        }
        if (elaTime >= timeAngle)
        {
            Debug.Log("a");
            curAngle += yimespreadAngle;
            elaTime = 0.0f;
        }
        ShootNWayBullets(curBullet, curAngle);
    }

    private void ShootNWayBullets(int curBullet, float curAngle)
    {
        float angleStep = curAngle / (curBullet - 1);
        float initialAngle = transform.eulerAngles.z - (curAngle / 2);
        bulletSpacing += Time.deltaTime;

        if (bulletSpacing > maxbulletSpacing)
        {
            Vector2 playerPosition = player.position; // プレイヤーの位置を取得

            for (int i = 0; i < curBullet; i++)
            {
                // 弾を生成して、初期位置を設定
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                float bulletAngle = initialAngle + (i * angleStep);

                // プレイヤーの位置から弾の向きを計算
                Vector2 directionToPlayer = (playerPosition - (Vector2)transform.position).normalized;
                float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

                // 弾の向きを設定
                bullet.transform.rotation = Quaternion.Euler(0, 0, bulletAngle + angleToPlayer);

                // 弾の速度を設定
                rb.velocity = bullet.transform.up * bulletSpeed;

                bulletSpacing = 0.0f;
            }
        }
    }
}
