using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuibiLauncher : MonoBehaviour
{
    public GameObject bulletPrefab; // �e�̃v���n�u
    public Transform firePoint; // �e�̔��ˈʒu
    private float nextFireTime = 0; // ���ɔ��ˉ\�Ȏ���
    private float shotDelay = 0.3f; // �e�̔��ˊԊu
    private int shotsFired = 0; // ���ˉ񐔂��J�E���g
    private float intervalDuration = 0.7f; // �C���^�[�o���̒���
    private bool isInterval = false; // �C���^�[�o�������ǂ����������t���O

    void Update()
    {
        if (isInterval && Time.time >= nextFireTime)
        {
            isInterval = false;
            shotsFired = 0; // ���ˉ񐔂����Z�b�g
        }

        if (!isInterval && Time.time >= nextFireTime)
        {
            Shoot();
            shotsFired++;

            if (shotsFired >= 3)
            {
                isInterval = true;
                nextFireTime = Time.time + intervalDuration;
            }
            else
            {
                nextFireTime = Time.time + shotDelay;
            }
        }
    }

    void Shoot()
    {
        // �e�̃v���n�u����V�����e�𐶐�
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // ���������e�𔭎�
        Rigidbody2D bulletRB = newBullet.GetComponent<Rigidbody2D>();
    }
}
