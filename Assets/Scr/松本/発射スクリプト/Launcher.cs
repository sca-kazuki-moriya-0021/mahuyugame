using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField, Header("�e�̃v���n�u")] private GameObject bulletPrefab;
    [SerializeField, Header("�e�̑��x")] private float bulletSpeed;
    [SerializeField, Header("�ŏ��̒e�̐�")] private int numberOfBullets;
    [SerializeField, Header("�ŏ��̕��ˏ�̊p�x")] private float spreadAngle;
    [SerializeField, Header("���ˊԊu")] private float bulletSpacing;
    [SerializeField, Header("�ύX�p���ˊԊu")] private float maxbulletSpacing;
    [SerializeField, Header("���̒e�̑�����")] private int bulletAmount;
    [SerializeField, Header("�e�𑝂₷����")] private float createBullet;
    [SerializeField, Header("�p�x�𑝂₷����")] private float timeAngle;
    [SerializeField, Header("�����p�x")] private float yimespreadAngle;
    [SerializeField, Header("�ő�e��")] private int maxBullet;

    private float bulletsTime; // �e�o�ߎ���
    private float elaTime; // �p�x�o�ߎ���
    private int curBullet; // ���݂̒e
    private float curAngle; // ���݂̊p�x

    private Transform player; // �v���C���[��Transform�R���|�[�l���g

    void Start()
    {
        curBullet = numberOfBullets;
        curAngle = spreadAngle;

        // �v���C���[��Transform���擾
        player = GameObject.FindWithTag("Player").transform; // �v���C���[�̃^�O�ɉ����ĕύX
    }

    void Update()
    {
        bulletsTime += Time.deltaTime;
        elaTime += Time.deltaTime;
        if (curBullet < maxBullet && bulletsTime >= createBullet)
        {
            curBullet += bulletAmount;
            curAngle += yimespreadAngle;
            bulletsTime = 0.0f;
        }
        if(curBullet == maxBullet)
        {
            maxbulletSpacing = 0.5f;
        }
        if (elaTime >= timeAngle)
        {
            Debug.Log("a");
            curAngle += yimespreadAngle;
            elaTime = 0.0f;
        }
        ShootNWayBullets(curBullet, curAngle);
    }

    private void ShootNWayBullets(int curBullet, float curAngle)
    {
        float angleStep = curAngle / (curBullet - 1);
        float initialAngle = transform.eulerAngles.z - (curAngle / 2);
        bulletSpacing += Time.deltaTime;

        if (bulletSpacing > maxbulletSpacing)
        {
            Vector2 playerPosition = player.position; // �v���C���[�̈ʒu���擾

            for (int i = 0; i < curBullet; i++)
            {
                // �e�𐶐����āA�����ʒu��ݒ�
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                float bulletAngle = initialAngle + (i * angleStep);

                // �v���C���[�̈ʒu����e�̌������v�Z
                Vector2 directionToPlayer = (playerPosition - (Vector2)transform.position).normalized;
                float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

                // �e�̌�����ݒ�
                bullet.transform.rotation = Quaternion.Euler(0, 0, bulletAngle + angleToPlayer);

                // �e�̑��x��ݒ�
                rb.velocity = bullet.transform.up * bulletSpeed;

                bulletSpacing = 0.0f;
            }
        }
    }
}
