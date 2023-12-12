using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFairyBulletCon : MonoBehaviour
{
    //íeÉIÉuÉWÉFÉNÉg
    [SerializeField]
    private GameObject[] bullets;
    //íeÇÃÉXÉsÅ[Éh
    [SerializeField]
    private float[] bulletSpeed;
    //WayíeÇÃî≠éÀäpìx
    [SerializeField]
    private float launchWayAngle;
    //î≠éÀÇ∑ÇÈWayíeÇÃêî
    [SerializeField]
    private int launchWaySpilt;

    //élÇ¬äpÇÃèÍèä
    [SerializeField]
    private GameObject cornerPos;

    //âÒì]Ç∑ÇÈãÖÇÃäpìx
    [SerializeField]
    private float[] spinAngle;

    private float _theta;
    float PI= Mathf.PI;

    //íeÇÃî≠éÀä¥äo
    [SerializeField]
    private float[] fireTime;

    private float[] time = new float[3]{0f,0f,0f};
    private int count = 0;

    enum STATE
    {
        No,
        Normal,
        Skill,
        End,
    }

    private STATE state;

    // Start is called before the first frame update
    void Start()
    {
        state = STATE.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        time[0] += Time.deltaTime;
        time[1] += Time.deltaTime;
        time[2] += Time.deltaTime;


        if(time[0] > fireTime[0])
        {
            //ShootWayBullet();
            //ShootCornerMove(2);
            //ShootBulletWithCustomDirection(count,0);
            //ShootBulletWithCustomDirection(-count,0);
            time[0] = 0;
        }

        if(time[1] > fireTime[1])
        {
            for(int i = 0; i < spinAngle.Length; i++)
            {
                //ShootBarrier(spinAngle[i] + count,1);
            }
            count  = count + 1 * 2 ;
            time[1] = 0;
        }

        if(time[2] > fireTime[2])
        {
            ShootCornerMove(2);
            time[2] = 0;
        }
    }

    private void ShootBulletWithCustomDirection(int i,int number)
    {
        // íeÇê∂ê¨ÇµÇƒÅAèâä˙à íuÇê›íË
        GameObject bullet = Instantiate(bullets[number], transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(Mathf.Cos(i),Mathf.Sin(i));
        rb.velocity = dir * bulletSpeed[number];
    }

    private void ShootWayBullet()
    {
        for(int i = 0; i< launchWaySpilt; i++)
        {
            //n-wayíeÇÃí[Ç©ÇÁí[Ç‹Ç≈ÇÃäpìx
            float AngleRange = PI * (launchWayAngle / 180);
            if(AngleRange>1) _theta = (AngleRange/(launchWaySpilt)) *i + 0.5f*(PI - AngleRange);
            else _theta = 0.5f* PI;

            GameObject bullet = Instantiate(bullets[1],transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            TestLineBullet testLineBullet = bullet.GetComponent<TestLineBullet>();
            testLineBullet.BulletSpeed = bulletSpeed[1];
            var bulletv = new Vector2(bulletSpeed[1] * Mathf.Cos(_theta),bulletSpeed[1] * Mathf.Sin(_theta));
            rb.velocity = bulletv;
        }
    }

    private void ShootBarrier(float angle,int number)
    {
        angle = angle * Mathf.Deg2Rad;
        GameObject bullet = Instantiate(bullets[number], transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rb.velocity = dir * bulletSpeed[number];
    }

    private void ShootCornerMove(int number)
    {
        GameObject bullet = Instantiate(bullets[number],transform.position , Quaternion.identity);
        CornerMoveBullet cornerMoveBullet = bullet.GetComponent<CornerMoveBullet>();
        cornerMoveBullet.CornerObject = cornerPos;
    }

}
