using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class SnowFairyBulletCon : MonoBehaviour
{
    [SerializeField]
    private SnowPushBulletCon pushBulletCon;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClip;

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

    //発射管理用
    private int count = 0;

    //修羅剣用コライダー
    //[SerializeField]
    //private GameObject shuraCenterObj;
    private GameObject centerObj = null;

    private bool shuraFlag;
    private float shuraTime = 0;

    //雪の結晶弾の角度
    [SerializeField]
    private float crystalAngle;
    //n方向に分かれるか
    [SerializeField]
    private int crystalNumberOfBullets;

    //プレイヤーの座標に向かわせるフラグ
    private bool pPosMoveFlag = false;

    private float time = 0f;

    private GameObject player;

    //パーティクル変更
    private bool blizzardFlag;

    //ライト取得用
    [SerializeField]
    private Light2D stageGlobalLight;

    public bool PPosMoveFlag
    {
        get { return this.pPosMoveFlag; }
        set { this.pPosMoveFlag = value; }
    }

    public bool BlizzardFlag { get => blizzardFlag; set => blizzardFlag = value; }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < cornerPosChild.Length; i++)
            cornerPosChild[i] = cornerPos.transform.GetChild(i).gameObject;

        player = GameObject.Find("Player");
        StartCoroutine(Atk());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //if(shuraFlag == true)
            //ShuraTimeCount();

    }

    private IEnumerator Atk()
    {


        while (time < 15f)
        {
            pushBulletCon.ShootBulletWithCustomDirection(count,bullets[0],bulletSpeed[0]);
            pushBulletCon.ShootBulletWithCustomDirection(-count, bullets[0],bulletSpeed[0]);
            count++;
            yield return new WaitForSeconds(0.03f);
        }
        count = 0;
        while(time < 30f)
        {
            for (int i = 0; i < spinAngle.Length; i++)
                pushBulletCon.ShootBarrier(spinAngle[i] + count, bullets[1], bulletSpeed[1]*2);

            count = (count + 1) * 2;
            yield return new WaitForSeconds(0.7f);
            pushBulletCon.ShootWayBullet(launchWaySpilt, launchWayAngle, bullets[1], bulletSpeed[1]);
        }
        count = 0;

        audioSource.PlayOneShot(audioClip);
        shuraFlag = true;
        pushBulletCon.ShuraShoot(0,1);
        pushBulletCon.ShuraShoot(1,-1);

        yield return new WaitForSeconds(10f);

        for (int i = 0; i < cornerPosChild.Length; i++)
            pushBulletCon.ShootCornerMove(cornerPos, cornerPosChild[i], bullets[2], bulletSpeed[2]);

        yield return new WaitForSeconds(12.0f);

       pushBulletCon.SnowCrystal(crystalAngle, crystalNumberOfBullets, bullets[3], bulletSpeed[3]);
       count++;
       yield return new WaitForSeconds(3f);

        count = 0;

        /*for(int i = 0; i < 15; i++)
        {
            pushBulletCon.ShootDemarcation(1);
            pushBulletCon.ShootDemarcation(-1);
            yield return new WaitForSeconds(0.5f);
        }*/
        yield return new WaitForSeconds(0.5f);


        pushBulletCon.DestroyShuraShoot();


        pushBulletCon.GaoukenShoot(0);
        yield return new WaitForSeconds(4f);
        pushBulletCon.GaoukenShoot(1);
        yield return new WaitForSeconds(4f);
        pushBulletCon.GaoukenShoot(2);
        yield return new WaitForSeconds(4f);
        pushBulletCon.GaoukenShoot(3);
        yield return new WaitForSeconds(4f);
        pushBulletCon.GaoukenShoot(4);
        yield return new WaitForSeconds(4f);
        pushBulletCon.GaoukenShoot(5);
        yield return new WaitForSeconds(4f);


        pushBulletCon.DestoryDemarcation();
        shuraFlag = false;

       for(int i = 0 ; i < 5; i++)
        {
            pushBulletCon.GeoglyphShoot(launchWayAngle, launchWaySpilt - 14, bullets[4], bulletSpeed[4], player.transform.position);
            pushBulletCon.GeoglyphShoot(launchWayAngle, launchWaySpilt - 28, bullets[4], bulletSpeed[4], player.transform.position);
            yield return new WaitForSeconds(3f);
        }

        //パーティクル変更
        blizzardFlag = true;
        yield return new WaitForSeconds(0.4f);
        audioSource.PlayOneShot(audioClip);
        //グローバルライト変更
        stageGlobalLight.color = new Color(0, 0, 0);

        audioSource.PlayOneShot(audioClip);
        for (int i = 0; i< 10; i++)
        {
            pushBulletCon.DreamRealityShoot(bullets[5],transform.position);
            yield return new  WaitForSeconds(2f);
        }

        yield return  null;
        StopCoroutine(Atk());
    }

    /*private void ShuraTimeCount()
    {
        shuraTime += Time.deltaTime;
        
        if (shuraTime > 3f && centerObj == null && pPosMoveFlag == false)
           centerObj = Instantiate(shuraCenterObj, new Vector3(0, 0, 0), Quaternion.identity);

        if (centerObj != null && shuraTime > 6f && pPosMoveFlag == false)
        {
            Destroy(centerObj);
            pPosMoveFlag = true;
        }

        if (pPosMoveFlag == true && shuraTime > 9f)
        {
            pPosMoveFlag = false;
            shuraTime = 0f;
        }
    }*/
}
