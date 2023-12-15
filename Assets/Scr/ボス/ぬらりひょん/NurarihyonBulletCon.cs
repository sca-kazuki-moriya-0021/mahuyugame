using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurarihyonBulletCon : MonoBehaviour
{
    private NurarihyonPushBulletCon nurarihyonPushBulletCon;

    //弾オブジェクト
    [SerializeField]
    private GameObject[] bullets;
    //弾のスピード
    [SerializeField]
    private float[] bulletSpeed;

    //弾の発射感覚
    [SerializeField]
    private float[] fireTime;
    private int count = 0;

    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Atk());
        nurarihyonPushBulletCon = FindObjectOfType<NurarihyonPushBulletCon>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }


    private IEnumerator Atk()
    {
        yield return null;
    }
}
