using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootTest : MonoBehaviour
{
    private BossSkillTest bossSkillTest;
    [SerializeField, Header("メイン弾")]
    private GameObject[] bulletPrefabs;
    [SerializeField, Header("サブ弾")]
    private GameObject[] subBulletPrefabs;
    [SerializeField, Header("弾速")]
    private float[] bulletSpeed;
    [SerializeField, Header("サブ弾速")]
    private float[] subBulletSpeed;
    [SerializeField, Header("弾数")]
    private int[] numberOfBullets;
    [SerializeField, Header("放出角度")]
    private float[] spreadAngle;
    [SerializeField, Header("発射間隔")]
    private float[] fireTime;


    //Way弾幕
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

    //AllspiralLauncher
    private int ShotCount;
    private float spiralRotationSpeed;
    private float spiralDistance;
    // Start is called before the first frame update
    void Start()
    {
        bossSkillTest = FindObjectOfType<BossSkillTest>();
        player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(ShootMultiSpread());
        //StartCoroutine(GenerateBullets());

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

    private IEnumerator ShootMultiSpread()
    {
        while (true)
        {
            for (int i = 0; i < numberOfShots; i++)
            {
                // 引数を追加してShootBulletsメソッドを呼び出し
                bossSkillTest.ShootBullets(numberOfBullets[1], bulletPrefabs[1], bulletSpeed[1], spiralDistance, spiralRotationSpeed, ShotCount);
                yield return new WaitForSeconds(fireTime[1]);
            }

            bossSkillTest.ShotCount++;
            yield return new WaitForSeconds(fireTime[1]);

            // 引数を追加してUpdateSpiralメソッドを呼び出し
            UpdateSpiral();
        }
    }

    private void UpdateSpiral()
    {
        float newRotationSpeed = Random.Range(180, 450);
        float newSpiralDistance = Random.Range(5f, 10f);
        float newSpeed = Random.Range(3, 7);

        bulletSpeed[1] = newSpeed;
        spiralRotationSpeed = newRotationSpeed;
        spiralDistance = newSpiralDistance;
    }
}
