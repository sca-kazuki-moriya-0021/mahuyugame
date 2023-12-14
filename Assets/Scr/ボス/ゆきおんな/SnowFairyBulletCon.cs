using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFairyBulletCon : MonoBehaviour
{
    private PushOnBulletCon pushOnBulletCon;

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

    //弾の発射感覚
    [SerializeField]
    private float[] fireTime;
    private int count = 0;

    private float time = 0f;

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
        pushOnBulletCon = FindObjectOfType<PushOnBulletCon>();
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
    }

    private IEnumerator Atk()
    {
        /*bool[] flag = new bool[5];
        if (flag[0] ==false && time < 5f)
        {
            flag[0] = true;
            pushOnBulletCon.ShootBulletWithCustomDirection(count,bullets[0],bulletSpeed[0]);
            pushOnBulletCon.ShootBulletWithCustomDirection(-count, bullets[0],bulletSpeed[0]);
            count++;
            Debug.Log("入ったよ");
            Debug.Log(time);
            yield return new WaitForSeconds(fireTime[0]);
        }
        count = 0;
        while(flag[0] == true && time < 30f)
        {
            flag[1] = true;
            time = Time.deltaTime;
            for (int i = 0; i < spinAngle.Length; i++)
            {
                pushOnBulletCon.ShootBarrier(spinAngle[i] + count, bullets[1], bulletSpeed[1]);
            }
            pushOnBulletCon.ShootWayBullet(launchWaySpilt, launchWayAngle, bullets[1], bulletSpeed[1]);
            count = (count + 1) * 2;
            yield return new WaitForSeconds(fireTime[1]);
        }
        count = 0;
        for (int i = 0; i < cornerPosChild.Length; i++)
        {
           pushOnBulletCon.ShootCornerMove(cornerPos, cornerPosChild[i], bullets[2]);
        }
        */

        yield return  null;
        StopCoroutine(Atk());

    }



}
