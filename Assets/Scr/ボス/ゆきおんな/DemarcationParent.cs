using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using DG.Tweening;

public class DemarcationParent : MonoBehaviour
{
    [Tooltip("‰ñ“]‘¬“x(deg/sec)"), SerializeField] private float rotateSpeed;
    [Tooltip("‰~‚ÌŠg‘å‘¬“x(uu/sec)"), SerializeField] private float radiusSpeed;
    [Tooltip("ŠJn”¼Œa"), SerializeField] private float startRaduis;
    [Tooltip("Å‘å”¼Œa"), SerializeField] private float maxRaduis;
    [Tooltip("’e‚ÌPrefab"), SerializeField] private GameObject bulletPrefab;
    [Tooltip("’e‚Ì”"), SerializeField] private int bulletCount;

    private float sign;

    private List<GameObject> bullets;
    private float nowRadius;            //  Œ»İ‚Ì”¼Œa
    private float bulletDelta;          //  ’e‚ÌŠÔŠu
    private float radiusDelta;          //  ”¼Œa‚ÌˆÚ“®‘¬“x(uu/sec)

    public float Sign
    {
        get { return this.sign; }
        set { this.sign = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        nowRadius = startRaduis;

        //  Delta‚Ìİ’è(’e‚ÌŠÔŠuE”¼Œa‚Ì‘¬“x(ƒtƒŒ[ƒ€’PˆÊ))
        bulletDelta = (360.0f / bulletCount);
        radiusDelta = (maxRaduis - nowRadius) / radiusSpeed;

        //  ’e‚Ì¶¬
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
        //  ‰~‚Ì”¼Œa‚ğL‚°‚é
        nowRadius += (radiusDelta * Time.deltaTime);
        if (nowRadius > maxRaduis)
        {
            nowRadius = maxRaduis;
        }

        //  ’e‚ÌˆÊ’u‚ğXV‚·‚é(ƒ[ƒJƒ‹À•W‚Ì•ÏX)
        for (var i = 0; i < bullets.Count; i++)
        {
            var pos = CalcBulletPositon(nowRadius, bulletDelta * i);
            bullets[i].transform.localPosition = pos;
        }

        //  e‚ğ‰ñ“]‚³‚¹‚é
        transform.Rotate(new Vector3(0.0f, 0.0f, 360.0f * sign *(Time.deltaTime / rotateSpeed)));
    }

    //  ’e‚ÌˆÊ’u‚ğŒvZ‚·‚é
    private Vector2 CalcBulletPositon(float radius, float angle)
    {
        var direction = Quaternion.Euler(0.0f, 0.0f, angle) * Vector2.up;
        direction *= radius;

        return new Vector2(direction.x, direction.y);
    }
}
