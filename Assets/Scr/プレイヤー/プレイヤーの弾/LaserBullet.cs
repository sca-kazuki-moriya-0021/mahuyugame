using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    private int power =1;
    private bool laser;

    private float velocity;
    private Vector3 angle;

    private Vector3 lastVelocity = Vector3.zero;

    private Rigidbody2D rb2d;

    public float Velocity { get => velocity; set => velocity = value; }
    public Vector3 Angle { get => angle; set => angle = value; }

    // Start is called before the first frame update
    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();
       //äpìxÇçló∂ÇµÇƒíeÇÃë¨ìxåvéZ
       Vector3 bulletV = rb2d.velocity;
       bulletV = velocity * angle;
       bulletV.z = 0;
       rb2d.velocity = bulletV;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(rb2d.velocity);
       lastVelocity = rb2d.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OutLine"))
        {
            Vector3 normal = collision.contacts[0].normal;
            Vector3 refrect = Vector3.Reflect(lastVelocity,normal);
            refrect.z = 0;
            rb2d.velocity = refrect;
        }

        if (collision.gameObject.CompareTag("DestroyBullet"))
        {
            power = 0;
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyBullet"))
        {
            power = 0;
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        if(laser == false)
        {
            Destroy(this.gameObject);
        }
        else
        {
            power = 0;
        }
    }

}
