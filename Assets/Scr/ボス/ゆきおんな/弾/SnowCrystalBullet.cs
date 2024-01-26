using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowCrystalBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private int numberOfShots;
    [SerializeField]
    private int numberOfBullets;
    [SerializeField]
    private float spreadAngle;
    [SerializeField]
    private float fireTime;

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

            // �e�𔭎�
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
