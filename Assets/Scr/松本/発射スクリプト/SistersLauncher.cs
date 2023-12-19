using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistersLauncher : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject subBulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float subBulletSpeed1;
    [SerializeField] float subBulletSpeed2;
    [SerializeField] float rotationSpeed;
    [SerializeField] float spreadAngle;
    [SerializeField] float timeBetweenShots;
    [SerializeField] float subTime;
    [SerializeField] float cooldownTime;

    private bool rotationFlag = true;
    private float timer = 0;
    private float subTimer = 0;
    private float coolTimer = 0;
    private float currentRotation = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        subTimer += Time.deltaTime;
        if(currentRotation >= 360f)
        {
            coolTimer += Time.deltaTime;
            if(coolTimer >= cooldownTime)
            {
                currentRotation = 0f;
                coolTimer = 0f;

                rotationFlag = !rotationFlag;
            }
        }
        else
        {
            if(timer >= timeBetweenShots)
            {
                RotateBullet();
                timer = 0f;
            }
            if(subTimer >= subTime)
            {
                SpreadBullet();
                subTimer = 0f;
            }
        }
        float rotationDirection = rotationFlag ? 1 : -1;
        transform.Rotate(0f,0f,rotationSpeed * Time.deltaTime * rotationDirection);
        currentRotation += rotationSpeed * Time.deltaTime;
    }

    private void RotateBullet()
    {
        Vector3 spawnPosition = transform.position;
        GameObject bullet = Instantiate(bulletPrefab,spawnPosition,Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z) * Vector2.right;
        rb.velocity = direction * bulletSpeed;
    }

    private void SpreadBullet()
    {
        Vector3 spawnPosition = transform.position;
        float randomAngle = Mathf.Lerp(-spreadAngle / 2,spreadAngle / 2,Random.value);
        Quaternion rotation = Quaternion.AngleAxis(randomAngle,Vector3.forward);
        GameObject bullet = Instantiate(subBulletPrefab,spawnPosition,rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 bulletDirection = rotation * Quaternion.Euler(0,0,transform.rotation.eulerAngles.z) * Vector2.right;
        rb.velocity = bulletDirection * subBulletSpeed1;
        GameObject bullet1 = Instantiate(subBulletPrefab, spawnPosition, rotation);
        Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
        Vector2 bulletDirection1 = rotation * Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector2.right;
        rb1.velocity = bulletDirection1 * subBulletSpeed2;

    }
}
