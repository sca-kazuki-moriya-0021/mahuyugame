using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CornerChildBullet : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private SnowFairyBulletCon snow;
    // Start is called before the first frame update
    void Start()
    {
        snow = FindObjectOfType<SnowFairyBulletCon>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(snow.BulletDeleteFlag[0] == true)
            Destroy(this.gameObject);
    }


    void OnBecameInvisible()
    {
       Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        Destroy(this.gameObject);
    }


}
