using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaoukenBullet : MonoBehaviour
{
    private Vector3 dir;

    private float speed = 2f;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    private SnowFairyBulletCon snow;

    public Vector3 Dir
    {
        get { return this.dir; }
        set { this.dir = value; }
    }

    public float Speed
    {
        get { return this.speed; }
        set { this.speed = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        snow = FindObjectOfType<SnowFairyBulletCon>();

        rigidbody2D.velocity = dir * speed;
    }

    // Update is called once per frame
    void Update()
    {
       if(snow.ReduceSpeedFlag == true)
       {
            rigidbody2D.velocity = dir * speed *0.5f;
       }
       else
       {
            rigidbody2D.velocity = dir * speed;
       }
    }
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
