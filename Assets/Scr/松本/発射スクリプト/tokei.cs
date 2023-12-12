using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tokei : MonoBehaviour
{
    public GameObject bulletPrefab; // ’e‚ÌƒvƒŒƒnƒu
    public GameObject bulletPrefab1;
    public float bulletSpeed = 5f; // ’e‚Ì‘¬‚³
    public float rotationSpeed = 60f; // ’e‚Ì‰ñ“]‘¬“xi“x/•bj
    public bool flag;
    private float timer = 0f;
    public float timeBetweenShots = 0.1f; // ’e‚Ì”­ŽËŠÔŠu

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenShots)
        {
            Right();
            Left();
            Up();
            Down();
            Revright();
            Revleft();
            ReUp();
            ReDown();
            timer = 0f;
        }
    }


    private void Right()
    {
        Vector3 spawnPosition = transform.position;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector2.right;
        rb.velocity = direction * bulletSpeed;
    }

    private void Left()
    {
        Vector3 spawnPosition = transform.position;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector2.left;
        rb.velocity = direction * bulletSpeed;
    }

    private void Up()
    {
        Vector3 spawnPosition = transform.position;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector2.up;
        rb.velocity = direction * bulletSpeed;
    }

    private void Down()
    {
        Vector3 spawnPosition = transform.position;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector2.down;
        rb.velocity = direction * bulletSpeed;
    }

    private void Revright()
    {
        Vector3 spawnPosition = transform.position;
        GameObject bullet = Instantiate(bulletPrefab1, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z) * Vector2.right;
        rb.velocity = direction * bulletSpeed;
    }

    private void Revleft()
    {
        Vector3 spawnPosition = transform.position;
        GameObject bullet = Instantiate(bulletPrefab1, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z) * Vector2.left;
        rb.velocity = direction * bulletSpeed;
    }

    private void ReUp()
    {
        Vector3 spawnPosition = transform.position;
        GameObject bullet = Instantiate(bulletPrefab1, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z) * Vector2.up;
        rb.velocity = direction * bulletSpeed;
    }

    private void ReDown()
    {
        Vector3 spawnPosition = transform.position;
        GameObject bullet = Instantiate(bulletPrefab1, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z) * Vector2.down;
        rb.velocity = direction * bulletSpeed;
    }

    private void FixedUpdate()
    {
        // ’e–‹‚Ì‰ñ“]
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
