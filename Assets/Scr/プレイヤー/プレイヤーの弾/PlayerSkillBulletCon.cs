using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillBulletCon : MonoBehaviour
{

    private float time = 0;

    private Vector2 middlePos;

    private GameObject player;
    
    private GameObject boss;


    // Start is called before the first frame update
    void Start()
    {
       boss = GameObject.FindGameObjectWithTag("Boss");
       player = GameObject.FindGameObjectWithTag("Player");
       middlePos = (boss.transform.position - player.transform.position) / 2;
       middlePos.y += 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if(boss != null)
        {
            time += Time.unscaledDeltaTime * 0.5f;
            var a = Vector3.Lerp(player.transform.position, middlePos, time);
            var b = Vector3.Lerp(middlePos,boss.transform.position,time);
            this.transform.position = Vector3.Lerp(a,b,time);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
