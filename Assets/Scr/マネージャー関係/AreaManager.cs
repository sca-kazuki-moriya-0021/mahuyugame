using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClips;

    //ボスオブジェクト
    //[SerializeField]
    //private GameObject bossObject;

    //雑魚オブジェクトの配列
    //[SerializeField]
    //private GameObject[] enemyObeject;

    //中ボスオブジェクト
    //[SerializeField]
    //private GameObject underBossObject

    private TotalGM gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<TotalGM>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[1];
        audioSource.Play();

        //敵の出現をコルーチンで書いていく
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
