using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuibiLauncher : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab; // �e�̃v���n�u
    [SerializeField] Transform firePoint; // �e�̔��ˈʒu
    [SerializeField] float maxCount;
    [SerializeField] private float shotDelay = 0.3f; // �e�̔��ˊԊu
    [SerializeField]private float intervalDuration = 0.7f; // �C���^�[�o���̒���
    private float nextFireTime = 0; // ���ɔ��ˉ\�Ȏ���
    private int shotsFired = 0; // ���ˉ񐔂��J�E���g
    private bool isInterval = false; // �C���^�[�o�������ǂ����������t���O

    void Update()
    {
        if (isInterval && Time.time >= nextFireTime)
        {
            isInterval = false;
            shotsFired = 0; // ���ˉ񐔂����Z�b�g
        }

        if (!isInterval && Time.deltaTime >= nextFireTime)
        {
            Shoot();
            shotsFired++;

            if (shotsFired >= maxCount)
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
