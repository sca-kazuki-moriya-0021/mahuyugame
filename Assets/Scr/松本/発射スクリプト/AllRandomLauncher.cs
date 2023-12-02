using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllRandomLauncher : MonoBehaviour
{
    [SerializeField]GameObject bulletPrefab;
    [SerializeField]float bulletSpeed;
    [SerializeField]int numberOfShots;
    [SerializeField]int numberOfBullets;
    [SerializeField]float spreadAngle;
    [SerializeField]float angleChange;
    [SerializeField]float fireTime;
    [SerializeField]float randomMoveTime;

    private List<Rigidbody2D> bulletsList = new List<Rigidbody2D>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootMultiSpreadAndMoveRandomly());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ShootMultiSpreadAndMoveRandomly()
    {
        while (true)
        {
            bulletsList.Clear();

            for(int i = 0;i < numberOfShots; i++)
            {
                ShootBullets();
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(fireTime);

            StopBullets();

            yield return new WaitForSeconds(randomMoveTime);

            MoveBulletsRandomly();
        }
    }

    private void ShootBullets()
    {
        float startAngle = -spreadAngle / 2;

        for(int i = 0;i < numberOfBullets; i++)
        {
            float angle = startAngle + i * (spreadAngle / (numberOfBullets));
            angle += angleChange;

            Vector3 direction = Quaternion.Euler(0,0,angle) * Vector3.up;
            GameObject bulletObject = Instantiate(bulletPrefab,transform.position,Quaternion.identity);
            Rigidbody2D bulletRigidbody = bulletObject.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;

            bulletsList.Add(bulletRigidbody);
        }
    }

    private void StopBullets()
    {
        foreach(Rigidbody2D bulletRigidbody in bulletsList.ToArray())
        {
            //’eŽ~‚ß
            if(bulletRigidbody != null)
            {
                bulletRigidbody.velocity = Vector2.zero;
            }
        }
    }

    private void MoveBulletsRandomly()
    {
        foreach(Rigidbody2D bulletRigidbody in bulletsList.ToArray())
        {
            //ƒ‰ƒ“ƒ_ƒ€ˆÚ“®
            if(bulletRigidbody != null)
            {
                Vector2 randomVelocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                bulletRigidbody.velocity = randomVelocity * bulletSpeed;
            }
        }
    }

}
