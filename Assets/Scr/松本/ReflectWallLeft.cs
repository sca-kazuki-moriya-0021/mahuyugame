using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectWallLeft : MonoBehaviour
{
    [SerializeField]
    string bulletTag = "ReflectBullet";
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
        if (collision.CompareTag(bulletTag))
        {
            ReflectBullet(collision.gameObject);
        }
    }

    private void ReflectBullet(GameObject bullet)
    {
        var rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        var inDirection = rb.velocity;
        var inNormal = (transform.position.x < 0) ? Vector2.right : Vector2.left;
        var result = Vector2.Reflect(inDirection, inNormal);

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
}
