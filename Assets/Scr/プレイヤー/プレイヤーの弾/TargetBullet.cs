using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBullet : MonoBehaviour
{
    //敵を保存するよう
    private GameObject target;
    //敵の数を所得する用
    private Queue<GameObject> searchObjects;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        searchObjects = FindEnemy();
        //探してきた0番目の敵を目標にする
        if(searchObjects.Count != 0)
            target = searchObjects.Peek();
    }

    // Update is called once per frame
    void Update()
    {
        //ターゲットがいなかったとき
        if(target == null)
        {
            //最初に取得した敵の数が0じゃない時
            if (searchObjects.Count != 0)
            {
                searchObjects.Dequeue();
                //Queueの中身を1つ消した際に敵の数が0じゃなかったらターゲットを再登録する
                if(searchObjects.Count != 0)
                target = searchObjects.Peek();
            }
            //取得した数が0だった時
            else if(searchObjects.Count == 0)
            {
                if (player.PBaffSkillFlag == true)
                    transform.Translate(Vector3.right * Time.deltaTime * 15.0f);
                else
                    transform.Translate(Vector3.right * Time.deltaTime * 10.0f);
            }
        }
        //ターゲットがいた際には、ターゲット方向に進む
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

    //敵を探して保存する関数
    private Queue<GameObject> FindEnemy()
    {
        GameObject[] gos;
        GameObject boss;
        //gos = GameObject.FindGameObjectsWithTag("Enemy");
        boss = GameObject.FindGameObjectWithTag("Boss");
        Queue<GameObject> queue = new Queue<GameObject>();

        /*if(gos != null){
            for (int i = 0; i < gos.Length; i++)
                queue.Enqueue(gos[i]);
        }*/

        if(boss != null)
            queue.Enqueue(boss);

        return queue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
            Destroy(this.gameObject);          

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
