using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBullet : MonoBehaviour
{
    private Vector3 position;
    private GameObject target;
    private Queue<GameObject> searchObjects;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        searchObjects = FindEnemy();
        //’T‚µ‚Ä‚«‚½0”Ô–Ú‚Ì“G‚ğ–Ú•W‚É‚·‚é
        target = searchObjects.Peek();
        //Debug.Log(target);
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update:" + target);

        if(target == null)
        {
            if (player.PBaffSkillFlag == true)
                transform.Translate(Vector3.right * Time.deltaTime * 15.0f);
            else
                transform.Translate(Vector3.right * Time.deltaTime * 10.0f);
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

    //“G‚ğ’T‚µ‚Ä•Û‘¶‚·‚éŠÖ”
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            if(collision.gameObject == target)
            {
                searchObjects.Dequeue();
                target = searchObjects.Peek();
                Debug.Log("Ÿ‚Ì–Ú•W: " +  target);
            }
        }


        if (collision.gameObject.CompareTag("Boss"))
        {
           //Destroy(collision.gameObject);
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

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
