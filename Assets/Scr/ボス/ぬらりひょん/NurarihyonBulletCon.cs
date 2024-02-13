using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurarihyonBulletCon : MonoBehaviour
{
    private NurarihyonPushBulletCon nurarihyonPushBulletCon;
    private NewHoming newHoming;
    //弾オブジェクト
    [SerializeField]
    private GameObject[] bullets;
    //弾のスピード
    [SerializeField]
    private float[] bulletSpeed;
    [SerializeField]
    private int[] numberOfBullet;
    //各発射地点
    [SerializeField]
    private Transform[] firePoint;
    [SerializeField]
    private Transform[] homingPoint;
    [SerializeField]
    private Transform[] NewPoint;
    private int count = 0;

    private float time = 0f;
    private float radius = 0.5f;
    private int shotCount = 0;
    
    private Transform player;
    private float spiralRotationSpeed = 360f;
    private float spiralDistance = 5f;
    private List<GameObject> bullet = new List<GameObject>();
    public int ShotCount
    {
        get { return shotCount; }
        set { shotCount = value; }
    }

    public List<GameObject> Bullet
    {
        get { return bullet;}
        set { bullet = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        nurarihyonPushBulletCon = FindObjectOfType<NurarihyonPushBulletCon>();
        StartCoroutine(Atk());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //transform.Rotate(0f, 0f, 60f * Time.deltaTime);
    }


    private IEnumerator Atk()
    {
       
        while (true)
        {
            nurarihyonPushBulletCon.CounterAttack(bullets[7], this.transform, 10, 2f, 120f);
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 45; i++)
            {
                nurarihyonPushBulletCon.ClockBullet(bullets[0], bulletSpeed[0]);
                nurarihyonPushBulletCon.reClockBullet(bullets[0],bulletSpeed[0]);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < 3; i++)
            {
                nurarihyonPushBulletCon.RandomDoll(bullets[0], bulletSpeed[0], numberOfBullet[0]);
                for (int j = 0; j < 2; j++)
                {
                    StartCoroutine(nurarihyonPushBulletCon.ShootHomingBullet(homingPoint, bullets[6]));
                    yield return new WaitForSeconds(0.25f);
                }
                yield return new WaitForSeconds(0.5f);
                
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < 3; i++)
            {
                nurarihyonPushBulletCon.ApolloReflector(bullets[9], numberOfBullet[2], bulletSpeed[0], 0);
                yield return new WaitForSeconds(4.0f);
            }
            yield return new WaitForSeconds(2f);

            for (int i = 0; i < 2; i++)
            {
                nurarihyonPushBulletCon.RotaHoming(bullets[10], transform, 14, 1, 0.5f);
                yield return new WaitForSeconds(0.1f);
                for (int j = 0;j < 30; j++)
                {
                    nurarihyonPushBulletCon.ClockBullet(bullets[0], bulletSpeed[0]);
                    nurarihyonPushBulletCon.ClockBullet45(bullets[0], bulletSpeed[0]);
                    yield return new WaitForSeconds(0.1f);
                }
            }

            yield return new WaitForSeconds(2f);

            nurarihyonPushBulletCon.CounterAttack(bullets[7], this.transform, 10, 2f, 120f);
            yield return new WaitForSeconds(3f);

            for (int i = 0; i < 10; i++)
            {
                StartCoroutine(nurarihyonPushBulletCon.ShootHomingBullet(homingPoint, bullets[6]));
                yield return new WaitForSeconds(0.65f);
            }
            yield return new WaitForSeconds(2f);
            for (int i = 0;i < 3; i++)
            {
                nurarihyonPushBulletCon.ApolloReflector(bullets[3], numberOfBullet[1], bulletSpeed[0], 0);
                nurarihyonPushBulletCon.RotaHoming(bullets[10], transform, 14, 1, 0.5f);
                yield return new WaitForSeconds(5.0f);
            }

            yield return new WaitForSeconds(2.0f);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    nurarihyonPushBulletCon.ShootBullets(numberOfBullet[1], bullets[0], bulletSpeed[0], 1, 120);
                    yield return new WaitForSeconds(0.09f);
                }
                ShotCount++;
            }
            
            for(int i = 0; i < 4; i++)
            {
                nurarihyonPushBulletCon.ApolloReflector(bullets[3], numberOfBullet[1], bulletSpeed[0], 1);
                yield return new WaitForSeconds(3.0f);
                nurarihyonPushBulletCon.ApolloReflector(bullets[3], numberOfBullet[1], bulletSpeed[0], 0);
                yield return new WaitForSeconds(3.0f);
            }
            
            yield return new WaitForSeconds(2.0f);

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    nurarihyonPushBulletCon.ShootBullets(numberOfBullet[3], bullets[0], bulletSpeed[0], 2, 60);
                    yield return new WaitForSeconds(0.1f);

                }
                ShotCount++;
                UpdateSpiral();
            }
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < 60; i++)
            {
                nurarihyonPushBulletCon.ClockBullet(bullets[0], bulletSpeed[0]);
                nurarihyonPushBulletCon.ClockBullet45(bullets[0], bulletSpeed[0]);
                yield return new WaitForSeconds(0.1f);
            }

            nurarihyonPushBulletCon.CounterAttack(bullets[7], this.transform, 5, 2f, 120f);
            yield return new WaitForSeconds(3f);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    nurarihyonPushBulletCon.AllBullet(bullets[0], bulletSpeed[0], numberOfBullet[1]);
                    for (int n = 0; n < 5; n++)
                    {
                        nurarihyonPushBulletCon.fanshapeNway(player, bullets[0], bulletSpeed[1], 40f, 5);
                        yield return new WaitForSeconds(0.1f);
                    }
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(0.1f);
            }

            for (int i = 0; i < 3; i++)
            {
                nurarihyonPushBulletCon.theHoming(bullets[10], NewPoint);
                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(1f);

            nurarihyonPushBulletCon.CounterAttack(bullets[7], this.transform, 10, 2f, 120f);
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < 10; i++)
            {
                StartCoroutine(nurarihyonPushBulletCon.ShootHomingBullet(homingPoint, bullets[6]));
                yield return new WaitForSeconds(0.5f);
                yield return new WaitForSeconds(1f);
            }

            yield return null;
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < 10; i++)
            {
                nurarihyonPushBulletCon.PresenceOfEvil(firePoint[2], firePoint[3], bullets[5], bulletSpeed[0]);
                yield return new WaitForSeconds(0.5f);
                nurarihyonPushBulletCon.AllRange(bullets[0], bulletSpeed[0]);
                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < 3; i++)
            {
                nurarihyonPushBulletCon.ApolloReflector(bullets[3], numberOfBullet[1], bulletSpeed[0], 1);
                yield return new WaitForSeconds(2.0f);
            }

            nurarihyonPushBulletCon.CounterAttack(bullets[7], this.transform, 15, 2f, 120f);
            yield return new WaitForSeconds(1.0f);

            for (int i = 0; i < 15; i++)
            {
                nurarihyonPushBulletCon.AllRange(bullets[0], bulletSpeed[0]);
                yield return new WaitForSeconds(0.25f);
                nurarihyonPushBulletCon.ReAllRange(bullets[4], bulletSpeed[0]);
                yield return new WaitForSeconds(0.45f);
            }

            for (int i = 0; i < 3; i++)
            {
                nurarihyonPushBulletCon.ApolloReflector(bullets[9], numberOfBullet[1], bulletSpeed[0], 0);
                yield return new WaitForSeconds(2.0f);
            }
            yield return new WaitForSeconds(2f);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    nurarihyonPushBulletCon.fanshapeNway(player, bullets[0], bulletSpeed[0], 40f, 10);
                    yield return new WaitForSeconds(0.15f);
                }
                yield return new WaitForSeconds(0.25f);
            }

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < 45; i++)
            {
                nurarihyonPushBulletCon.ClockBullet(bullets[0], bulletSpeed[0]);
                nurarihyonPushBulletCon.ClockBullet45(bullets[0], bulletSpeed[0]);
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    nurarihyonPushBulletCon.AllBullet(bullets[0], bulletSpeed[0], numberOfBullet[1]);
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < 1; i++)
            {
                nurarihyonPushBulletCon.Reflect(bullets[1], bulletSpeed[0], numberOfBullet[1], 0);
                yield return new WaitForSeconds(0.5f);
                nurarihyonPushBulletCon.Reflect(bullets[1], bulletSpeed[0], numberOfBullet[1], 1);
                yield return new WaitForSeconds(0.5f);
                nurarihyonPushBulletCon.Reflect(bullets[1], bulletSpeed[0], numberOfBullet[1], 2);
                yield return new WaitForSeconds(0.5f);
                nurarihyonPushBulletCon.Reflect(bullets[1], bulletSpeed[0], numberOfBullet[1], 3);
                yield return new WaitForSeconds(0.5f);
                nurarihyonPushBulletCon.Reflect(bullets[1], bulletSpeed[0], numberOfBullet[1], 4);
                yield return new WaitForSeconds(0.5f);
            }

            nurarihyonPushBulletCon.SpawnCircle(bullets[2], NewPoint);
            yield return new WaitForSeconds(3.0f);

            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    nurarihyonPushBulletCon.RandomDoll(bullets[0], bulletSpeed[0], numberOfBullet[0]);
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(1f);

            nurarihyonPushBulletCon.CounterAttack(bullets[7], this.transform, 20, 2f, 120f);
            yield return new WaitForSeconds(1.0f);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    nurarihyonPushBulletCon.fanshapeNway(player, bullets[0], bulletSpeed[0], 100f, 15);
                    yield return new WaitForSeconds(0.25f);
                }

                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void UpdateSpiral()
    {
        float newRotationSpeed = Random.Range(180, 360);
        float newSpiralDistance = Random.Range(5, 10);
        float newSpeed = Random.Range(3, 5);
        int newNum = Random.Range(50,60);

        numberOfBullet[3] = newNum;
        bulletSpeed[0] = newSpeed;
        spiralRotationSpeed = newRotationSpeed;
        spiralDistance = newSpiralDistance;
    }
}
