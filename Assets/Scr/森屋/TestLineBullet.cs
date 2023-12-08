using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLineBullet : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private float speed = 2f;
    private float time =0;

    private int count =0;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //rigidbody.velocity = Vector2.left *speed;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime *3;
        if(count == 2)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SkillBulletOutLine"))
        {
            Debug.Log("‚ ‚½‚Á‚½‚æ");
            StartCoroutine(DirectionChange());
           
        }
    }

    private IEnumerator DirectionChange()
    {
        var vec = transform.position;

        yield return null;

        var vec2 = transform.position;

        var dir = vec2 - vec;
       
        transform.position = new Vector2(dir.x  + transform.position.x * -1,dir.y + transform.position.y * -1); 

        //rigidbody.velocity = rigidbody.velocity * -1;

        count++;
        StopCoroutine(DirectionChange());
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
