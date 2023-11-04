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
    void FixedUpdate()
    {
        //�e�̐i�ފ�����Time.deltaTime�Ō��߂�
        
        transform.Translate(Vector3.right * Time.deltaTime * 1.5f);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
