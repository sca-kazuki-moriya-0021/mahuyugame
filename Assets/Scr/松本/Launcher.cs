using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; //�e�̃v���n�u
    [SerializeField] private float bulletSpeed = 5f; //�e�̑��x
    [SerializeField] private int numberOfBullets = 8; //�ŏ��̒e�̐�
    [SerializeField] private float spreadAngle = 45f; //�ŏ��̕��ˏ�̊p�x
    [SerializeField] private float bulletSpacing = 0.0f; //�e�̊Ԋu
    [SerializeField] private int bulletAmount = 1; //���̒e�̑�����
    [SerializeField] private float createBullet = 5.0f;//�e�𑝂₷����
    [SerializeField] private float timeAngle = 15f;//�p�x�𑝂₷���ԊԊu
    [SerializeField] private float yimespreadAngle = 10f;//�����p�x

    private float elaTime;//�o�ߎ���
    private int curbullet;//���݂̒e
    void Update()
    {
        ShootNWayBullets();
    }

    private void ShootNWayBullets()
    {
        float angleStep = spreadAngle / (numberOfBullets - 1);
        float initialAngle = transform.eulerAngles.z - (spreadAngle / 2);
        bulletSpacing += Time.deltaTime;
        if (bulletSpacing > 1.0f)
        {
            for (int i = 0; i < numberOfBullets; i++)
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
