using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpLauncher : MonoBehaviour
{
    [SerializeField]GameObject spBulletPrefab;
    [SerializeField]private float timeBetweenBullets = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBullets());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnBullets()
    {
        int count = 0;

        while (true)
        {
            GameObject bullet = Instantiate(spBulletPrefab, transform.position, Quaternion.identity);
            SpBullet bullet_cs = bullet.GetComponent<SpBullet>();
            bullet_cs.count = count;
            bullet_cs.insCount = count;
            yield return new WaitForSeconds(timeBetweenBullets);
            count++;
        }
    }
}
