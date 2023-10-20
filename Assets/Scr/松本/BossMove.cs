using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField]
    GameObject TimeBulletSpawn;
    [SerializeField]
    GameObject BulletSpawn;
    [SerializeField]
    GameObject SnakeSpawn;
    private float angle;
    private Vector3 startPos;
    private float elapsedTime = 0.0f;
    private int phase = 0; // �t�F�[�Y���Ǘ�

    public float speed = 2.0f; // �ړ����x
    public float amplitudeX = 3.0f; // X���̐U��
    public float amplitudeY = 1.0f; // Y���̐U��
    public float skill1Duration = 5.0f; // �X�L��1�̎�������
    public float danmakuDuration = 10.0f; // �e���̎�������
    public float skill2Duration = 5.0f; // �X�L��2�̎�������

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        angle += Time.deltaTime * speed;
        elapsedTime += Time.deltaTime;

        float x = startPos.x + Mathf.Sin(angle * 2) * amplitudeX;
        float y = startPos.y + Mathf.Sin(angle) * amplitudeY;

        // Z���̈ʒu�͌Œ�i2D��ԂɌŒ�j
        transform.position = new Vector3(x, y, 0);

        // �e�t�F�[�Y�ł̏���
        switch (phase)
        {
            case 0:
                // �X�L��1�̏���
                SnakeSpawn.SetActive(false);
                TimeBulletSpawn.SetActive(true);
                break;
            case 1:
                // �e���̏���
                TimeBulletSpawn.SetActive(false);
                BulletSpawn.SetActive(true);
                break;
            case 2:
                BulletSpawn.SetActive(false);
                SnakeSpawn.SetActive(true);
                // �X�L��2�̏���
                break;
        }

        // �t�F�[�Y�Ǘ�
        if (phase == 0 && elapsedTime >= skill1Duration)
        {
            // �X�L��1�̏������I�����玟�̃t�F�[�Y��
            phase = 1;
            elapsedTime = 0.0f;
        }
        else if (phase == 1 && elapsedTime >= danmakuDuration)
        {
            // �e���̏������I�����玟�̃t�F�[�Y��
            phase = 2;
            elapsedTime = 0.0f;
        }
        else if (phase == 2 && elapsedTime >= skill2Duration)
        {
            // �X�L��2�̏������I������ŏ��̃t�F�[�Y�֖߂�
            phase = 0;
            elapsedTime = 0.0f;
        }


    }
}
