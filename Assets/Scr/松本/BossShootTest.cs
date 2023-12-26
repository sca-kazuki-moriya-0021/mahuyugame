using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootTest : MonoBehaviour
{
    private BossSkillTest bossSkillTest;
    [SerializeField, Header("���C���e")]
    private GameObject[] bulletPrefabs;
    [SerializeField, Header("�T�u�e")]
    private GameObject[] subBulletPrefabs;
    [SerializeField, Header("�e��")]
    private float[] bulletSpeed;
    [SerializeField, Header("�T�u�e��")]
    private float[] subBulletSpeed;
    [SerializeField, Header("�e��")]
    private int[] numberOfBullets;
    [SerializeField, Header("���o�p�x")]
    private float[] spreadAngle;
    [SerializeField, Header("���ˊԊu")]
    private float[] fireTime;


    //Way�e��
    private int bulletAmount;
    private float maxbulletSpacing;
    private float createBullet;
    private float timeAngle;
    private float yimespreadAngle;
    private float bulletsTime;
    private int maxBullet;
    private Transform player;
    //tokei
    private float rotationSpeed = 60f;

    //RandomLauncher
    private int numberOfShots = 3;
    // Start is called before the first frame update
    void Start()
    {
        bossSkillTest = FindObjectOfType<BossSkillTest>();
        player = GameObject.FindWithTag("Player").transform;
        //StartCoroutine(ShootMultiSpread());
        StartCoroutine(GenerateBullets());
    }

    // Update is called once per frame
    void Update()
    {
        //bulletsTime += Time.deltaTime;
        //if(bulletsTime >= fireTime[0])
        //{
        //    bossSkillTest.Launcher(spreadAngle[0], numberOfBullets[0], fireTime[0], maxbulletSpacing, player, bulletPrefabs[0], bulletSpeed[0]);
        //    bulletsTime = 0f;
        //}
    }

    //IEnumerator ShootMultiSpread()
    //{
    //    while (true)
    //    {
    //        for (int i = 0; i < numberOfShots; i++)
    //        {
    //            bossSkillTest.RandomLauncher(spreadAngle[1],numberOfBullets[1],bulletPrefabs[0],bulletSpeed[1]);
    //            yield return new WaitForSeconds(0.1f);
    //        }
    //        yield return new WaitForSeconds(fireTime[1]);
    //    }
    //}
    private IEnumerator GenerateBullets()
    {
        while (true)
        {
            for (int i = 0; i < 14; i++)
            {
                bossSkillTest.ShootBullet(numberOfBullets[2], bulletPrefabs[0], bulletSpeed[2]);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(fireTime[2]);
        }
    }
}
