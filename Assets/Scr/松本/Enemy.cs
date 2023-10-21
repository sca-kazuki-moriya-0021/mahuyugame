using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("弾の発射プレハブ")]
    GameObject bulletPoint;
    [SerializeField, Header("撃ち返し弾プレハブ")]
    GameObject deathBulletPoint;
    [SerializeField, Header("体力")]
    int hp;
    public Transform centerPoint;  // 回転の中心点
    public float rotationSpeed = 30.0f;  // 移動速度（度/秒）

    void Start()
    {
        bulletPoint.SetActive(true);
    }

    void Update()
    {
        // 中心点を中心に時計回りに移動
        Vector2 direction = (Vector2)(transform.position - centerPoint.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= rotationSpeed * Time.deltaTime;

        // 新しい位置を計算
        float radius = direction.magnitude;
        float newX = centerPoint.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float newY = centerPoint.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

        // キャラクターの位置を更新
        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("PlayerBullet"))
        {
            //hp = hp - BulletPower;
            if (hp == 0)
            {
                deathBulletPoint.SetActive(true);
                Invoke("Death", 3.0f);
            }
        }
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
