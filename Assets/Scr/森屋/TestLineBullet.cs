using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLineBullet : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private float speed = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.left *speed;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.left);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SkillBulletOutLine"))
        {
            Debug.Log("‚ ‚½‚Á‚½‚æ");
            rigidbody.velocity = Vector2.left * -speed;
        }
    }
}
