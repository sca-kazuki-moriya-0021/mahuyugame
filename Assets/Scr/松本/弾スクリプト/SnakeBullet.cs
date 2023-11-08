using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBullet : MonoBehaviour
{
    [SerializeField,Header("’e‚Ì‘¬“x")]
    float speed;
    [SerializeField,Header("“®‚«‚ÌU‚ê•")]
    float amplitude;
    [SerializeField,Header("“®‚«‚Ìü”g”")]
    float frequency;
    private float startTime;
    private Vector3 initialPosition;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        initialPosition = transform.position;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Mathf.Sin((Time.time - startTime) * frequency) * amplitude;
        Vector3 newPosition = initialPosition + Vector3.up * offset;

        transform.Translate(Vector3.left * speed * Time.deltaTime);
        //‰E•ûŒü‚Éi‚ß‚½‚¢‚È‚çVector3.right‚É•ÏX‚·‚é

        transform.position = new Vector3(transform.position.x,newPosition.y,transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyBullet") && player.BulletSeverFlag)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyBullet") && player.BulletSeverFlag)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
