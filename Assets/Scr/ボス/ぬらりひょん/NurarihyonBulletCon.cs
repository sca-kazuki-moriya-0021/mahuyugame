using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurarihyonBulletCon : MonoBehaviour
{
    private NurarihyonPushBulletCon nurarihyonPushBulletCon;

    //�e�I�u�W�F�N�g
    [SerializeField]
    private GameObject[] bullets;
    //�e�̃X�s�[�h
    [SerializeField]
    private float[] bulletSpeed;

    //�e�̔��ˊ��o
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
