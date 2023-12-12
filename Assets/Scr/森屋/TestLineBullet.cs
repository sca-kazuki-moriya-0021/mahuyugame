using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLineBullet : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private float speed;
    private float time =0;

    private int maxCount = 2;

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
            if (count == 3)
            {
                Destroy(this.gameObject);
            }
            StartCoroutine(DirectionChange());
           
        }
    }

    private IEnumerator DirectionChange()
    {
        

        var t = transform.position;

        Debug.Log("åªç›ÇÃç¿ï\" + t);

        yield return new WaitForSecondsRealtime(0.1f);

        var t2 = transform.position;

        Debug.Log("1få„ÇÃç¿ï\"+t2);

        var dir = (t2 - t) * 5;
        Debug.Log(dir);

        count++; 

        if (count < maxCount)
        { 
            transform.position = new Vector2(transform.position.x + (dir.x), transform.position.y + (dir.y));
            //Debug.Log(transform.position);
        }
        else if (count == maxCount)
        {
            transform.position = new Vector2(transform.position.x - (dir.x), transform.position.y - (dir.y));
        }

        rigidbody.velocity *= -1;

        StopCoroutine(DirectionChange());
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}