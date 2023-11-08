using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A : MonoBehaviour
{
    public float bulletSpeed = 5.0f; // 弾の速度
    public float homingDuration = 2.0f; // 追尾の持続時間
    private Transform target; // 追尾対象（通常はプレイヤー）
    private Rigidbody2D rb;
    private float homingTimer = 0.0f;
    private bool isHoming = true;
    private Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
        // 追尾対象を設定（通常はプレイヤー）
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(player.BulletSeverFlag == true)
        {
            Destroy(this.gameObject);
        }

        if (isHoming)
        {
            if (target != null)
            {
                // プレイヤーの位置を目標とし、弾をその方向に進ませる
                Vector2 direction = (target.position - transform.position).normalized;
                rb.velocity = direction * bulletSpeed;

                // 追尾タイマーを更新
                homingTimer += Time.deltaTime;

                // 指定の時間経過後に追尾を終了
                if (homingTimer >= homingDuration)
                {
                    isHoming = false;
                }
            }
            else
            {
                // 追尾対象が破壊された場合、追尾を終了して画面外に向かって進む
                isHoming = false;
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
