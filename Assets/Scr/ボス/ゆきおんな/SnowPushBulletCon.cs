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

    [SerializeField]
    private GameObject[] shuraObject;

    private int geoglyphShootCount;

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
    public void GaoukenShoot(int identifier)
    {
        var r  = new Vector3(0,0,0);
        switch (identifier)
        {
            case 0:
                r = new Vector3(0,5,0);
            break;
            case 1:
                r = new Vector3(0,-5 , 0);
            break;
        }
        transform.TransformPoint(r);
        GameObject i = Instantiate(gaoukenObject,r,quaternion.identity);
        GaoukenObjectCon objectCon = i.GetComponent<GaoukenObjectCon>();
        objectCon.Identifier = identifier;
    }

    //修羅剣「現世妄執-Lunatic-」
    public void ShuraShoot(int number,int i)
    {
        GameObject o = Instantiate(shuraObject[number],new Vector3(0,5 * i,0),quaternion.identity);
        objects.Add(o);
    }

    //修羅剣で出したオブジェクト削除
    public void DestroyShuraShoot()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            var g = objects[i];
            Destroy(g.gameObject);
        }
        objects.Clear();
    }

    //雪の結晶弾
    public void SnowCrystal(float angle,int numberOfBullets,GameObject bullet,float speed)
    {
        float startAngle = - angle / 2;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float v = startAngle + i * (angle / (numberOfBullets));

            // 弾を発射
            Vector3 direction = Quaternion.Euler(0, 0, v) * Vector3.up;
            Rigidbody2D bulletRigidbody = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * speed;
        }
    }

    //地上穢の純化
    public void GeoglyphShoot(float angle,int spilt,GameObject obj, float speed,Vector3 pos)
    {
        geoglyphShootCount++;
        for (int i = 0; i < spilt; i++)
        {
            //n-way弾の端から端までの角度
            float AngleRange = PI * (angle / 180);
            if (AngleRange > 1) _theta = (AngleRange / (spilt)) * i + 0.5f * (PI - AngleRange);
            else _theta = 0.5f * PI;

            GameObject bullet = Instantiate(obj, pos, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            GeoglyphBullet bullet_cs = bullet.GetComponent<GeoglyphBullet>();
            bullet_cs.ShootCount = geoglyphShootCount;
            var bulletv = new Vector2(speed * Mathf.Cos(_theta), speed * Mathf.Sin(_theta));
            rb.velocity = bulletv;
            
        }
    }
}
