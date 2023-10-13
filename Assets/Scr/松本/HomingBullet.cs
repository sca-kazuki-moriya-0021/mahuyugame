using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [SerializeField, Header("弾の追尾対象")] Transform target;
    [SerializeField, Header("弾の速度")] float speed = 5f;
    [SerializeField, Header("ホーミングが有効な時間")] float homingDuration = 2.0f; // ホーミングが有効な時間

    private bool homingEnabled = true;
    private float homingTimer = 0.0f;

    private Vector3 forwardDirection; // ホーミングが無効になった後の進行方向

    private void Start()
    {
        forwardDirection = transform.up; // 初期進行方向を設定
    }

    private void Update()
    {
        if (homingEnabled)
        {
            if (target != null)
            {
                Homing();
                homingTimer += Time.deltaTime;

                if (homingTimer >= homingDuration)
                {
                    homingEnabled = false;
                    forwardDirection = (target.position - transform.position).normalized; // ターゲットの方向に進行方向を設定
                }
            }
            else
            {
                Forward();
            }
        }
        else
        {
            Forward();
        }
    }

    private void Homing()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 velocity = direction * speed;
        transform.position += velocity * Time.deltaTime;
    }

    private void Forward()
    {
        Vector3 velocity = forwardDirection * speed;
        transform.position += velocity * Time.deltaTime;
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
