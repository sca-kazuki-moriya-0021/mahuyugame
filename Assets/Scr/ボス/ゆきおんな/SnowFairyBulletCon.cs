using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFairyBulletCon : MonoBehaviour
{
    private SnowPushBulletCon pushOnBulletCon;

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

    //�l�p�̏ꏊ
    [SerializeField]
    private GameObject cornerPos;
    private GameObject[] cornerPosChild = new GameObject[4];

    //��]���鋅�̊p�x
    [SerializeField]
    private float[] spinAngle;

    //�e�̔��ˊ��o
    [SerializeField]
    private float[] fireTime;
    private int count = 0;

    //�����̒e�������t���O
    private bool[] bulletDeleteFlag = new bool[2];

    //�����t���O
    private bool reduceSpeedFlag = false;

    //�C�����p�R���C�_�[
    [SerializeField]
    private GameObject shuraCenterObj;
    private GameObject centerObj;
    private bool shuraFlag;

    //�v���C���[�̍��W�Ɍ����킹��t���O
    private bool pPosMoveFlag = false;

    private float time = 0f;
    private float shuraTime = 0;


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

        for(int i = 0; i < 15; i++)
        {
            pushOnBulletCon.ShootDemarcation(1);
            pushOnBulletCon.ShootDemarcation(-1);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);

        shuraFlag = true;
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
        shuraFlag = false;

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
