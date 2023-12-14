using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushOnBulletCon : MonoBehaviour
{
    //Šp“x“ü‚ê‚é•Ï”
    private float _theta;
    //ƒÎ
    float PI = Mathf.PI;

    //Šp“x‚ğŒˆ‚ß‚Ä”­Ë‚·‚é
    public void ShootBulletWithCustomDirection(int i, GameObject obj, float speed)
    {
        // ’e‚ğ¶¬‚µ‚ÄA‰ŠúˆÊ’u‚ğİ’è
        GameObject bullet = Instantiate(obj, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(Mathf.Cos(i), Mathf.Sin(i));
        rb.velocity = dir * speed;
    }

    //Way’e
    public void ShootWayBullet(int spilt, float angle, GameObject obj, float speed)
    {
        for (int i = 0; i < spilt; i++)
        {
            //n-way’e‚Ì’[‚©‚ç’[‚Ü‚Å‚ÌŠp“x
            float AngleRange = PI * (angle / 180);
            if (AngleRange > 1) _theta = (AngleRange / (spilt)) * i + 0.5f * (PI - AngleRange);
            else _theta = 0.5f * PI;

            GameObject bullet = Instantiate(obj, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            TestLineBullet testLineBullet = bullet.GetComponent<TestLineBullet>();
            testLineBullet.BulletSpeed = speed;
            var bulletv = new Vector2(speed * Mathf.Cos(_theta), speed * Mathf.Sin(_theta));
            rb.velocity = bulletv;
        }
    }

    //‰ñ“]’e
    public void ShootBarrier(float angle, GameObject obj, float speed)
    {
        angle = angle * Mathf.Deg2Rad;
        GameObject bullet = Instantiate(obj, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rb.velocity = dir * speed;
    }

    //l‹÷ˆÚ“®’e”­Ë
    public void ShootCornerMove(GameObject cornerObj ,GameObject childObj, GameObject bulletObj)
    {
        GameObject bullet = Instantiate(bulletObj, childObj.transform.position, Quaternion.identity);
        CornerMoveBullet cornerMoveBullet = bullet.GetComponent<CornerMoveBullet>();
        cornerMoveBullet.CornerObject = cornerObj;
        cornerMoveBullet.InitializationPos = childObj.transform.position;
    }
}
