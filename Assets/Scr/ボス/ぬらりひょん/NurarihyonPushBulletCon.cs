using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurarihyonPushBulletCon : MonoBehaviour
{
    private NurarihyonBulletCon nurarihyonBullet;
    private bool isMove = false;
    private Vector2 direction;
    private void Start()
    {
        nurarihyonBullet = FindObjectOfType<NurarihyonBulletCon>();
    }
    //使う弾はBulletAll
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

    public void ApolloReflector(GameObject bulletPrefab,int numberOfBullets,float bulletSpeed,int id)
    {
        var r = new Vector3(0, 0, 0);
        switch (id)
        {
            case 0:
                r = new Vector3(0, -0.5f, 0);
                break;
            case 1:
                r = transform.position;
                break;
        }
        float startAngle = -360 / 2;
        for(int i = 0;i < numberOfBullets; i++)
        {
            float angle = startAngle + i * (360 / (numberOfBullets));

            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            //Vector3 spawnPosition = transform.position + direction * radius; // 中心から方向に1の距離だけずらす
            Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, r, Quaternion.identity).GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }

    //使う弾はBulletAll
    public void RandomDoll(GameObject bulletPrefab,float bulletSpeed,int numberOfBullets)
    {
        float startAngle = -360 / 2;

        float x = Random.Range(7.15f,-6.9f);
        float y = Random.Range(-3.98f,1.95f);
        float z = Random.Range(0,0);

        for(int i = 0; i < numberOfBullets; i++)
        {
            float angle = startAngle + i * (360 / (numberOfBullets));

            Vector3 direction = Quaternion.Euler(0,0,angle) * Vector3.up;
            Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab,new Vector3(x,y,z),Quaternion.identity).GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }


    //使う弾はBulletAllとサブ弾(色が違うとよい)
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
        float[] angles = isReversed ? new float[] { 0f,50f,-50f, 45f, -45f, 40f, -40f, 35f, -35f, 5f, -5f, 10f, -10f, } : new float[] {-15f, -12f, -9f, -6f, -3f, 0f, 3f, 6f, 9f, 12f, 15f , 125f, -125 ,150f,-150f,175f,-175f};
        foreach (float angle in angles)
        {
            GameObject bullet = Instantiate(isReversed ? bulletPrefab : bulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 bulletDirection = Quaternion.Euler(0, 0, (isReversed ? -1 : 1) * transform.rotation.eulerAngles.z + angle) * direction;
            rb.velocity = bulletDirection * bulletSpeed;
        }
    }

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
                circle.SetDirection(Quaternion.Euler(0, 0, angle) * Vector2.left);
            }
        }
    }

    public void theHoming(GameObject bulletPrefab, Transform[] bulletSpawnPoints)
    {
        for (int i = 0; i < 4; i++)
        {
            float angle = i * (360 / 4);
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * 0.5f;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * 0.5f;

            foreach (var spawnPoint in bulletSpawnPoints)
            {
                Vector3 spawnposition = spawnPoint.position + new Vector3(x, y, 0);
                GameObject bullet = Instantiate(bulletPrefab, spawnposition, Quaternion.identity);
                NewHoming circle = bullet.GetComponent<NewHoming>();
                circle.IsMove = true;
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
    }

    public void PresenceOfEvil(Transform upperFirepoint,Transform lowerFirepoint, GameObject bulletPrefab,float bulletSpeed)
    {
        upperFirepoint.position = new Vector3(upperFirepoint.position.x, Random.Range(2.43f, 0.24f), upperFirepoint.position.z);

        lowerFirepoint.position = new Vector3(lowerFirepoint.position.x, Random.Range(-4.54f, -2.38f), lowerFirepoint.position.z);

        GameObject upperBullet = Instantiate(bulletPrefab, upperFirepoint.position, Quaternion.identity);
        upperBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, Time.deltaTime);

        GameObject lowerBullet = Instantiate(bulletPrefab, lowerFirepoint.position, Quaternion.identity);
        lowerBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, -Time.deltaTime);
    }

    public void CounterAttack(GameObject bulletPrefab,Transform centerPoint,int numberOfBullet,float radius,float rotationSpeed)
    {
        for (int i = 0; i < numberOfBullet; i++)
        {
            float angle = i * (360f / numberOfBullet);
            Vector3 spanwPosition = GetCirclePosition(centerPoint.position,angle,radius);
            GameObject bullet = Instantiate(bulletPrefab,spanwPosition,Quaternion.identity);
            bullet.transform.parent = centerPoint;
        }
        StartCoroutine(RotateBullets(centerPoint, rotationSpeed));
    }

    Vector3 GetCirclePosition(Vector3 center, float angle,float radius)
    {
        float x = center.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = center.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector3(x, y, center.z);
    }

    IEnumerator RotateBullets(Transform centerPoint,float rotationSpeed)
    {
        while (true)
        {
            foreach (Transform bulletTransform in centerPoint)
            {
                bulletTransform.RotateAround(centerPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            }

            yield return null;
        }
    }

    public IEnumerator ShootHomingBullet(Transform[] firePoints, GameObject homingBullet)
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            GameObject bullet = Instantiate(homingBullet, firePoints[i].position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    public void ClockBullet(GameObject bulletPrefab, float bulletSpeed)
    {
        Shoot(Vector2.right, bulletPrefab, bulletSpeed);
        Shoot(Vector2.left, bulletPrefab, bulletSpeed);
        Shoot(Vector2.up, bulletPrefab, bulletSpeed);
        Shoot(Vector2.down, bulletPrefab, bulletSpeed);
    }

    public void ClockBullet45(GameObject bulletPrefab, float bulletSpeed)
    {
        Shoot45(Vector2.right, bulletPrefab, bulletSpeed);
        Shoot45(Vector2.left, bulletPrefab, bulletSpeed);
        Shoot45(Vector2.up, bulletPrefab, bulletSpeed);
        Shoot45(Vector2.down, bulletPrefab, bulletSpeed);
    }

    public void reClockBullet(GameObject bulletPrefab, float bulletSpeed)
    {
        ShootReversed(Vector2.right, bulletPrefab, bulletSpeed);
        ShootReversed(Vector2.left, bulletPrefab, bulletSpeed);
        ShootReversed(Vector2.up, bulletPrefab, bulletSpeed);
        ShootReversed(Vector2.down, bulletPrefab, bulletSpeed);
    }

    private void Shoot(Vector2 direction, GameObject bulletPrefab, float bulletSpeed)
    {
        Vector3 spawnPosition = transform.position;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 bulletDirection = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * direction;
        rb.velocity = bulletDirection * bulletSpeed;
    }

    private void Shoot45(Vector2 direction, GameObject bulletPrefab, float bulletSpeed)
    {
        Vector3 spawnPosition = transform.position;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 bulletDirection = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 45) * direction;
        rb.velocity = bulletDirection * bulletSpeed;
    }

    private void ShootReversed(Vector2 direction, GameObject bulletPrefab, float bulletSpeed)
    {
        Vector3 spawnPosition = transform.position;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 bulletDirection = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z) * direction;
        rb.velocity = bulletDirection * bulletSpeed;
    }

    public void ShootBullets(int numberOfBullets, GameObject bulletPrefab, float bulletSpeed, float spiralDistance, float spiralRotationSpeed)
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = i * (360f / numberOfBullets);

            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            Vector3 spawnPosition = transform.position + direction * 0.5f;
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

            StartCoroutine(SpiralMotion(bulletRigidbody, direction, bulletSpeed, spiralDistance, spiralRotationSpeed));
        }
    }

    private IEnumerator SpiralMotion(Rigidbody2D bulletRigidbody, Vector3 initialDirection, float bulletSpeed, float spiralDistance, float spiralRotationSpeed)
    {
        float distanceTraveled = 0f;
        Vector3 startPosition = bulletRigidbody.position;
        float rotationDirection = (nurarihyonBullet.ShotCount % 2 == 0) ? 1 : -1;

        while (distanceTraveled < spiralDistance)
        {
            float angle = distanceTraveled * rotationDirection * spiralRotationSpeed / spiralDistance;

            Vector3 spiralMotion = Quaternion.Euler(0, 0, angle) * initialDirection;
            bulletRigidbody.velocity = spiralMotion * bulletSpeed;

            distanceTraveled = Vector3.Distance(bulletRigidbody.position, startPosition);

            yield return null;
        }
    }

    public void A(GameObject bulletPrefab, float bulletSpeed, int numberOfBullets, Transform[] bulletSpawnPoints, Transform player, float spreadAngle)
    {
        foreach (var spawnPoint in bulletSpawnPoints)
        {
            float startAngle = -360f / 2f;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = startAngle + i * (360f / numberOfBullets);
                Vector3 direction = Quaternion.Euler(0f, 0f, angle) * Vector3.up;
                Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                bulletRigidbody.velocity = direction * bulletSpeed;
            }
        }
        StartCoroutine(B(player,bulletPrefab,bulletSpeed,spreadAngle));
    }

    private IEnumerator B(Transform player, GameObject bulletPrefab, float bulletSpeed, float spreadAngle)
    {
        if (player != null)
        {
            Vector2 playerDirection = (player.position - transform.position).normalized;

            float startAngle = -spreadAngle / 2f;
            float angleStep = spreadAngle / (float)(6 - 1);

            for (int i = 0; i < 6; i++)
            {
                float angle = startAngle + i * angleStep;
                Vector2 direction = Quaternion.Euler(0f, 0f, angle) * playerDirection;

                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                bulletRb.velocity = direction * bulletSpeed;
            }
        }
        yield return null;
    }
