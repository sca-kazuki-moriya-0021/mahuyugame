using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllspiralLauncher : MonoBehaviour
{
    [SerializeField]GameObject bulletPrefab;
    [SerializeField]int numberOfShots;
    [SerializeField]int numberOfBullets;
    [SerializeField]float bulletSpeed;
    [SerializeField]float fireInterval;
    [SerializeField]float shootInterval;
    [SerializeField]float spiralRotationSpeed;
    [SerializeField]float spiralDistance;

    private int shotCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootMultiSpread());
    }

    private IEnumerator ShootMultiSpread()
    {
        while (true)
        {
            for(int i = 0;i < numberOfShots; i++)
            {
                ShootBullets();
                yield return new WaitForSeconds(fireInterval);
            }

            shotCount++;
            yield return new WaitForSeconds(shootInterval);

            UpdateSpiral();
        }
    }

    private void UpdateSpiral()
    {
        int newRotationSpeed = Random.Range(180,450);
        float newSpiralDistance = Random.Range(5f,10f);
        int newSpeed = Random.Range(3,7);

        bulletSpeed = newSpeed;
        spiralRotationSpeed = newRotationSpeed;
        spiralDistance = newSpiralDistance;
    }

    private void ShootBullets()
    {
        for(int i = 0;i < numberOfBullets; i++)
        {
            float angle = i * (360f / numberOfBullets);

            Vector3 direction = Quaternion.Euler(0,0,angle) * Vector3.up;
            GameObject bullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

            StartCoroutine(SpiralMotion(bulletRigidbody,direction));
        }
    }

    private IEnumerator SpiralMotion(Rigidbody2D bulletRigidbody,Vector3 initialDirection)
    {
        float distanceTraveled = 0f;
        Vector3 startPosition = bulletRigidbody.position;
        int rotationDirection = (shotCount % 2 == 0) ? 1 : -1;

        while(distanceTraveled < spiralDistance)
        {
            float angle = distanceTraveled * rotationDirection * spiralRotationSpeed / spiralDistance;

            Vector3 spiralMotion = Quaternion.Euler(0, 0, angle) * initialDirection;
            bulletRigidbody.velocity = spiralMotion * bulletSpeed;

            distanceTraveled = Vector3.Distance(bulletRigidbody.position, startPosition);

            yield return null;
        }
    }
}
