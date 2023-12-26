using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillTest : MonoBehaviour
{
    private Transform RangeA;
    private Transform RangeB;

    private List<Rigidbody2D> bulletsList = new List<Rigidbody2D>();
    // Start is called before the first frame update
    void Start()
    {
        RangeA = GameObject.Find("RangeA").transform;
        RangeB = GameObject.Find("RangeB").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Right(GameObject bulletPrefab, float bulletSpeed)
    {
        Vector3 spawnPosition = transform.position;
        float[] angles = { -25f, -20f, -15f, -10f, -5f, 0f, 5f, 10f, 15f, 20f };

        foreach (float angle in angles)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + angle) * Vector2.right;
            rb.velocity = direction * bulletSpeed;
        }
    }

    public void Left(GameObject bulletPrefab, float bulletSpeed)
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

    public void Up(GameObject bulletPrefab, float bulletSpeed)
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

    public void Down(GameObject bulletPrefab, float bulletSpeed)
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

    public void Reright(GameObject reBulletPrefab, float reBulletSpeed)
    {
        Vector3 spawnPosition = transform.position;
        float[] angles = { 0f, 45f, -45f };
        foreach (float angle in angles)
        {
            GameObject bullet = Instantiate(reBulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z + angle) * Vector2.right;
            rb.velocity = direction * reBulletSpeed;
        }
    }

    public void Releft(GameObject reBulletPrefab, float reBulletSpeed)
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

    public void Reup(GameObject reBulletPrefab, float reBulletSpeed)
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

    public void Redoen(GameObject reBulletPrefab, float reBulletSpeed)
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

    //AllRandomLauncher
    public void ShootBullets(float spreadAngle, int numberOfBullets, GameObject bulletPrefab, float bulletSpeed)
    {
        float startAngle = -spreadAngle / 2;
        var r = new Vector3(0, 2, 0);
        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = startAngle + i * (spreadAngle / (numberOfBullets));
            transform.TransformPoint(r);
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            GameObject bulletObject = Instantiate(bulletPrefab, r, Quaternion.identity);
            Rigidbody2D bulletRigidbody = bulletObject.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;

            bulletsList.Add(bulletRigidbody);
        }
    }

    public void StopBullets()
    {
        foreach (Rigidbody2D bulletRigidbody in bulletsList.ToArray())
        {
            //弾止め
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = Vector2.zero;
            }
        }
    }

    public void MoveBulletsRandomly(float bulletSpeed)
    {
        foreach (Rigidbody2D bulletRigidbody in bulletsList.ToArray())
        {
            //ランダム移動
            if (bulletRigidbody != null)
            {
                Vector2 randomVelocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                bulletRigidbody.velocity = randomVelocity * bulletSpeed;
            }
        }
    }

    //AllBullet
    public void AllBullet(float spreadAngle, int numberOfBullets, GameObject bulletPrefab, float bulletSpeed)
    {
        float startAngle = -spreadAngle / 2;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = startAngle + i * (spreadAngle / (numberOfBullets));

            // 弾を発射
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }

    //AllspiralLauncher
    public void AllspiralLauncher(int numberOfBullets, GameObject bulletPrefab)
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = i * (360f / numberOfBullets);

            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

            //StartCoroutine(SpiralMotion(bulletRigidbody, direction));
        }
    }

    public void UpdateSpiral(float bulletSpeed, float spiralRotationSpeed, float spiralDistance)
    {
        int newRotationSpeed = Random.Range(180, 450);
        float newSpiralDistance = Random.Range(5f, 10f);
        int newSpeed = Random.Range(3, 7);

        bulletSpeed = newSpeed;
        spiralRotationSpeed = newRotationSpeed;
        spiralDistance = newSpiralDistance;
    }

    //Launcher
    public void Launcher(float curAngle, int curBullet, float bulletSpacing, float maxbulletSpacing, Transform player, GameObject bulletPrefab, float bulletSpeed)
    {
        float angleStep = curAngle / (curBullet - 1);
        float initialAngle = this.transform.eulerAngles.z - (curAngle / 2);
        bulletSpacing += Time.deltaTime;

        if (bulletSpacing > maxbulletSpacing)
        {
            Vector2 playerPosition = player.position; // プレイヤーの位置を取得

            for (int i = 0; i < curBullet; i++)
            {
                // 弾を生成して、初期位置を設定
                GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                float bulletAngle = initialAngle + (i * angleStep);

                // プレイヤーの位置から弾の向きを計算
                Vector2 directionToPlayer = (playerPosition - (Vector2)this.transform.position).normalized;
                float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

                // 弾の向きを設定
                bullet.transform.rotation = Quaternion.Euler(0, 0, bulletAngle + angleToPlayer);

                // 弾の速度を設定
                rb.velocity = bullet.transform.up * bulletSpeed;

                bulletSpacing = 0.0f;
            }
        }
    }

    //SistersLauncher
    public void RotateBullet(GameObject bulletPrefab, float bulletSpeed)
    {
        Vector3 spawnPosition = transform.position;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector2.right;
        rb.velocity = direction * bulletSpeed;
    }

    public void SpreasBullet(float spreadAngle, GameObject subBulletPrefab, float subBulletSpeed1, float subBulletSpeed2)
    {
        Vector3 spawnPosition = transform.position;
        float randomAngle = Mathf.Lerp(-spreadAngle / 2, spreadAngle / 2, Random.value);
        Quaternion rotation = Quaternion.AngleAxis(randomAngle, Vector3.forward);
        GameObject bullet = Instantiate(subBulletPrefab, spawnPosition, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 bulletDirection = rotation * Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector2.right;
        rb.velocity = bulletDirection * subBulletSpeed1;
        GameObject bullet1 = Instantiate(subBulletPrefab, spawnPosition, rotation);
        Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
        Vector2 bulletDirection1 = rotation * Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector2.right;
        rb1.velocity = bulletDirection1 * subBulletSpeed2;
    }
    //HomingLauncher
    public void ShootHomingBullet(GameObject firePoint, GameObject homingBullet)
    {
        if (firePoint != null && homingBullet != null)
        {
            // ホーミング弾のインスタンスを生成
            GameObject bullet = Instantiate(homingBullet, firePoint.transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        }
    }

    //RandomLauncher
    public void RandomLauncher(float spreadAngle, int numberOfBullets, GameObject bulletPrefab, float bulletSpeed)
    {
        float startAngle = -spreadAngle / 2;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = startAngle + i * (spreadAngle / (numberOfBullets));

            // ランダムな方向を取得
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            // 方向にランダムな偏差を追加
            direction = Quaternion.Euler(0, 0, Random.Range(-10f, 10f)) * direction;

            Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }

    public void ShootBullet(int numberOfBullets, GameObject bulletPrefab, float bulletSpeed)
    {
        float startAngle = -360 / 2;

        float x = Random.Range(7.96f, -8.32f);
        float y = Random.Range(-3.94f, 3.94f);
        float z = Random.Range(0, 0);
        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = startAngle + i * (360 / (numberOfBullets));

            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, new Vector3(x, y, z), Quaternion.identity).GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }

}
