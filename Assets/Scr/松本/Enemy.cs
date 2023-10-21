using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("�e�̔��˃v���n�u")]
    GameObject bulletPoint;
    [SerializeField, Header("�����Ԃ��e�v���n�u")]
    GameObject deathBulletPoint;
    [SerializeField, Header("�̗�")]
    int hp;
    public Transform centerPoint;  // ��]�̒��S�_
    public float rotationSpeed = 30.0f;  // �ړ����x�i�x/�b�j

    void Start()
    {
        bulletPoint.SetActive(true);
    }

    void Update()
    {
        // ���S�_�𒆐S�Ɏ��v���Ɉړ�
        Vector2 direction = (Vector2)(transform.position - centerPoint.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= rotationSpeed * Time.deltaTime;

        // �V�����ʒu���v�Z
        float radius = direction.magnitude;
        float newX = centerPoint.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float newY = centerPoint.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

        // �L�����N�^�[�̈ʒu���X�V
        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("PlayerBullet"))
        {
            //hp = hp - BulletPower;
            if (hp == 0)
            {
                deathBulletPoint.SetActive(true);
                Invoke("Death", 3.0f);
            }
        }
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
