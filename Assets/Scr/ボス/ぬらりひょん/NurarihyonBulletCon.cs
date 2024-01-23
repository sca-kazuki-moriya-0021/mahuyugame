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
            nurarihyonPushBulletCon.ApolloReflector(bullets[3], numberOfBullet[1], bulletSpeed[0], radius);
            yield return new WaitForSeconds(2.0f);
            
            
            yield return null;
        }
    }

    private void UpdateSpiral()
    {
        float newRotationSpeed = Random.Range(180, 360);
        float newSpiralDistance = Random.Range(1, 5);
        float newSpeed = Random.Range(2, 6);

        bulletSpeed[0] = newSpeed;
        spiralRotationSpeed = newRotationSpeed;
        spiralDistance = newSpiralDistance;
    }
}
