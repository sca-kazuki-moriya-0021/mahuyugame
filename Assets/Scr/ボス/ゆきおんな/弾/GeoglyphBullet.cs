using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GeoglyphBullet : MonoBehaviour
{

    private float speed = 1f;

    private float time = 0f;

    private GameObject player;

    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private CircleCollider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.material.color = new Color(255,255,255,0);
        collider2D.enabled = false;

        Tween t = spriteRenderer.material.DOFade(255f, 1f);
        t.Play();
        //t.OnComplete(() => {StartCoroutine(Move())});

        rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
       time += Time.deltaTime;
       if(time > 1F)
       {
           

            StartCoroutine(Move());
       }

    }

    private IEnumerator Move()
    {

 
        yield return new WaitForSeconds(1f);
        collider2D.enabled = true;

        rigidbody2D.velocity = Vector3.zero;
        var p = player. transform.position;
        yield return null;
        Vector3 v = p -transform.position;
        rigidbody2D.velocity = v * speed;
    }
}
