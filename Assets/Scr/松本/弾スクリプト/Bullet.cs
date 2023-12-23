using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 3;
    private Player player;
    private Rigidbody2D rb;
    private WayMoveLauncher wayMoveLauncher;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        wayMoveLauncher = GetComponent<WayMoveLauncher>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wayMoveLauncher == null)
        {
            Vector2 randomVelocity = new Vector2(Random.Range(-1f,-1f), Random.Range(-2f, -2f)).normalized;
            rb.velocity = randomVelocity * bulletSpeed;
        }
        if(player.BulletSeverFlag == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
