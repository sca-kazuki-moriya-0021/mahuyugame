using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllRangeLauncher : MonoBehaviour
{
    [SerializeField]GameObject bulletPrefab;
    [SerializeField]GameObject reBulletPrefab;
    [SerializeField]float bulletSpeed;
    [SerializeField]float reBulletSpeed;
    [SerializeField]float rotationSpeed;
    [SerializeField]float timeShots;
    [SerializeField]float reTimeShots;

    private float timer = 0f;
    private float reTimer = 0;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        reTimer += Time.deltaTime;
        if(player.BulletSeverFlag == false)
        {
            if (timer >= timeShots)
            {
                Right();
                Left();
                Up();
                Down();
                timer = 0;
            }
            if (reTimer >= reTimeShots)
            {
                Revright();
                Revleft();
                Revup();
                Revdown();
                reTimer = 0;
            }
        }
    }

    private void Right()
    {
        Vector3 spawnPosition = transform.position;
        float [] angles = {-25f,-20f,-15f,-10f,-5f,0f,5f,10f,15f,20f};

        foreach(float angle in angles)
        {
            GameObject bullet = Instantiate(bulletPrefab,spawnPosition,Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z + angle) * Vector2.right;
            rb.velocity = direction * bulletSpeed;
        }
    }

    private void Left()
    {
        Vector3 spawnPosition = transform.position;
        float[] angles = { -25f, -20f, -15f, -10f, -5f, 0f, 5f, 10f, 15f, 20f };

        foreach (float angle in angles)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + angle) * Vector2.left;
            rb.velocity = direction * bulletSpeed;
        }
    }

    private void Up()
    {
        Vector3 spawnPosition = transform.position;
        float[] angles = { -20f, -15f, -10f, -5f, 0f, 5f, 10f, 15f, 20f, 25f };

        foreach (float angle in angles)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + angle) * Vector2.up;
            rb.velocity = direction * bulletSpeed;
        }
    }

    private void Down()
    {
        Vector3 spawnPosition = transform.position;
        float[] angles = { -20f, -15f, -10f, -5f, 0f, 5f, 10f, 15f, 20f, 25f };

        foreach (float angle in angles)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + angle) * Vector2.down;
            rb.velocity = direction * bulletSpeed;
        }
    }

    private void Revright()
    {
        Vector3 spawnPosition = transform.position;
        float [] angles = {0f,45f,-45f};
        foreach(float angle in angles)
        {
            GameObject bullet = Instantiate(reBulletPrefab,spawnPosition,Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = Quaternion.Euler(0,0,-transform.rotation.eulerAngles.z + angle) * Vector2.right;
            rb.velocity = direction * reBulletSpeed;
        }
    }

    private void Revleft()
    {
        Vector3 spawnPosition = transform.position;
        float[] angles = { 0f, 45f, -45f };
        foreach (float angle in angles)
        {
            GameObject bullet = Instantiate(reBulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z + angle) * Vector2.left;
            rb.velocity = direction * reBulletSpeed;
        }
    }

    private void Revup()
    {
        Vector3 spawnPosition = transform.position;
        float[] angles = { 0f, 40f, -40f };
        foreach (float angle in angles)
        {
            GameObject bullet = Instantiate(reBulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z + angle) * Vector2.up;
            rb.velocity = direction * reBulletSpeed;
        }
    }

    private void Revdown()
    {
        Vector3 spawnPosition = transform.position;
        float[] angles = { 0f, 40f, -40f };
        foreach (float angle in angles)
        {
            GameObject bullet = Instantiate(reBulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z + angle) * Vector2.down;
            rb.velocity = direction * reBulletSpeed;
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0f,0f,rotationSpeed * Time.deltaTime);
    }
}
