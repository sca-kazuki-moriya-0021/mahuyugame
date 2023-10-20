using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab; // �e�̃v���n�u
    [SerializeField] float spawnInterval = 1.0f; // �e�̐����Ԋu
    [SerializeField] float spreadAngle = 45.0f; // ��`�̊p�x
    [SerializeField] private float bulletSpeed;

    private void Start()
    {
        // �w�肵���Ԋu�Œe�𐶐�
        InvokeRepeating("SpawnBullet", 0f, spawnInterval);
    }

    void SpawnBullet()
    {

        // �e�̐����ʒu��ݒ�
        Vector3 spawnPosition = transform.position;

        // �����_���Ȋp�x�𐶐�
        float randomAngle = Random.Range(-spreadAngle / 2, spreadAngle / 2);

        // �p�x�����W�A���ɕϊ�
        float radianAngle = Mathf.Deg2Rad * randomAngle;

        // �e�̌�����ݒ�
        Quaternion rotation = Quaternion.Euler(0f, 0f, radianAngle);

        // �e�𐶐�
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        // �e�̑��x�x�N�g����ݒ肵�āA�w�肳�ꂽ�p�x�ɔ�΂�
        Vector2 bulletDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.left;
        rb.velocity = bulletDirection * bulletSpeed;
    }
}
