using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    private float velocity;
    private Vector2 angle;

    private Vector2 lastVelocity = Vector2.zero;

    private Rigidbody2D rb2d;

    public float Velocity { get => velocity; set => velocity = value; }
    public Vector2 Angle { get => angle; set => angle = value; }

    // Start is called before the first frame update
    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();

       //äpìxÇçló∂ÇµÇƒíeÇÃë¨ìxåvéZ
       Vector2 bulletV = rb2d.velocity;
       bulletV = velocity * angle;
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
