using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingLauncher : MonoBehaviour
{
    [SerializeField,Header("�ˌ��n�_1")]
    GameObject Fire1;
    [SerializeField,Header("�ˌ��n�_2")]
    GameObject Fire2;
    [SerializeField,Header("�z�[�~���O�e")]
    GameObject HomingBullet;
    [SerializeField,Header("���ˊԊu")]
    float fireInterval = 1.0f;

    private float nextFireTime = 0.0f;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            // ���ˊԊu���o�߂�����z�[�~���O�e�𔭎�
            ShootHomingBullet(Fire1);
            ShootHomingBullet(Fire2);
            nextFireTime = Time.time + fireInterval;
        }
    }

    void ShootHomingBullet(GameObject firePoint)
    {
        if (firePoint != null && HomingBullet != null)
        {
            // �z�[�~���O�e�̃C���X�^���X�𐶐�
            GameObject homingBullet = Instantiate(HomingBullet, firePoint.transform.position, Quaternion.identity);
        }
    }
}
