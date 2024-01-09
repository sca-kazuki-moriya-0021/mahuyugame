using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    private float limit = 20;
    private float period = 2;
    private bool isHomingMove = true;

    private Rigidbody2D rb;
    private Transform target;

    public bool IsHomingMove
    {
        get { return isHomingMove;}
        set { isHomingMove = value;}
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (IsHomingMove)
        {
            var acceleration = Vector2.zero;
            var diff = (Vector2)target.position - rb.position;

            acceleration += (diff - rb.velocity * period) * 2f / (period * period);

            if(acceleration.magnitude > limit)
            {
                acceleration = acceleration.normalized * limit;
            }

            period -= Time.deltaTime;
            rb.velocity += acceleration * Time.deltaTime;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}