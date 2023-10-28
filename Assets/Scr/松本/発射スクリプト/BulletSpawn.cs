using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField, Range(0, 360)] private float launchAngle = 45.0f;
    [SerializeField] private float fireTime;

    private float bulletTime = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        bulletTime += Time.deltaTime;
        if(bulletTime > fireTime)
        {
            ShootBulletWithCustomDirection();
            bulletTime = 0.0f;
        }
    }

    private void ShootBulletWithCustomDirection()
    {
        // �e�𐶐����āA�����ʒu��ݒ�
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // �e�̌������J�X�^�}�C�Y���邽�߂ɁA�e�̊p�x��ύX
        bullet.transform.rotation = Quaternion.Euler(0, 0, launchAngle);

        // �e�̑��x�x�N�g����ݒ肵�āA�w�肳�ꂽ�p�x�ɔ�΂�
        Vector2 bulletDirection = Quaternion.Euler(0, 0, launchAngle) * Vector2.up;
        rb.velocity = bulletDirection * bulletSpeed;
    }
}
