using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFairyBulletCon : MonoBehaviour
{
    [SerializeField]
    private SnowPushBulletCon pushBulletCon;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClip;

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

    //���ˊǗ��p
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
    //n�����ɕ�����邩
    [SerializeField]
    private int crystalNumberOfBullets;

    //�v���C���[�̍��W�Ɍ����킹��t���O
    private bool pPosMoveFlag = false;

    private float time = 0f;

    private GameObject player;

    private bool blizzardFlag;
    private bool testFlag;

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
        if(shuraFlag == true)
            ShuraTimeCount();

    }

    private IEnumerator Atk()
    {

        audioSource.PlayOneShot(audioClip);
        /*while (time < 10f)
        {
            pushBulletCon.ShootBulletWithCustomDirection(count,bullets[0],bulletSpeed[0]);
            pushBulletCon.ShootBulletWithCustomDirection(-count, bullets[0],bulletSpeed[0]);
            count++;
            yield return new WaitForSeconds(0.03f);
        }
        count = 0;
        while(time < 20f)
        {
            for (int i = 0; i < spinAngle.Length; i++)
                pushBulletCon.ShootBarrier(spinAngle[i] + count, bullets[1], bulletSpeed[1]*2);

            count = (count + 1) * 2;
            yield return new WaitForSeconds(0.7f);
            pushBulletCon.ShootWayBullet(launchWaySpilt, launchWayAngle, bullets[1], bulletSpeed[1]);
        }
        count = 0;

        for (int i = 0; i < cornerPosChild.Length; i++)
            pushBulletCon.ShootCornerMove(cornerPos, cornerPosChild[i], bullets[2], bulletSpeed[2]);

        yield return new WaitForSeconds(12.0f);

        while (count < 3)
        {
            pushBulletCon.SnowCrystal(crystalAngle, crystalNumberOfBullets, bullets[3], bulletSpeed[3]);
            count++;
            yield return new WaitForSeconds(3f);
        }

        count = 0;

        for(int i = 0; i < 15; i++)
        {
            pushBulletCon.ShootDemarcation(1);
            pushBulletCon.ShootDemarcation(-1);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);*/

        shuraFlag = true;
        pushBulletCon.ShuraShoot(0,1);
        pushBulletCon.ShuraShoot(1,-1);

        /*pushBulletCon.GaoukenShoot(0);
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
        yield return new WaitForSeconds(4f);*/

        //pushBulletCon.DestroyShuraShoot();
        //pushBulletCon.DestoryDemarcation();
        //shuraFlag = false;

       /* for(int i = 0 ; i < 20; i++)
        {
            pushBulletCon.GeoglyphShoot(launchWayAngle, launchWaySpilt - 14, bullets[4], bulletSpeed[4], player.transform.position);
            pushBulletCon.GeoglyphShoot(launchWayAngle, launchWaySpilt - 28, bullets[4], bulletSpeed[4], player.transform.position);
            yield return new WaitForSeconds(5f - i* 0.1f);
        }*/

        /*for(int i = 0; i< 15; i++)
        {
            pushBulletCon.DreamRealityShoot(bullets[5],transform.position);
            yield return new  WaitForSeconds(2f);
        }*/

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
