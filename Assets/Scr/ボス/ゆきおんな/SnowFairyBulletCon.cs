using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFairyBulletCon : MonoBehaviour
{
    //�e�I�u�W�F�N�g
    [SerializeField]
    private GameObject[] bullets;
    //�e�̃X�s�[�h
    [SerializeField]
    private float[] bulletSpeed;
    //Way�e�̔��ˊp�x
    [SerializeField]
    private float launchWayAngle;
    //���˂���Way�e�̐�
    [SerializeField]
    private int launchWaySpilt;

    private float _theta;
    float PI= Mathf.PI;

    //�e�̔��ˊ��o
    [SerializeField]
    private float fireTime;

    private float time = 0;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > fireTime)
        {
            ShootWayBullet();
            //ShootBulletWithCustomDirection(count,0);
            //ShootBulletWithCustomDirection(-count,0);
            count += 1;
            time = 0;
        }

        if(time > 120)
        {
           
        }


    }

    private void ShootBulletWithCustomDirection(int i,int number)
    {
        // �e�𐶐����āA�����ʒu��ݒ�
        GameObject bullet = Instantiate(bullets[number], transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(Mathf.Cos(i),Mathf.Sin(i));
        rb.velocity = dir * bulletSpeed[number];
    }

    private void ShootWayBullet()
    {
        for(int i = 0; i<= (launchWaySpilt -1); i++)
        {
            //n-way�e�̒[����[�܂ł̊p�x
            float AngleRange = PI * (launchWayAngle / 180);
            if(AngleRange>1) _theta = (AngleRange/(launchWaySpilt- 1)) *i + 0.5f*(PI - AngleRange);
            else _theta = 0.5f* PI;

            GameObject bullet = Instantiate(bullets[1],transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            TestLineBullet testLineBullet = bullet.GetComponent<TestLineBullet>();
            testLineBullet.BulletSpeed = bulletSpeed[1];
            var bulletv = new Vector2(bulletSpeed[1] * Mathf.Cos(_theta),bulletSpeed[1] * Mathf.Sin(_theta));
            rb.velocity = bulletv;
        }
    }
}
