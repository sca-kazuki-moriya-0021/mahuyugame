using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBullet : MonoBehaviour
{
    private GameObject target;
    private Queue<GameObject> searchObjects;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        searchObjects = FindEnemy();
        //íTÇµÇƒÇ´ÇΩ0î‘ñ⁄ÇÃìGÇñ⁄ïWÇ…Ç∑ÇÈ
        if(searchObjects.Count != 0)
        {
            target = searchObjects.Peek();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            if (searchObjects.Count != 0)
            {
                searchObjects.Dequeue();
                if(searchObjects.Count != 0)
                target = searchObjects.Peek();
            }
            else if(searchObjects.Count == 0)
            {
                if (player.PBaffSkillFlag == true)
                    transform.Translate(Vector3.right * Time.deltaTime * 15.0f);
                else
                    transform.Translate(Vector3.right * Time.deltaTime * 10.0f);
            }
        }
        else if(target != null)
        {
            var dir = target.transform.position - transform.position;
            dir = dir.normalized;
            if (player.PBaffSkillFlag == true)
                transform.Translate(dir * Time.deltaTime * 15.0f);
            else
                transform.Translate(dir * Time.deltaTime * 10.0f);
        }
    }

    //ìGÇíTÇµÇƒï€ë∂Ç∑ÇÈä÷êî
    private Queue<GameObject> FindEnemy()
    {
        GameObject[] gos;
        GameObject boss;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        boss = GameObject.FindGameObjectWithTag("Boss");
        Queue<GameObject> queue = new Queue<GameObject>();

        if(gos != null){
            for (int i = 0; i < gos.Length; i++)
            {
                queue.Enqueue(gos[i]);
            }
        }
        if(boss != null)
        {
            queue.Enqueue(boss);
        }
        return queue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            Destroy(this.gameObject);          
        }

        if (collision.gameObject.CompareTag("DestroyBullet"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyBullet"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
