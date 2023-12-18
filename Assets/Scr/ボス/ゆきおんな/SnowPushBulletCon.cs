using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SnowPushBulletCon : MonoBehaviour
{
    //角度入れる変数
    private float _theta;
    //π
    float PI = Mathf.PI;

    //ディマーケイション用の親オブジェクト
    [SerializeField]
    private GameObject demarcationParentObject;

    [SerializeField]
    private GameObject gaoukenObject;

    private List<GameObject> objects = new List<GameObject>();

    //角度を決めて発射する
    public void ShootBulletWithCustomDirection(int i, GameObject obj, float speed)
    {
        // 弾を生成して、初期位置を設定
        GameObject bullet = Instantiate(obj, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(Mathf.Cos(i), Mathf.Sin(i));
        rb.velocity = dir * speed;
    }

    //Way弾
    public void ShootWayBullet(int spilt, float angle, GameObject obj, float speed)
    {
        for (int i = 0; i < spilt; i++)
        {
            //n-way弾の端から端までの角度
            float AngleRange = PI * (angle / 180);
            if (AngleRange > 1) _theta = (AngleRange / (spilt)) * i + 0.5f * (PI - AngleRange);
            else _theta = 0.5f * PI;

            GameObject bullet = Instantiate(obj, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            var bulletv = new Vector2(speed * Mathf.Cos(_theta), speed * Mathf.Sin(_theta));
            rb.velocity = bulletv;
        }
    }

    //回転弾
    public void ShootBarrier(float angle, GameObject obj, float speed)
    {
        angle = angle * Mathf.Deg2Rad;
        GameObject bullet = Instantiate(obj, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rb.velocity = dir * speed;
    }

    //四隅移動弾発射
    //クランベリートラップ
    public void ShootCornerMove(GameObject cornerObj ,GameObject childObj, GameObject bulletObj,float speed)
    {
        GameObject bullet = Instantiate(bulletObj, childObj.transform.position, Quaternion.identity);
        CornerMoveBullet cornerMoveBullet = bullet.GetComponent<CornerMoveBullet>();
        cornerMoveBullet.CornerObject = cornerObj;
        cornerMoveBullet.InitializationPos = childObj.transform.position;
    }

    //ディマーケイション
    public void ShootDemarcation(float sign)
    {
        GameObject parent = Instantiate(demarcationParentObject,transform.position,quaternion.identity);
        objects.Add(parent);
        DemarcationParent parent_cs = parent.GetComponent<DemarcationParent>();
        parent_cs.Sign = sign;
    }

    //ディマーケイションで出てきたオブジェクト消す変数
    public void DestoryDemarcation()
    {
        Debug.Log("消した");
        for(int i = 0; i < objects.Count; i++)
        {
            var g = objects[i];
            Destroy(g.gameObject);
        }
        objects.Clear();
    }

    //餓王剣「餓鬼十王の報い」
    public void GaoukenShoot()
    {
        Instantiate(gaoukenObject,transform.position,quaternion.identity);
    }
}
