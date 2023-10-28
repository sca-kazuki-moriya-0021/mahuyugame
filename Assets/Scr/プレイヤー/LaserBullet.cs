using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    private float velocity;
    private float theta;

    private Vector2 lastVelocity = Vector2.zero;

    private Rigidbody2D rb2d;
    private CircleCollider2D collider2D;

    public float Velocity { get => velocity; set => velocity = value; }
    public float Theta { get => theta; set => theta = value; }

    // Start is called before the first frame update
    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();
       collider2D = GetComponent<CircleCollider2D>();

       //äpìxÇçló∂ÇµÇƒíeÇÃë¨ìxåvéZ
       Vector2 bulletV = rb2d.velocity;
       bulletV.x = velocity * Mathf.Cos(theta);
       bulletV.y = velocity * Mathf.Sin(theta);
       rb2d.velocity = bulletV;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
       lastVelocity = rb2d.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OutLine"))
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 refrect = Vector2.Reflect(lastVelocity,normal);
            rb2d.velocity = refrect;
        }
     
    }


    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
