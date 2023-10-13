using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField,Header("�e�̃v���n�u")] private GameObject bulletPrefab;
    [SerializeField,Header("�e�̑��x")] private float bulletSpeed;
    [SerializeField,Header("�ŏ��̒e�̐�")] private int numberOfBullets;
    [SerializeField,Header("�ŏ��̕��ˏ�̊p�x")] private float spreadAngle;
    [SerializeField,Header("���ˊԊu")] private float bulletSpacing;
    [SerializeField,Header("���̒e�̑�����")] private int bulletAmount;
    [SerializeField,Header("�e�𑝂₷����")] private float createBullet;
    [SerializeField,Header("�p�x�𑝂₷����")] private float timeAngle;
    [SerializeField,Header("�����p�x")] private float yimespreadAngle;
    [SerializeField,Header("�ő�e��")]private int MaxBullet;

    private float BulletsTime;//�e�o�ߎ���
    private float elaTime;//�p�x�o�ߎ���
    private int curBullet;//���݂̒e
    private float curAngle;//���݂̊p�x

     void Start()
    {
        curBullet = numberOfBullets;
        curAngle = spreadAngle;
    }

    void Update()
    {
        BulletsTime += Time.deltaTime;
        elaTime += Time.deltaTime;
        if(curBullet < MaxBullet && BulletsTime >= createBullet)
        {
            curBullet += bulletAmount;
            curAngle += yimespreadAngle;
            BulletsTime = 0.0f;
        }
        if(elaTime >= timeAngle)
        {
            Debug.Log("a");
            curAngle += yimespreadAngle;
            elaTime = 0.0f;
        }
        ShootNWayBullets(curBullet,curAngle);
    }

    private void ShootNWayBullets(int curBullet, float curAngle)
    {
        float angleStep = curAngle / (curBullet - 1);
        float initialAngle = transform.eulerAngles.z - (curAngle / 2);
        bulletSpacing += Time.deltaTime;
        if (bulletSpacing > 2.0f)
        {
            for (int i = 0; i < curBullet; i++)
            {
                // �e�𐶐����āA�����ʒu��ݒ�
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                float bulletAngle = initialAngle + (i * angleStep);

                // �e�̌������J�X�^�}�C�Y���邽�߂ɁA�e�̊p�x��ύX
                bullet.transform.rotation = Quaternion.Euler(0, 0, bulletAngle);

                Vector2 bulletDirection = Quaternion.Euler(0, 0, bulletAngle) * Vector2.up;
                rb.velocity = bulletDirection * bulletSpeed;
            }
            bulletSpacing = 0.0f;
        }
            
    }
}
