using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFairyBulletCon : MonoBehaviour
{
    //弾オブジェクト
    [SerializeField]
    private GameObject[] bullets;
    //弾のスピード
    [SerializeField]
    private float[] bulletSpeed;
    //弾の発射角度
    [SerializeField, Range(0, 360)]
    private float[] launchAngle;
    //弾の発射感覚
    [SerializeField]
    private float fireTime;

    private float time = 0;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > fireTime)
        {
            ShootBulletWithCustomDirection(count);
            ShootBulletWithCustomDirection(-count);
            count += 1;
            time = 0;
        }

        if(time > 120)
        {

        }


    }

    private void ShootBulletWithCustomDirection(int i)
    {
        // 弾を生成して、初期位置を設定
        GameObject bullet = Instantiate(bullets[0], transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(Mathf.Cos(i),Mathf.Sin(i));
        //Debug.Log(count);

        // 弾の向きをカスタマイズするために、弾の角度を変更
       // bullet.transform.rotation = Quaternion.Euler(0, 0, launchAngle[0] + i);

        // 弾の速度ベクトルを設定して、指定された角度に飛ばす
        //var bulletDirection = Quaternion.Euler(0, 0, launchAngle[0] + i);
        rb.velocity = dir * bulletSpeed[0];
    }
}
