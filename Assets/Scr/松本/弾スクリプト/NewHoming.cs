using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHoming : MonoBehaviour
{
    [SerializeField]
    private float limit;
    [SerializeField]
    private float period;

    private bool isMove;

    private Rigidbody2D rb;
    private Transform target;

    public bool IsMove
    {
        get { return isMove; }
        set { isMove = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMove)
        {
            var acceleration = Vector2.zero; // Vector2�ɕύX
            var diff = (Vector2)target.position - rb.position; // rb.position�ɕύX
            acceleration += (diff - rb.velocity * period) * 2f / (period * period);

            if (acceleration.magnitude > limit)
            {
                acceleration = acceleration.normalized * limit;
            }

            period -= Time.deltaTime;
            rb.velocity += acceleration * Time.deltaTime;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
