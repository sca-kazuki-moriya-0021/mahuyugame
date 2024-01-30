using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    private float time = 0;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        //bulletPostion  = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime; 
        //íeÇÃêiÇﬁäÑçáÇTime.deltaTimeÇ≈åàÇﬂÇÈ
        if(player.PBaffSkillFlag == true)
            transform.Translate(Vector3.right * Time.deltaTime * 10.0f * time);
        else
            transform.Translate(Vector3.right * Time.deltaTime * 7.0f * time);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Boss"))
        {
            Destroy(this.gameObject);
        }

        /*if (collision.gameObject.CompareTag("DestroyBullet"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag("DestroyBullet"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }*/
    }
}
