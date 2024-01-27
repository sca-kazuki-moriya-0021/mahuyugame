using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    //エフェクト部分が消えたかどうか
    private bool laser;

    private float time;

    //速度と角度
    private float velocity;
    private Vector3 angle;

    //保存用
    private Vector3 lastVelocity = Vector3.zero;

    private Rigidbody2D rb2d;

    //壁とたまに当たった回数
    private int count = 0;
    public float Velocity { get => velocity; set => velocity = value; }
    public Vector3 Angle { get => angle; set => angle = value; }

    // Start is called before the first frame update
    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();
       //角度を考慮して弾の速度・角度計算
       Vector3 bulletV = rb2d.velocity;
       bulletV = velocity * angle;
       bulletV.z = 0;
       rb2d.velocity = bulletV;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 4f)
            Destroy(this.gameObject);

        //現在の角度等を保存しておく
        lastVelocity = rb2d.velocity;
        if(count >= 4)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //反射させるプログラム
        if (collision.gameObject.CompareTag("OutLine") || collision.gameObject.CompareTag("PlayerBullet"))
        {
            Vector3 normal = collision.contacts[0].normal;
            Vector3 refrect = Vector3.Reflect(lastVelocity,normal);
            refrect.z = 0;
            rb2d.velocity = refrect;
            lastVelocity = Vector3.zero;
            count ++;
        }

       /*if (collision.gameObject.CompareTag("DestroyBullet"))
        {
            power = 0;
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("DestroyBullet"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }*/

        if (collision.gameObject.CompareTag("Boss"))
            Destroy(this.gameObject);
    }

    void OnBecameInvisible()
    {
        if(laser == false)
            Destroy(this.gameObject);
    }

}
