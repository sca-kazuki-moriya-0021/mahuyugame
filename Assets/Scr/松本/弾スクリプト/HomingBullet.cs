using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [SerializeField]
    private float limit;
    [SerializeField]
    private float period;

    private bool isMove = true;

    private Rigidbody2D rb;
    private GameObject target;

    public bool IsMove
    {
        get { return isMove; }
        set { isMove = value; }
    }

    void Awake() // Awakeでの初期化を推奨
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate() // UpdateではなくFixedUpdateを使用
    {
        if (IsMove && target != null)
        {
            Vector2 acceleration = Vector2.zero;
            Vector2 diff = (Vector2)target.transform.position - rb.position; // rb.positionからVector2に変換
            acceleration += (diff - rb.velocity * period) * 2f / (period * period);

            if (acceleration.magnitude > limit)
            {
                acceleration = acceleration.normalized * limit;
            }

            period -= Time.fixedDeltaTime; // periodをTime.fixedDeltaTimeで減算
            rb.velocity += acceleration * Time.fixedDeltaTime; // FixedDeltaTimeを使用して速度を更新
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}