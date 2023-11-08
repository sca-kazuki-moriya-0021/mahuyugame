using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A : MonoBehaviour
{
    public float bulletSpeed = 5.0f; // �e�̑��x
    public float homingDuration = 2.0f; // �ǔ��̎�������
    private Transform target; // �ǔ��Ώہi�ʏ�̓v���C���[�j
    private Rigidbody2D rb;
    private float homingTimer = 0.0f;
    private bool isHoming = true;
    private Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
        // �ǔ��Ώۂ�ݒ�i�ʏ�̓v���C���[�j
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(player.BulletSeverFlag == true)
        {
            Destroy(this.gameObject);
        }

        if (isHoming)
        {
            if (target != null)
            {
                // �v���C���[�̈ʒu��ڕW�Ƃ��A�e�����̕����ɐi�܂���
                Vector2 direction = (target.position - transform.position).normalized;
                rb.velocity = direction * bulletSpeed;

                // �ǔ��^�C�}�[���X�V
                homingTimer += Time.deltaTime;

                // �w��̎��Ԍo�ߌ�ɒǔ����I��
                if (homingTimer >= homingDuration)
                {
                    isHoming = false;
                }
            }
            else
            {
                // �ǔ��Ώۂ��j�󂳂ꂽ�ꍇ�A�ǔ����I�����ĉ�ʊO�Ɍ������Đi��
                isHoming = false;
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
