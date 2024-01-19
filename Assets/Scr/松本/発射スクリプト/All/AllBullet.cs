using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public int numberOfShots;
    public int numberOfBullets;
    public float spreadAngle;
    public float fireTime;

    void Start()
    {
        StartCoroutine(ShootMultiSpread());
    }

    IEnumerator ShootMultiSpread()
    {
        while (true)
        {

            for (int i = 0; i < numberOfShots; i++)
            {
                ShootBullets();
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(fireTime);
        }
    }

    private void ShootBullets()
    {
        float startAngle = -spreadAngle / 2;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = startAngle + i * (spreadAngle / (numberOfBullets));

            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }
}
