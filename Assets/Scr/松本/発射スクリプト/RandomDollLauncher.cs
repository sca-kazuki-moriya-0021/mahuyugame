using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDollLauncher : MonoBehaviour
{
    [SerializeField]GameObject bulletPrefab;
    [SerializeField]Transform RangeA;
    [SerializeField]Transform RangeB;
    [SerializeField]float bulletSpeed;
    [SerializeField]int numberOfBullets;
    [SerializeField]float spreadAngle;
    [SerializeField]float fireTime;
    [SerializeField]int numBullet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateBullets());
    }

    private IEnumerator GenerateBullets()
    {
        while (true)
        {
            for(int i = 0; i < 2; i++)
            {
                ShootBullet();
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(fireTime);
        }
    }

    private void ShootBullet()
    {
        float startAngle = -spreadAngle / 2;

        float x = Random.Range(RangeA.position.x, RangeB.position.x);
        float y = Random.Range(RangeA.position.y, RangeB.position.y);
        float z = Random.Range(RangeA.position.z, RangeB.position.z);
        for(int i = 0; i < numberOfBullets; i++)
        {
            float angle = startAngle + i * (spreadAngle / (numberOfBullets));

            Vector3 direction = Quaternion.Euler(0,0,angle) * Vector3.up;
            Rigidbody2D bulletRigidbody = Instantiate(bulletPrefab,new Vector3(x,y,z),Quaternion.identity).GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * bulletSpeed;
        }
    }
}
