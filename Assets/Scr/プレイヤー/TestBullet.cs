using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{

    //���ꂼ��̈ʒu��ۑ�����ϐ�
    //�X�^�[�g�n�_

    private Vector3 bulletPostion;

    private float time = 0;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        bulletPostion  = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�e�̐i�ފ�����Time.deltaTime�Ō��߂�
        if(player.PBaffSkillFlag == true)
            transform.Translate(Vector3.right * Time.deltaTime * 3.0f);
        else
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

        if (collision.gameObject.CompareTag("DestroyBullet"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyBullet"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
