using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLineBullet : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private float time =0;

    private Vector3 keepPos;

    private int maxCount = 2;

    private int count =0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
            count ++;
            StartCoroutine(DirectionChange());
           
        }
    }

    private IEnumerator DirectionChange()
    {
        if (count < maxCount)
        {
            var t = transform.position;

            yield return new WaitForSecondsRealtime(0.1f);

            var t2 = transform.position;

            var dir = (t2 - t) * 5;
            Debug.Log(dir);

            transform.position = new Vector2(transform.position.x + (dir.x), transform.position.y + (dir.y));
            keepPos = transform.position;

        }
        else if (count == maxCount)
        {
            transform.position = keepPos;
        }

        rigidbody.velocity *= -1;

        StopCoroutine(DirectionChange());
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}