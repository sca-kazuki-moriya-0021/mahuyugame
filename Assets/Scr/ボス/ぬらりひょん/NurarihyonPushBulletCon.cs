using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurarihyonPushBulletCon : MonoBehaviour
{
    private NurarihyonBulletCon nurarihyonBullet;

    private Vector2 direction;
    private void Start()
    {
        nurarihyonBullet = FindObjectOfType<NurarihyonBulletCon>();
    }
    //égÇ§íeÇÕBulletAll
    public void AllBullet(GameObject bulletPrefab,float bulletSpeed,int numberOfBullets)
    {
        float startAngle = -360 / 2;

        for(int i = 0; i < numberOfBullets; i++)
        {
            float angle = startAngle + i * (360 / (numberOfBullets));
            Vector3 direction = Quaternion.Euler(0,0,angle) * Vector3.up;
            Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }

    public void Reflect(GameObject bulletPrefab, float bulletSpeed, int numberOfBullets,int id)
    {
        float startAngle = -360 / 2;
        var r = new Vector3(0, 0, 0);
        switch (id)
        {
            case 0:
                r = new Vector3(4.5f, 4.5f, 0);
                break;
            case 1:
                r = new Vector3(4.5f, 0, 0);
                break;
            case 2:
                r = new Vector3(4.5f, -4.5f, 0);
                break;
            case 3:
                r = new Vector3(6.5f, 2.25f, 0);
                break;
            case 4:
                r = new Vector3(6.5f,-2.25f,0);
                break;
        }
        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = startAngle + i * (360 / (numberOfBullets));
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, r, Quaternion.identity).GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }

    public void ApolloReflector(GameObject bulletPrefab,int numberOfBullets,float bulletSpeed,float radius)
    {
        float startAngle = -360 / 2;
        for(int i = 0;i < numberOfBullets; i++)
        {
            float angle = startAngle + i * (360 / (numberOfBullets));

            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            Vector3 spawnPosition = transform.position + direction * radius; // íÜêSÇ©ÇÁï˚å¸Ç…1ÇÃãóó£ÇæÇØÇ∏ÇÁÇ∑
            Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }

    //égÇ§íeÇÕBulletAll
    public void RandomDoll(GameObject bulletPrefab,float bulletSpeed,int numberOfBullets)
    {
        float startAngle = -360 / 2;

        float x = Random.Range(7.9f,-8.3f);
        float y = Random.Range(-3.9f,3.9f);
        float z = Random.Range(0,0);

        for(int i = 0; i < numberOfBullets; i++)
        {
            float angle = startAngle + i * (360 / (numberOfBullets));

            Vector3 direction = Quaternion.Euler(0,0,angle) * Vector3.up;
            Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab,new Vector3(x,y,z),Quaternion.identity).GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }


    //égÇ§íeÇÕBulletAllÇ∆ÉTÉuíe(êFÇ™à·Ç§Ç∆ÇÊÇ¢)
    public void AllRange(GameObject bulletPrefab, float bulletSpeed)
    {
        Range1(Vector2.right, bulletPrefab, bulletSpeed);
        Range1(Vector2.left, bulletPrefab, bulletSpeed);
        Range1(Vector2.up, bulletPrefab, bulletSpeed);
        Range1(Vector2.down, bulletPrefab, bulletSpeed);
    }

    public void ReAllRange(GameObject reBulletPrefab, float reBulletSpeed)
    {
        Range2(Vector2.right, reBulletPrefab, reBulletSpeed);
        Range2(Vector2.left, reBulletPrefab, reBulletSpeed);
        Range2(Vector2.up, reBulletPrefab, reBulletSpeed);
        Range2(Vector2.down, reBulletPrefab, reBulletSpeed);
    }

    private void Range1(Vector2 direction, GameObject bulletPrefab, float speed)
    {
        Range(direction, false, bulletPrefab, speed);
    }

    private void Range2(Vector2 direction, GameObject bulletPrefab, float speed)
    {
        Range(direction, true, bulletPrefab, speed);
    }

    private void Range(Vector2 direction, bool isReversed, GameObject bulletPrefab, float bulletSpeed)
    {
        Vector3 spawnPosition = transform.position;
        float[] angles = isReversed ? new float[] { 0f, 45f, -45f, 40f, -40f } : new float[] { -20f, -15f, -10f, -5f, 0f, 5f, 10f, 15f, 20f, 25f };
        foreach (float angle in angles)
        {
            GameObject bullet = Instantiate(isReversed ? bulletPrefab : bulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 bulletDirection = Quaternion.Euler(0, 0, (isReversed ? -1 : 1) * transform.rotation.eulerAngles.z + angle) * direction;
            rb.velocity = bulletDirection * bulletSpeed;
        }
    }
    //Ç¢Ç¡ÇΩÇÒèCê≥Ç∑ÇÈ
    //public void RotateBullet(GameObject bulletPrefab,float bulletSpeed)
    //{
    //    Vector3 spawnPosition = transform.position;
    //    GameObject bullet = Instantiate(bulletPrefab,spawnPosition,Quaternion.identity);
    //    nurarihyonBullet.Homing.Add(bullet);
    //    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    //    Vector2 direction = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector2.right;
    //    rb.velocity = direction * bulletSpeed;
    //}

    public void SpawnCircle(GameObject bulletPrefab, Transform[] bulletSpawnPoints)
    {
        for(int i = 0; i < 4; i++)
        {
            float angle = i * (360 / 4);
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * 0.5f;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * 0.5f;

            foreach(var spawnPoint in bulletSpawnPoints)
            {
                Vector3 spawnposition = spawnPoint.position + new Vector3(x,y,0);
                GameObject bullet = Instantiate(bulletPrefab,spawnposition,Quaternion.identity);
                CircleBullet circle = bullet.GetComponent<CircleBullet>();
                circle.SetDirection(Quaternion.Euler(0, 0, angle) * Vector2.right);
            }
        }
    }

    public void fanshapeNway(Transform player,GameObject bulletPrefab,float bulletSpeed,float spreadAngle,int numberOfBullets)
    {
        if (player != null)
        {
            Vector2 playerDirection = (player.position - transform.position).normalized;

            float startAngle = -spreadAngle / 2f;
            float angleStep = spreadAngle / (float)(numberOfBullets - 1);

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = startAngle + i * angleStep;
                Vector2 direction = Quaternion.Euler(0f, 0f, angle) * playerDirection;

                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                bulletRb.velocity = direction * bulletSpeed;
            }
        }
        else
        {
            Debug.LogWarning("Player not assigned for shooting bullets.");
        }
    }
}
