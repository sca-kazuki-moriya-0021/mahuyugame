using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLgaBullet : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float speed;
    [SerializeField]
    float RandomBulletIntervalMin;
    [SerializeField]
    float RandomBulletIntervalMax;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(TimeLag());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TimeLag()
    {
        yield return new WaitForSeconds(Random.Range(RandomBulletIntervalMin,RandomBulletIntervalMax));

        if(target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
        Destroy(gameObject);
    }
}
