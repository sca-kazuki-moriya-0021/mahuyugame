using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb;
    private TotalGM gm;

    private int hp = 100;

    private bool moveFlag = true;
    private float moveStoptime = 5.0f;
    private float moveStopCountTime;
    private bool debuffFlag = false;
    private float debuffTime;
    private float debuffCountTime;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        gm =FindObjectOfType<TotalGM>();
        rb = GetComponent<Rigidbody2D>();

        //player.SkillAtkFlag[0] = true;
        //gm.PlayerSkill[0] = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (moveFlag == true)
        {
            Move();
        }

        if(player.BussMoveStopFlag == true)
        {
            if(moveStopCountTime <= moveStoptime)
            {
                moveStopCountTime += Time.deltaTime;
                moveFlag = false;
                if (moveStopCountTime > moveStoptime )
                {
                    moveStopCountTime = 0;
                    moveFlag = true;
                    player.BussMoveStopFlag = false;
                }
            }
        }

        if(debuffFlag == true)
        {
            if (debuffCountTime <= debuffTime)
            {
                debuffCountTime+=Time.deltaTime;
                
                if (debuffCountTime > debuffTime)
                {
                    debuffCountTime = 0;
                    debuffFlag = true;
                }
            }
        }

    }

    private void Move()
    {
        if (debuffFlag == true)
            rb.AddForce(Vector2.left * 0.5f);
        else
            rb.AddForce(Vector2.left * 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            HitBullet();
        }

        if (collision.gameObject.CompareTag("PlayerSkillBullet"))
        {
            Destroy(collision.gameObject);
            debuffFlag = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            HitBullet();
        }
    }

    private void HitBullet()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
        if (debuffFlag == true)
            hp -= 2;
        else
            hp--;
    } 

    
}
