using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{

    //���ꂼ��̈ʒu��ۑ�����ϐ�
    //�X�^�[�g�n�_
    private Vector3 bulletPostion;

    private float time = 0;

    private void Awake()
    {
        bulletPostion  = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //�e�̐i�ފ�����Time.deltaTime�Ō��߂�
        time += Time.deltaTime;
        transform.Translate(Vector3.right * time);
    }

    void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
