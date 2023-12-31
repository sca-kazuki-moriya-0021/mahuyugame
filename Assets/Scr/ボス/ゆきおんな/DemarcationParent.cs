using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using DG.Tweening;

public class DemarcationParent : MonoBehaviour
{
    [Tooltip("回転速度(deg/sec)"), SerializeField] private float rotateSpeed;
    [Tooltip("円の拡大速度(uu/sec)"), SerializeField] private float radiusSpeed;
    [Tooltip("開始半径"), SerializeField] private float startRaduis;
    [Tooltip("最大半径"), SerializeField] private float maxRaduis;
    [Tooltip("弾のPrefab"), SerializeField] private GameObject bulletPrefab;
    [Tooltip("弾の数"), SerializeField] private int bulletCount;

    private float sign;

    private List<GameObject> bullets;
    private float nowRadius;            //  現在の半径
    private float bulletDelta;          //  弾の間隔
    private float radiusDelta;          //  半径の移動速度(uu/sec)

    public float Sign
    {
        get { return this.sign; }
        set { this.sign = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        nowRadius = startRaduis;

        //  Deltaの設定(弾の間隔・半径の速度(フレーム単位))
        bulletDelta = (360.0f / bulletCount);
        radiusDelta = (maxRaduis - nowRadius) / radiusSpeed;

        //  弾の生成
        bullets = new List<GameObject>();
        for (var i = 0; i < bulletCount; i++)
        {
            var pos = CalcBulletPositon(nowRadius, bulletDelta * i);
            var bullet = Instantiate(bulletPrefab, pos, quaternion.identity, transform);
            bullets.Add(bullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //  円の半径を広げる
        nowRadius += (radiusDelta * Time.deltaTime);
        if (nowRadius > maxRaduis)
        {
            nowRadius = maxRaduis;
        }

        //  弾の位置を更新する(ローカル座標の変更)
        for (var i = 0; i < bullets.Count; i++)
        {
            var pos = CalcBulletPositon(nowRadius, bulletDelta * i);
            bullets[i].transform.localPosition = pos;
        }

        //  親を回転させる
        transform.Rotate(new Vector3(0.0f, 0.0f, 360.0f * sign *(Time.deltaTime / rotateSpeed)));
    }

    //  弾の位置を計算する
    private Vector2 CalcBulletPositon(float radius, float angle)
    {
        var direction = Quaternion.Euler(0.0f, 0.0f, angle) * Vector2.up;
        direction *= radius;

        return new Vector2(direction.x, direction.y);
    }
}
