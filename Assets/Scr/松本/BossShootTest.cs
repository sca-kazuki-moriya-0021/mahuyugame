using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    [SerializeField, Header("連射数")]
    private float[] numberOfShots;
    [SerializeField, Header("放出角度")]
    private float[] spreadAngle;
    [SerializeField, Header("発射間隔")]
    private float[] fireTime;
    [SerializeField,Header("回転速度")]
    private float[] rotationSpeed;
    [SerializeField,Header("ホーミング用")]
    private GameObject[] firePoint;
    //Way弾幕
    private int bulletAmount;
    private float maxbulletSpacing;
    private float createBullet;
    private float timeAngle;
    private float yimespreadAngle;
    private float bulletsTime;
    private int maxBullet;
    private Transform player;

    //AllspiralLauncher
    private int shotCount;
    private float spiralRotationSpeed = 360f;
    private float spiralDistance = 5f;

    //SistersLauncher
    private float subTimer = 0;
    private float timer = 0;
    private float coolTimer = 0;
    private float currentRotation = 0;
    private bool rotationFlag = true;

    private List<Rigidbody2D> bulletsList = new List<Rigidbody2D>();

    public int ShotCount
    {
        get { return shotCount; }
        set { shotCount = value; }
    }

   
    public List<Rigidbody2D> BulletsList
    {
        get { return bulletsList; }
        set { bulletsList = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        bossSkillTest = FindObjectOfType<BossSkillTest>();
        player = GameObject.FindWithTag("Player").transform;
        //StartCoroutine(AllspiralLauncher());
        //StartCoroutine(RandomDollLauncher());
        //StartCoroutine(AllBullets());
        //StartCoroutine(HomingLauncher());
        //StartCoroutine(AllRandomLauncher());
        //StartCoroutine(RandomLauncher());
        StartCoroutine(AllRangeLauncher());

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > fireTime[2])
        {
            timer = 0f;
        }
        //subTimer += Time.deltaTime;
        //if(currentRotation >= 360f)
        //{
        //    coolTimer += Time.deltaTime;
        //    if(coolTimer >= 1)
        //    {
        //        currentRotation = 0f;
        //        coolTimer = 0f;

        //        rotationFlag = !rotationFlag;
        //    }
        //}
        //else
        //{
        //    if(timer >= fireTime[3])
        //    {
        //        bossSkillTest.RotateBullet(bulletPrefabs[1], bulletSpeed[1]);
        //        timer = 0f;
        //    }
        //    if(subTimer >= fireTime[2])
        //    {
        //        bossSkillTest.SpreasBullet(spreadAngle[0], subBulletPrefabs[0], subBulletSpeed[0], subBulletSpeed[1]);
        //        subTimer = 0f;
        //    }
        //}
        //float rotationDirection = rotationFlag ? 1 : -1;
        //transform.Rotate(0f, 0f, rotationSpeed[1] * Time.deltaTime * rotationDirection);
        //currentRotation += rotationSpeed[1] * Time.deltaTime;
        //bulletsTime += Time.deltaTime;
        //if(bulletsTime >= fireTime[0])
        //{
        //    bossSkillTest.Launcher(spreadAngle[0], numberOfBullets[0], fireTime[0], maxbulletSpacing, player, bulletPrefabs[0], bulletSpeed[0]);
        //    bulletsTime = 0f;
        //}
    }

    private void FixedUpdate()
    {
        transform.Rotate(0f, 0f, rotationSpeed[1] * Time.deltaTime);
    }

    private IEnumerator AllRangeLauncher()
    {
        while(true)
        {
            bossSkillTest.ShootBullets(bulletPrefabs[0], bulletSpeed[0]);
            yield return new WaitForSeconds(0.25f);
            bossSkillTest.ShootReBullets(subBulletPrefabs[0], subBulletSpeed[0]);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator RandomLauncher()
    {
        while (true)
        {
            for (int i = 0; i < numberOfShots[0]; i++)
           {
                bossSkillTest.RandomLauncher(spreadAngle[1],numberOfBullets[0],bulletPrefabs[0],bulletSpeed[1]);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(fireTime[1]);
        }
    }
    private IEnumerator AllRandomLauncher()
    {
        while(true)
        {
            bulletsList.Clear();

            for(int i = 0;i < numberOfShots[1]; i++)
            {
                bossSkillTest.AllRandomLauncher(spreadAngle[1], numberOfBullets[0], bulletPrefabs[0], bulletSpeed[0]);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(fireTime[2]);

            bossSkillTest.StopBullets();

            yield return new WaitForSeconds(0.1f);

            bossSkillTest.MoveBulletsRandomly(bulletSpeed[2]);
        }
    }

    private IEnumerator RandomDollLauncher()
    {
        while (true)
        {
            for (int i = 0; i < 14; i++)
            {
                bossSkillTest.ShootBullet(numberOfBullets[0], bulletPrefabs[0], bulletSpeed[0]);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(fireTime[0]);
        }
    }

    private IEnumerator AllspiralLauncherd()
    {
        while (true)
        {
            for (int i = 0; i < numberOfShots[0]; i++)
            {
                Debug.Log("a");
                // 引数を追加してShootBulletsメソッドを呼び出し
                bossSkillTest.ShootBullets(numberOfBullets[0], bulletPrefabs[0], bulletSpeed[0], spiralDistance, spiralRotationSpeed);
                yield return new WaitForSeconds(fireTime[0]);
            }

            ShotCount++;
            yield return new WaitForSeconds(fireTime[1]);

            // 引数を追加してUpdateSpiralメソッドを呼び出し
            UpdateSpiral();
        }
    }

    private IEnumerator HomingLauncher()
    {
        while (true)
        {
            bossSkillTest.ShootHomingBullet(firePoint[0], bulletPrefabs[2]);
            bossSkillTest.ShootHomingBullet(firePoint[1], bulletPrefabs[2]);
            yield return new WaitForSeconds(fireTime[0]);
        }
    }

    private void UpdateSpiral()
    {
        float newRotationSpeed = Random.Range(180, 450);
        float newSpiralDistance = Random.Range(5f, 10f);
        float newSpeed = Random.Range(3, 7);

        bulletSpeed[0] = newSpeed;
        spiralRotationSpeed = newRotationSpeed;
        spiralDistance = newSpiralDistance;
    }
}
