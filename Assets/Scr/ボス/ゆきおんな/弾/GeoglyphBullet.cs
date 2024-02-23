using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GeoglyphBullet : MonoBehaviour
{

    private float speed = 1.5f;

    private float time = 0f;


    private GameObject player;

    private Player playerCon;

    private Rigidbody2D rigidbody2D;

    private bool flag = false;

    private int shootCount = 0;

    //画面外に行った時のカウント
    private int count = 0;


    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private CircleCollider2D collider2D;

    public int ShootCount
    {
        get { return this.shootCount; }
        set { this.shootCount = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.material.color = new Color(255, 255, 255, 0f);

        rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerCon = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        time+= Time.deltaTime;
        if (time > 1 && flag == false)
        {
           StartCoroutine(Move());
           flag = true;
        }

        if(playerCon.BulletSeverFlag == true)
         Destroy(this.gameObject);
    }

    private IEnumerator Move()
    {
        for (int i = 0; i < 255; i++)
        {
            spriteRenderer.material.color = new Color(255, 255, 255, i);
            spriteRenderer.material.color = new Color(255, 255, 255, i);
            yield return null;
        }
        collider2D.enabled = true;
        rigidbody2D.velocity = Vector3.zero;
        var p = player. transform.position;
        if(shootCount % 2 != 0)
        yield return new WaitForSeconds(6f);
        else
        yield return new WaitForSeconds(4f);
        Vector3 v = p - transform.position;
        v.Normalize();
        rigidbody2D.velocity = v * speed;
    }

    private void OnBecameInvisible()
    {
        count++;
        if(count >= 2)
            Destroy(this.gameObject);
    }
}
