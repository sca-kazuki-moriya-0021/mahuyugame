using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    float time = 0;
    float angle = 45;
    float speed = 2;
    Vector3 vec;
    Vector2 lastVelocity = Vector2.zero;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lastVelocity = rb2d.velocity;
        transform.Translate(Vector3.right * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("OutLine"))
        {
            Vector2 normalVector = collision.contacts[0].normal;
            Vector2 refrectVector = Vector2.Reflect(lastVelocity,normalVector);
            rb2d.velocity = refrectVector;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
