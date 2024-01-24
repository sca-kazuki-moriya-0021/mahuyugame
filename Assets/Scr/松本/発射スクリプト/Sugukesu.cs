using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugukesu : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float timeBetweenShots;
    [SerializeField] float cooldownTime;

    public bool rotationFlag = true;

    private float timer = 0;
    private float coolTimer = 0;
    private float currentRotation = 0;

    private Transform playerTransform;
    private bool isMove = false;
    private List<GameObject> bullets = new List<GameObject>();

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        StartCoroutine(BulletSpawnRoutine());
    }

    IEnumerator BulletSpawnRoutine()
    {
        while (true)
        {
            timer += Time.deltaTime;
            
            if (currentRotation >= 360f)
            {
                isMove = true;
                coolTimer += Time.deltaTime;

                if (coolTimer >= cooldownTime)
                {
                    currentRotation = 0f;
                    coolTimer = 0f;
                }
            }
            else
            {
                if (timer >= timeBetweenShots)
                {
                    Debug.Log(timer);
                    isMove = false;
                    RotateBullet();
                    timer = 0f;
                }
            }

            foreach (var bullet in bullets)
            {
                if (bullet != null)
                {
                    bullet.GetComponent<NewHoming>().IsMove = isMove;
                }
            }
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
            currentRotation += rotationSpeed * Time.deltaTime;

            yield return null; // 1フレーム待機
        }
    }

    private void RotateBullet()
    {
        Vector3 spawnPosition = transform.position;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        bullets.Add(bullet); // リストに追加
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector2.right;
        rb.velocity = direction * bulletSpeed;
    }



}
