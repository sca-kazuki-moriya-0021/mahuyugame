using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GaoukenObjectCon : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    //Way弾の発射角度
    [SerializeField]
    private float[] launchWayAngle;

    //スピード
    [SerializeField]
    private float bulletSpeed;

    //角度入れる変数
    private float _theta;
    //π
    float PI = Mathf.PI;

    // Start is called before the first frame update
    void Start()
    {
        //角度をラジアンに変換
        for (int i = 0; i < launchWayAngle.Length; i++)
        {
            launchWayAngle[i] = launchWayAngle[i] * Mathf.Deg2Rad;
        }

        for (int i = 0; i < 3; i++)
        {
            transform.DOMove(new Vector3(-1 + i,0,0),0.1f);
            //BulletIns();
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BulletIns()
    {
        for(int i = 0; i <launchWayAngle.Length; i++)
        {
            Vector3 dir = new Vector2(Mathf.Cos(launchWayAngle[i]), Mathf.Sin(launchWayAngle[i]));
            dir.z = 0;
            //弾インスタンスを取得し、初速と発射角度を与える
            GameObject bullet_obj = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody2D rigidbody2D = bullet_obj.GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = dir * bulletSpeed;
        }
    }

}
