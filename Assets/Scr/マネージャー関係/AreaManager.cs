using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClips;

    //�{�X�I�u�W�F�N�g
    //[SerializeField]
    //private GameObject bossObject;

    //�G���I�u�W�F�N�g�̔z��
    //[SerializeField]
    //private GameObject[] enemyObeject;

    //���{�X�I�u�W�F�N�g
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

        //�G�̏o�����R���[�`���ŏ����Ă���
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
