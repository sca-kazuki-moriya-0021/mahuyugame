using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("�e�̔��˃v���n�u")]
    GameObject bulletPoint;
    [SerializeField, Header("�����Ԃ��e�v���n�u")]
    GameObject deathBulletPoint;
    [SerializeField, Header("�̗�")]
    int hp;
    public float speed;
    Vector3 movePoint;

    void Start()
    {
        bulletPoint.SetActive(true);
        movePoint = RandomMove();
    }

    void Update()
    {
        if(movePoint == this.transform.position)
        {
            movePoint = RandomMove();
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position,movePoint,speed * Time.deltaTime);
    }

    private Vector3 RandomMove()
    {
        Vector3 randomPos = new Vector3(Random.Range(0,15),Random.Range(5,-4),0);
        return randomPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("PlayerBullet"))
        {
            //hp = hp - BulletPower;
            if (hp == 0)
            {
                deathBulletPoint.SetActive(true);
                //Invoke("Death", 3.0f);
            }
        }
    }

     void OnBecameInvisible()
    {
        Debug.Log("aaa");
        Destroy(this.gameObject);
    }
}
