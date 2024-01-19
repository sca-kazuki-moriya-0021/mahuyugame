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
    //弾の発射感覚
    [SerializeField]
    private float[] fireTime;
    [SerializeField]
    private Transform[] firePoint;
    private int count = 0;

    private float time = 0f;
    private float radius = 0.5f; 
    private List<GameObject> homing = new List<GameObject>();
    private Transform player;
    public List<GameObject> Homing
    {
        get { return homing;}
        set { homing = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        nurarihyonPushBulletCon = FindObjectOfType<NurarihyonPushBulletCon>();
        newHoming = FindObjectOfType<NewHoming>();
        StartCoroutine(Atk());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }


    private IEnumerator Atk()
    {
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                nurarihyonPushBulletCon.ApolloReflector(bullets[3], numberOfBullet[0], bulletSpeed[0], radius);
                yield return new WaitForSeconds(1.0f);
            }
            for (int i = 0;i < 2; i++)
            {
                nurarihyonPushBulletCon.AllBullet(bullets[0], bulletSpeed[0], numberOfBullet[0]);
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
            nurarihyonPushBulletCon.SpawnCircle(bullets[2],firePoint);
            for(int i = 0; i < 14; i++)
            {
                for(int j = 0; j < 2;j++)
                {
                    nurarihyonPushBulletCon.RandomDoll(bullets[0], bulletSpeed[0], numberOfBullet[0]);
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(0.5f);
            }
            
            for(int i = 0;i < 30; i++)
            {
                for(int j = 0;j < 2; j++)
                {
                    nurarihyonPushBulletCon.fanshapeNway(player, bullets[0], bulletSpeed[0], 30f, 5);
                    yield return new WaitForSeconds(0.25f);
                }
                
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(0.5f);
            yield return null;
        }
    }
}
