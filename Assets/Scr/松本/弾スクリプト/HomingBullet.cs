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

    void Awake() // Awake�ł̏������𐄏�
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate() // Update�ł͂Ȃ�FixedUpdate���g�p
    {
        if (IsMove && target != null)
        {
            Vector2 acceleration = Vector2.zero;
            Vector2 diff = (Vector2)target.transform.position - rb.position; // rb.position����Vector2�ɕϊ�
            acceleration += (diff - rb.velocity * period) * 2f / (period * period);

            if (acceleration.magnitude > limit)
            {
                acceleration = acceleration.normalized * limit;
            }

            period -= Time.fixedDeltaTime; // period��Time.fixedDeltaTime�Ō��Z
            rb.velocity += acceleration * Time.fixedDeltaTime; // FixedDeltaTime���g�p���đ��x���X�V
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}