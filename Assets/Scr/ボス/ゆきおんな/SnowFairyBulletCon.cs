using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFairyBulletCon : MonoBehaviour
{
    private SnowPushBulletCon pushOnBulletCon;

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

    //四つ角の場所
    [SerializeField]
    private GameObject cornerPos;
    private GameObject[] cornerPosChild = new GameObject[4];

    //回転する球の角度
    [SerializeField]
    private float[] spinAngle;

    //弾の発射感覚
    [SerializeField]
    private float[] fireTime;
    private int count = 0;

    //自分の弾を消すフラグ
    private bool[] bulletDeleteFlag = new bool[2];

    //減速フラグ
    private bool reduceSpeedFlag = false;

    //修羅剣用コライダー
    [SerializeField]
    private GameObject shuraCenterObj;
    private GameObject centerObj;
    private bool shuraFlag;

    //雪の結晶弾の角度
    [SerializeField]
    private float crystalAngle;
    //雪の結晶弾の出始めに何個出すか
    [SerializeField]
    private int crystalShoots;
    //n方向に分かれるか
    [SerializeField]
    private int crystalNumberOfBullets;

    //プレイヤーの座標に向かわせるフラグ
    private bool pPosMoveFlag = false;

    private float time = 0f;
    private float shuraTime = 0;

    private GameObject player;

    public bool[] BulletDeleteFlag
    {
        get { return this.bulletDeleteFlag; }
        set { this.bulletDeleteFlag = value; }
    }

    public bool ReduceSpeedFlag
    {
        get { return this.reduceSpeedFlag; }
        set { this.reduceSpeedFlag = value; }
    }

    public bool PPosMoveFlag
    {
        get { return this.pPosMoveFlag; }
        set { this.pPosMoveFlag = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        pushOnBulletCon = FindObjectOfType<SnowPushBulletCon>();
        player = GameObject.Find("Player");
        for (int i = 0; i < cornerPosChild.Length; i++)
        {
            cornerPosChild[i] = cornerPos.transform.GetChild(i).gameObject;
        }

        StartCoroutine(Atk());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(shuraFlag == true)
            ShuraTimeCount();
        
        if(time > 30f)
        {
            bulletDeleteFlag[0] = true;
        }

        if(time > 10f)
        {
            reduceSpeedFlag = true;
        }

        if(reduceSpeedFlag == true && time > 20f)
        {
            reduceSpeedFlag = false;
        }
    }

    private IEnumerator Atk()
    {
        /*while (time < 5f)
        {
            pushOnBulletCon.ShootBulletWithCustomDirection(count,bullets[0],bulletSpeed[0]);
            pushOnBulletCon.ShootBulletWithCustomDirection(-count, bullets[0],bulletSpeed[0]);
            count++;
            yield return new WaitForSeconds(fireTime[0]);
        }*/
        count = 0;
        /*while(time < 15f)
        {
            for (int i = 0; i < spinAngle.Length; i++)
            {
                pushOnBulletCon.ShootBarrier(spinAngle[i] + count, bullets[1], bulletSpeed[1]*2);
            }
            count = (count + 1) * 2;
            yield return new WaitForSeconds(fireTime[1]);
            pushOnBulletCon.ShootWayBullet(launchWaySpilt, launchWayAngle, bullets[1], bulletSpeed[1]);
        }*/
        count = 0;

        //for (int i = 0; i < cornerPosChild.Length; i++)
        {
            //pushOnBulletCon.ShootCornerMove(cornerPos, cornerPosChild[i], bullets[2], bulletSpeed[2]);
        }

        /*while (count <100)
        {
            for (int i = 0; i < crystalShoots; i++)
            {
                pushOnBulletCon.SnowCrystal(crystalAngle, crystalNumberOfBullets, bullets[3], bulletSpeed[3]);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(fireTime[2]);
            count++;
        }*/

        /*for(int i = 0; i < 15; i++)
        {
            pushOnBulletCon.ShootDemarcation(1);
            pushOnBulletCon.ShootDemarcation(-1);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);*/

        /*shuraFlag = true;
        //pushOnBulletCon.ShuraShoot(0,1);
        //pushOnBulletCon.ShuraShoot(1,-1);

        for(int i= 0; i< 4; i++)
        {
            pushOnBulletCon.GaoukenShoot(0);
            yield return new WaitForSeconds(0.1f);
            pushOnBulletCon.GaoukenShoot(1);
        }

        pushOnBulletCon.DestroyShuraShoot();
        pushOnBulletCon.DestoryDemarcation();
        shuraFlag = false;*/

        pushOnBulletCon.GeoglyphShoot(launchWaySpilt, launchWayAngle, bullets[4], bulletSpeed[4],player.transform.position);

        yield return  null;
        StopCoroutine(Atk());

    }

    private void ShuraTimeCount()
    {
        shuraTime += Time.deltaTime;

        if (shuraTime > 3f)
        {
            if (centerObj == null)
                centerObj = Instantiate(shuraCenterObj, new Vector3(0, 0, 0), Quaternion.identity);
        }

        if (centerObj != null && shuraTime > 6f)
        {
            Destroy(centerObj);
            pPosMoveFlag = true;
        }

        if (pPosMoveFlag == true && shuraTime > 9f)
        {
            pPosMoveFlag = false;
            shuraTime = 0f;
        }
    }

}
