using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : MonoBehaviour
{
    public float speed = 5.0f; // �e�̑��x
    public float rotationSpeed = 45.0f; // ��]���x

    private Player player;
    private Vector2 direction;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        // �e�̈ʒu���X�V
        transform.Translate(direction * speed * Time.deltaTime);

        // �e����]
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyBullet") && player.BulletSeverFlag)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyBullet") && player.BulletSeverFlag)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
