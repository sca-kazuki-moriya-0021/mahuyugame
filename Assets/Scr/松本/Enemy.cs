using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("弾の発射プレハブ")]
    GameObject bulletPoint;
    [SerializeField, Header("撃ち返し弾プレハブ")]
    GameObject deathBulletPoint;
    [SerializeField, Header("体力")]
    int hp;
    [SerializeField] float speed;  
    Vector3 movePosition;  

    void Start()
    {
        movePosition = moveRandomPosition();  
    }

    void Update()
    {
        if (movePosition == transform.position)
        {
            movePosition = moveRandomPosition();
        }
        this.transform.position = Vector3.MoveTowards(transform.position, movePosition, speed * Time.deltaTime);  //①②playerオブジェクトが, 目的地に移動, 移動速度
    }

    private Vector3 moveRandomPosition()
    {
        Vector3 randomPosi = new Vector3(Random.Range(-7, 7), Random.Range(-4, 4), 5);
        return randomPosi;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("PlayerBullet"))
        {
            //hp = hp - BulletPower;
            if (hp == 0)
            {
                deathBulletPoint.SetActive(true);
                Invoke("Death", 3.0f);
            }
        }
    }
}
