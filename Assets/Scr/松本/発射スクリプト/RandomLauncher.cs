using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLauncher : MonoBehaviour
{
    [SerializeField]GameObject bulletPrefab;
    [SerializeField]float bulletSpeed;
    [SerializeField]int numberOfShots;
    [SerializeField]int numberOfBullets;
    [SerializeField]float spreadAngle;
    [SerializeField]float fireTime;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        StartCoroutine(ShootMultiSpread());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShootMultiSpread()
    {
        while (true)
        {
            if (player.BulletSeverFlag == false)
            {
                for (int i = 0; i < numberOfShots; i++)
                {
                    ShootBullets();
                    yield return new WaitForSeconds(0.1f);
                }
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

            // ランダムな方向を取得
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            // 方向にランダムな偏差を追加
            direction = Quaternion.Euler(0, 0, Random.Range(-10f, 10f)) * direction;

            Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }
}
