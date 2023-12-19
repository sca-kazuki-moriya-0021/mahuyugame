using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : MonoBehaviour
{
    [SerializeField] float speed; // �e�̑��x
    [SerializeField] float rotationSpeed; // ��]���x
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int numberOfBullets;
    [SerializeField] float spreadAngle;
    private Player player;
    private Vector2 direction;
    private float timer;
    private float destroyTimer;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        destroyTimer += Time.deltaTime;
        // �e�̈ʒu���X�V
        transform.Translate(direction * speed * Time.deltaTime);

        // �e����]
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        if(timer >= 2f)
        {
            float startAngle = -spreadAngle / 2;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = startAngle + i * (spreadAngle / (numberOfBullets));

                // �e�𔭎�
                Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
                Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                bulletRigidbody.velocity = direction * speed;
            }
            timer = 0f;
        }

        if (player.BulletSeverFlag == true)
        {
            float startAngle = -spreadAngle / 2;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = startAngle + i * (spreadAngle / (numberOfBullets));

                // �e�𔭎�
                Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
                Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                bulletRigidbody.velocity = direction * speed;
            }
            Destroy(this.gameObject);
        }

        if(destroyTimer >= 5f)
        {
            Destroy(this.gameObject);
        }
    }

    // �e�̕�����ݒ肷�郁�\�b�h
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    // �e�̉�]���x��ݒ肷�郁�\�b�h
    public void SetRotationSpeed(float newRotationSpeed)
    {
        rotationSpeed = newRotationSpeed;
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
