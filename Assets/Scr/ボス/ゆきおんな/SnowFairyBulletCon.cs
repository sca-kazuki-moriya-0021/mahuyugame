using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE
{
    No,
    Normal,
    Skill,
    End,
}

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

    private float time = 0f;

    private STATE state;

    public bool[] BulletDeleteFlag
    {
        get { return this.bulletDeleteFlag; }
        set { this.bulletDeleteFlag = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        pushOnBulletCon = FindObjectOfType<SnowPushBulletCon>();
        for (int i = 0; i < cornerPosChild.Length; i++)
        {
            cornerPosChild[i] = cornerPos.transform.GetChild(i).gameObject;
        }

        state = STATE.Normal;

        StartCoroutine(Atk());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //Debug.Log(time);

        if(time > 30f)
        {
            bulletDeleteFlag[0] = true;
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

        //pushOnBulletCon.ShootDemarcation(launchWaySpilt, launchWayAngle, bullets[3], bulletSpeed[3]);
        //pushOnBulletCon.ShootDemarcation(launchWaySpilt, launchWayAngle, bullets[3], bulletSpeed[3]);

        yield return  null;
        StopCoroutine(Atk());

    }



}
