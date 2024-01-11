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

    //�C�����p�R���C�_�[
    [SerializeField]
    private GameObject shuraCenterObj;
    private GameObject centerObj = null;

    private bool shuraFlag;
    private float shuraTime = 0;

    //��̌����e�̊p�x
    [SerializeField]
    private float crystalAngle;
    //��̌����e�̏o�n�߂ɉ��o����
    [SerializeField]
    private int crystalShoots;
    //n�����ɕ�����邩
    [SerializeField]
    private int crystalNumberOfBullets;


    //�v���C���[�̍��W�Ɍ����킹��t���O
    private bool pPosMoveFlag = false;

    private float time = 0f;

    private GameObject player;

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
        //count = 0;
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
        //count = 0;

        //for (int i = 0; i < cornerPosChild.Length; i++)
        {
            //ShootCornerMove(cornerPos, cornerPosChild[i], bullets[2], bulletSpeed[2]);
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

        //shuraFlag = true;
        //pushOnBulletCon.ShuraShoot(0,1);
        //pushOnBulletCon.ShuraShoot(1,-1);

       /*for(int i = 0; i < 2 ; i++)
       {
            pushOnBulletCon.GaoukenShoot(0);
            yield return new WaitForSeconds(2f);
            pushOnBulletCon.GaoukenShoot(1);
            yield return new WaitForSeconds(2f);
            pushOnBulletCon.GaoukenShoot(2);
            yield return new WaitForSeconds(2f);
            pushOnBulletCon.GaoukenShoot(3);
            yield return new WaitForSeconds(2f);
       }*/


        //pushOnBulletCon.DestroyShuraShoot();
        //pushOnBulletCon.DestoryDemarcation();
        //shuraFlag = false;
        /*for(int i = 0 ; i < 20; i++)
        {
            pushOnBulletCon.GeoglyphShoot(launchWayAngle, launchWaySpilt - 14, bullets[4], bulletSpeed[4], player.transform.position);
            pushOnBulletCon.GeoglyphShoot(launchWayAngle, launchWaySpilt - 28, bullets[4], bulletSpeed[4], player.transform.position);
            yield return new WaitForSeconds(2f - i* 0.1f);
        }*/

        for(int i = 0; i< 10; i++)
        {
            pushOnBulletCon.DreamRealityShoot(bullets[5],transform.position);
            yield return new  WaitForSeconds(2f);
        }

      
        yield return  null;
        StopCoroutine(Atk());
    }

    private void ShuraTimeCount()
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
    }

}
