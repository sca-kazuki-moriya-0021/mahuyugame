using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFairyBulletCon : MonoBehaviour
{
    //弾オブジェクト
    [SerializeField]
    private GameObject[] bullets;
    //弾のスピード
    [SerializeField]
    private float[] bulletSpeed;
    //Way弾の発射角度
    [SerializeField]
    private float launchWayAngle;
    //発射するWay弾の数
    [SerializeField]
    private int launchWaySpilt;

    //一回だけ入るフラグ
    private bool shootFlag;

    //四つ角の場所
    [SerializeField]
    private GameObject cornerPos;
    private GameObject[] cornerPosChild = new GameObject[4];

    //回転する球の角度
    [SerializeField]
    private float[] spinAngle;

    private float _theta;
    float PI= Mathf.PI;

    //弾の発射感覚
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
        for (int i = 0; i < cornerPosChild.Length; i++)
        {
            cornerPosChild[i] = cornerPos.transform.GetChild(i).gameObject;
        }

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

        if(time[2] > fireTime[2] && shootFlag == false)
        {
            shootFlag = true;
            for(int i = 0; i < cornerPosChild.Length; i++)
            {
                ShootCornerMove(2, i);
            }
        }
    }

    private void ShootBulletWithCustomDirection(int i,int number)
    {
        // 弾を生成して、初期位置を設定
        GameObject bullet = Instantiate(bullets[number], transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(Mathf.Cos(i),Mathf.Sin(i));
        rb.velocity = dir * bulletSpeed[number];
    }

    //拡散弾
    private void ShootWayBullet()
    {
        for(int i = 0; i< launchWaySpilt; i++)
        {
            //n-way弾の端から端までの角度
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

    //回転弾
    private void ShootBarrier(float angle,int number)
    {
        angle = angle * Mathf.Deg2Rad;
        GameObject bullet = Instantiate(bullets[number], transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rb.velocity = dir * bulletSpeed[number];
    }

    //四隅移動弾発射
    private void ShootCornerMove(int number, int pos)
    {
        GameObject bullet = Instantiate(bullets[number],cornerPosChild[pos].transform.position, Quaternion.identity);
        CornerMoveBullet cornerMoveBullet = bullet.GetComponent<CornerMoveBullet>();
        cornerMoveBullet.CornerObject = cornerPos;
        cornerMoveBullet.InitializationPos = cornerPosChild[pos].transform.position;
    }



}
