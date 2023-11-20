using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBullet : MonoBehaviour
{
    private GameObject target;
    private Queue<GameObject> searchObjects;
    private Player player;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        searchObjects = FindEnemy();
        //�T���Ă���0�Ԗڂ̓G��ڕW�ɂ���
        if(searchObjects != null)
        {
            target = searchObjects.Peek();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(searchObjects == null)
        {
            if (player.PBaffSkillFlag == true)
                transform.Translate(Vector3.right * Time.deltaTime * 15.0f);
            else
                transform.Translate(Vector3.right * Time.deltaTime * 10.0f);
        }


        if(target == null)
        {
            searchObjects.Dequeue();
            Debug.Log(searchObjects.Count);
            if (searchObjects.Peek() != null)
            {
                target = searchObjects.Peek();
            }
            if(searchObjects.Peek() == null)
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

    //�G��T���ĕۑ�����֐�
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
        else{
            queue = null;
        }

        if(boss != null)
        {
            queue.Enqueue(boss);
        }
        return queue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("misu");
        if (collision.gameObject.CompareTag("Enemy"))
        {

            Destroy(collision.gameObject);
            Destroy(this.gameObject);
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

}
