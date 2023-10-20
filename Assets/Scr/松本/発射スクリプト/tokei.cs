using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tokei : MonoBehaviour
{
    public GameObject bulletPrefab; // ’e‚ÌƒvƒŒƒnƒu
    public float bulletSpeed = 5f; // ’e‚Ì‘¬‚³
    public float rotationSpeed = 60f; // ’e‚Ì‰ñ“]‘¬“xi“x/•bj

    private float timer = 0f;
    public float timeBetweenShots = 0.1f; // ’e‚Ì”­ŽËŠÔŠu

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenShots)
        {
            ShootBullet();
            timer = 0f;
        }
    }

    private void ShootBullet()
    {
        Vector3 spawnPosition = transform.position;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector2.right;
        rb.velocity = direction * bulletSpeed;
    }

    private void FixedUpdate()
    {
        // ’e–‹‚Ì‰ñ“]
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
