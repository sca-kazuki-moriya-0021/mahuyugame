using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    [SerializeField, Header("Œ‚‚¿•Ô‚µ’eƒvƒŒƒnƒu")]
    GameObject deathBulletPoint;
    [SerializeField, Header("‘Ì—Í")]
    int hp;
    [SerializeField] float speed;  
    Vector3 movePosition;

    void Start()
    {
        GameObject LeftPos = GameObject.Find("LeftPos");
        GameObject RightPos = GameObject.Find("RightPos");
        movePosition = moveRandomPosition();  
    }

    void Update()
    {
        if (movePosition == transform.position)
        {
            movePosition = moveRandomPosition();
        }
        this.transform.position = Vector3.MoveTowards(transform.position, movePosition, speed * Time.deltaTime);
    }

    private Vector3 moveRandomPosition()
    {
        Vector3 randomPosi = new Vector3(Random.Range(9, 0), Random.Range(-5,5), 0);
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

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
