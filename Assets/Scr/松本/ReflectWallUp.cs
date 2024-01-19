using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectWallUp : MonoBehaviour
{
    [SerializeField]
    string bulletTag1 = "ReflectBullet";
    [SerializeField]
    string bulletTag2 = "ApolloReflector";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(bulletTag1))
        {
            ReflectBullet(collision.gameObject);
        }

        if (collision.CompareTag(bulletTag2))
        {
            ApolloReflector(collision.gameObject);
        }
    }

    private void ReflectBullet(GameObject bullet)
    {
        Debug.Log("a");
        var rb = bullet.GetComponent<Rigidbody2D>();
        if(rb == null)return;

        var inDirection = rb.velocity;
        var inNormal = transform.up;
        var result = Vector2.Reflect(inDirection,inNormal);

        rb.velocity = result;

        BulletCon ballCountScript = bullet.GetComponent<BulletCon>();
        if (ballCountScript != null)
        {
            ballCountScript.IncrementCount();

            
            if (ballCountScript.GetCount() >= 5)
            {
                Destroy(bullet);
            }
        }
    }

    private void ApolloReflector(GameObject bullet)
    {
        var rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        var inDirection = rb.velocity;
        var inNormal = transform.up;
        var result = Vector2.Reflect(inDirection, inNormal);

        rb.velocity = result * 1.5f;

        BulletCon ballCountScript = bullet.GetComponent<BulletCon>();
        if (ballCountScript != null)
        {
            ballCountScript.IncrementCount();


            if (ballCountScript.GetCount() >= 5)
            {
                Destroy(bullet);
            }
        }
    }
}
