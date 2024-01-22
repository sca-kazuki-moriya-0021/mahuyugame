using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurarihyonBulletCon : MonoBehaviour
{
    private NurarihyonPushBulletCon nurarihyonPushBulletCon;
    private NewHoming newHoming;
    //�e�I�u�W�F�N�g
    [SerializeField]
    private GameObject[] bullets;
    //�e�̃X�s�[�h
    [SerializeField]
    private float[] bulletSpeed;
    [SerializeField]
    private int[] numberOfBullet;
    //�e�̔��ˊ��o
    [SerializeField]
    private float[] fireTime;
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
    private float spiralRotationSpeed = 360f;
    private float spiralDistance = 5f;
    private Transform player;

    public int ShotCount
    {
        get { return shotCount; }
        set { shotCount = value; }
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
        transform.Rotate(0f, 0f, 60f * Time.deltaTime);
    }


    private IEnumerator Atk()
    {
        while (true)
        {
            for(int i = 0; i < 20; i++)
            {
                for(int j = 0;j < 10; j++)
                {
                    nurarihyonPushBulletCon.ShootBullets(numberOfBullet[0], bullets[0], bulletSpeed[0], 2, 60);
                    yield return new WaitForSeconds(0.1f);
                    
                }
                ShotCount++;
                UpdateSpiral();
            }
            yield return new WaitForSeconds(1f);
            for (int i = 0;i< 30; i++)
            {
                nurarihyonPushBulletCon.ClockBullet(bullets[0], bulletSpeed[0]);
                nurarihyonPushBulletCon.ClockBullet45(bullets[0], bulletSpeed[0]);
                yield return new WaitForSeconds(0.1f);
            }
            nurarihyonPushBulletCon.CounterAttack(bullets[7], this.transform, 10, 2f, 120f);
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
           
            for (int i = 0;i< 3; i++)
            {
                nurarihyonPushBulletCon.theHoming(bullets[8], NewPoint);
                yield return new WaitForSeconds(0.5f);
            }
           
            for (int i = 0; i < 10; i++) 
            {
                StartCoroutine(nurarihyonPushBulletCon.ShootHomingBullet(homingPoint, bullets[6]));
                yield return new WaitForSeconds(1f);
            }
            yield return null;
            for (int i = 0; i < 10; i++)
            {  
                nurarihyonPushBulletCon.PresenceOfEvil(firePoint[2], firePoint[3], bullets[5], bulletSpeed[0]);
                yield return new WaitForSeconds(0.5f);
            }
            
            for (int i = 0; i < 3; i++)
            {
                nurarihyonPushBulletCon.ApolloReflector(bullets[3], numberOfBullet[1], bulletSpeed[0], radius);
                yield return new WaitForSeconds(2.0f);
            }
            for (int i = 0; i < 15; i++)
            {
                nurarihyonPushBulletCon.AllRange(bullets[0], bulletSpeed[0]);
                yield return new WaitForSeconds(0.45f);
                nurarihyonPushBulletCon.ReAllRange(bullets[4], bulletSpeed[0]);
                yield return new WaitForSeconds(0.45f);
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    nurarihyonPushBulletCon.fanshapeNway(player, bullets[0], bulletSpeed[0], 40f, 10);
                    yield return new WaitForSeconds(0.15f);
                }
                yield return new WaitForSeconds(0.25f);
            }
            for (int i = 0;i < 5; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    nurarihyonPushBulletCon.AllBullet(bullets[0], bulletSpeed[0], numberOfBullet[1]);
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < 1; i++)
            {
                nurarihyonPushBulletCon.Reflect(bullets[1], bulletSpeed[0], numberOfBullet[1],0);
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
            nurarihyonPushBulletCon.SpawnCircle(bullets[2], firePoint);
            yield return new WaitForSeconds(1.0f);
            for(int i = 0; i < 14; i++)
            {
                for(int j = 0; j < 2;j++)
                {
                    nurarihyonPushBulletCon.RandomDoll(bullets[0], bulletSpeed[0], numberOfBullet[0]);
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(0.5f);
            }
            
            yield return null;
        }
    }

    private void UpdateSpiral()
    {
        float newRotationSpeed = Random.Range(180, 450);
        float newSpiralDistance = Random.Range(1f, 20f);
        float newSpeed = Random.Range(2, 6);

        bulletSpeed[0] = newSpeed;
        spiralRotationSpeed = newRotationSpeed;
        spiralDistance = newSpiralDistance;
    }
}
