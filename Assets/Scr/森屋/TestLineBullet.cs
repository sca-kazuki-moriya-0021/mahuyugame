using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLineBullet : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private float speed;
    private float time =0;

    private Vector3 oldVeloctiy;

    private int count =0;

    public float BulletSpeed
    {
        set => speed = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //rigidbody.velocity = Vector2.left *speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SkillBulletOutLine"))
        {
            count++;
            Debug.Log(count);
            Debug.Log(transform.position);
            if (count == 3)
            {
                Destroy(this.gameObject);
            }
            StartCoroutine(DirectionChange());
           
        }
    }

    private IEnumerator DirectionChange()
    {

        Debug.Log("ÇÕÇ¢Ç¡ÇΩÇÊ");
        //rigidbody.velocity = Vector3.zero;
        var t = transform.position;

        yield return new WaitForSeconds(0.01f);

        var t2 = transform.position;
        Debug.Log("1få„ÇÃç¿ï\"+t2);

        var dir = (t2 -t) * 50;


        if (count < 2)
        { 
            transform.position = new Vector2(transform.position.x + (dir.x), transform.position.y + (dir.y));
            Debug.Log(transform.position);
        }
        else if (count == 2)
            transform.position = new Vector2(transform.position.x - (dir.x), transform.position.y - (dir.y));

        rigidbody.velocity = rigidbody.velocity * -1;

        StopCoroutine(DirectionChange());
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
