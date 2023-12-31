using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [Header("弾の速度")]
    [SerializeField] float speed;
    [Header("ホーミングが有効な時間")]
    [SerializeField] float homingDuration;
    [Header("ターゲットのタグ")]
    [SerializeField] string targetTag = "Player"; // ターゲットのタグ

    private Transform target;
    private bool homingEnabled = true;
    private float homingTimer = 0.0f;
    private Vector3 forwardDirection;
    private Player player;

    private void Awake()
    {
        // タグを使用してターゲットを見つける
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        if (target == null)
        {
            Debug.LogWarning("ターゲットが見つかりませんでした");
        }
        else
        {
            // 初期進行方向は transform.up のまま
        }
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
                    forwardDirection = (target.position - transform.position).normalized;
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

        if(player.BulletSeverFlag == true)
        {
            Destroy(this.gameObject);
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

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}