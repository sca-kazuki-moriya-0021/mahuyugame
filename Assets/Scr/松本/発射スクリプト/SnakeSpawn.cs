using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawn : MonoBehaviour
{
    [SerializeField,Header("’e‚ÌƒvƒŒƒnƒu")]
    GameObject SnakeBulletPrefab;
    [SerializeField,Header("”­ŽËŠÔŠu")]
    float fireRate = 0.2f;
    private float nextFireTime;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            SpownSnakeBullet();
        }
    }

    void SpownSnakeBullet()
    {
        GameObject bullet = Instantiate(SnakeBulletPrefab,transform.position,Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(1,0);
    }
}
