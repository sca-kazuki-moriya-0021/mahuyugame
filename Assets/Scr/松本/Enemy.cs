using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    [SerializeField, Header("‘Ì—Í")]
    int hp;
    [SerializeField] float speed;  
    Vector3 movePosition; 
    [SerializeField] private int numberOfBullets;
    [SerializeField] private float spreadAngle;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

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
                float startAngle = -spreadAngle / 2;

                for (int i = 0; i < numberOfBullets; i++)
                {
                    float angle = startAngle + i * (spreadAngle / (numberOfBullets));

                    // ’e‚ð”­ŽË
                    Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
                    Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                    bulletRigidbody.velocity = direction * bulletSpeed;
                }
                Destroy(gameObject, 1);
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
