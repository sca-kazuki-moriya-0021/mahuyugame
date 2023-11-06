using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb;
    private TotalGM gm;

    private float moveStoptime = 100.0f;
    private float countTime;
    private bool moveFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        gm =FindObjectOfType<TotalGM>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        if (moveFlag == true)
        {
            rb.AddForce(Vector2.left * 0.5f);
        }

        if(player.SkillAtkFlag[0] == true && gm.PlayerSkill[0] == true)
        {
            while(countTime <= moveStoptime)
            {
                Debug.Log("“ü‚Á‚Ä‚é");
                countTime += Time.deltaTime;
                Debug.Log(countTime);
                moveFlag = false;
                if (countTime > moveStoptime )
                {
                    countTime = 0;
                    moveFlag = true;
                    player.SkillAtkFlag[0] = false;
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
