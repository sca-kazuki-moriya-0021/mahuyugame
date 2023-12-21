using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoglyphBullet : MonoBehaviour
{

    private float speed = 0.1f;

    private GameObject player;

    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private CircleCollider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.enabled = false;
        collider2D.enabled = false;
        StartCoroutine(Move());
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);
        collider2D.enabled = true;
        spriteRenderer.enabled = true;
        rigidbody2D.velocity = Vector3.zero;
        var p = player. transform.position;
        yield return null;
        Vector3 v = p -transform.position;
        rigidbody2D.velocity = v * speed;
    }
}