public void RotaHoming(GameObject bulletPrefab, Transform centerPoint, int numberOfBullet, float radius, float duration)
    {
        isMove = false;
        StartCoroutine(C(bulletPrefab, centerPoint, numberOfBullet, radius, duration));
    }

    private IEnumerator C(GameObject bulletPrefab, Transform centerPoint, int numberOfBullet, float radius, float duration)
    {
        for (int i = 0; i < numberOfBullet; i++)
        {
            float angle = i * (360f / numberOfBullet);
            Vector3 spawnPosition = GetCirclePosition(centerPoint.position, angle, radius);
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

            nurarihyonBullet.Bullet.Add(bullet);

            // 少し待機してから次の弾を生成
            yield return new WaitForSeconds(duration / numberOfBullet);
        }

        isMove = true;
        foreach (var bullet in nurarihyonBullet.Bullet)
        {
            if (bullet != null)
            {
                bullet.GetComponent<NewHoming>().IsMove = isMove;
            }
        }
    }

    public void CallParticles(Transform[] a,GameObject particlePrefab) 
    {
        foreach (Transform transform in a)
        {
            GameObject particleObject = Instantiate(particlePrefab, transform.position, Quaternion.identity);

            // パーティクルをアクティブにする
            particleObject.SetActive(true);

            // 一定時間後に非アクティブにする
            StartCoroutine(DeactivateParticleAfterDelay(particleObject));
        }
    }
    
    private IEnumerator DeactivateParticleAfterDelay(GameObject particleObject)
    {
        yield return new WaitForSeconds(2f);

        // 一定時間後にパーティクルを非アクティブにする
        Destroy(particleObject);
    }
}
