using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{

    //���ꂼ��̈ʒu��ۑ�����ϐ�
    //�X�^�[�g�n�_
    private Vector2 charaPos;
    public Vector2 CharaPos { set { charaPos = value; } }
    //�S�[���n�_
    private Vector2 playerPos;
    public Vector2 PlayerPos { set { playerPos = value; } }
    //���p�n�_
    private Vector2 greenPos;
    public Vector2 GreenPos { set { greenPos = value; } }
    //�i�ފ������Ǘ�����ϐ�
    private float time;



    // Update is called once per frame
    void Update()
    {
        //�e�̐i�ފ�����Time.deltaTime�Ō��߂�
        time += Time.deltaTime;

        //�񎟃x�W�F�Ȑ�
        //�X�^�[�g�n�_���璆�p�n�_�܂ł̃x�N�g�����ʂ�_�̌��݂̈ʒu
        var a = Vector3.Lerp(charaPos, greenPos, time);
        //���p�n�_����^�[�Q�b�g�܂ł̃x�N�g�����ʂ�_�̌��݂̈ʒu
        var b = Vector3.Lerp(greenPos, playerPos, time);
        //��̓�̓_�����񂾃x�N�g�����ʂ�_�̌��݂̈ʒu�i�e�̈ʒu�j
        this.transform.position = Vector3.Lerp(a, b, time);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
