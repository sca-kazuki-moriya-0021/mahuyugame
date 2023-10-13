using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [SerializeField, Header("�e�̒ǔ��Ώ�")] Transform target;
    [SerializeField, Header("�e�̑��x")] float speed = 5f;
    [SerializeField, Header("�z�[�~���O���L���Ȏ���")] float homingDuration = 2.0f; // �z�[�~���O���L���Ȏ���

    private bool homingEnabled = true;
    private float homingTimer = 0.0f;

    private Vector3 forwardDirection; // �z�[�~���O�������ɂȂ�����̐i�s����

    private void Start()
    {
        forwardDirection = transform.up; // �����i�s������ݒ�
    }

    private void Update()
    {
        if (homingEnabled)
        {
            if (target != null)
            {
                Homing();
                homingTimer += Time.deltaTime;

                if (homingTimer >= homingDuration)
                {
                    homingEnabled = false;
                    forwardDirection = (target.position - transform.position).normalized; // �^�[�Q�b�g�̕����ɐi�s������ݒ�
                }
            }
            else
            {
                Forward();
            }
        }
        else
        {
            Forward();
        }
    }

    private void Homing()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 velocity = direction * speed;
        transform.position += velocity * Time.deltaTime;
    }

    private void Forward()
    {
        Vector3 velocity = forwardDirection * speed;
        transform.position += velocity * Time.deltaTime;
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
