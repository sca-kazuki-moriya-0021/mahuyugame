using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GaoukenObjectCon : MonoBehaviour
{
    //弾のプレハブ
    [SerializeField]
    private GameObject bullet;

    //Way弾の発射角度
    [SerializeField]
    private float[] launchWayAngle;

    //スピード
    [SerializeField]
    private float bulletSpeed;

    //自分の位置保存用
    private Vector3 pos;

    //前いた座標保存用
    private List<Vector3> oldPos = new List<Vector3>();

    //Tween保存用
    private Tween tween = null;

    // Start is called before the first frame update
    void Start()
    {
        //角度をラジアンに変換
        for (int i = 0; i < launchWayAngle.Length; i++)
        {
            launchWayAngle[i] = launchWayAngle[i] * Mathf.Deg2Rad;
        }

        pos = transform.position;

        //Tweenで動かし、そのTweenを保存する
        if (pos.y > 0)
            tween = this.transform.DOMove(new Vector3(0,transform.position.y -10f,0),3f);
        else
            tween = this.transform.DOMove(new Vector3(0, transform.position.y + 10f, 0), 3f);

        //再生
        tween.Play();

    }

    // Update is called once per frame
    void Update()
    {
        //前に保存した座標と今の座標を比較して、1より大きいときはListにその座標を保存
        var t = transform.position;
        if(Vector3.Distance(t,pos) >= 1)
        {
            oldPos.Add(t);
            pos = t;
        }

        //Tweenが終わったらコルーチン発動
        tween.OnComplete(() => {StartCoroutine(BulletIns());});

        //  親を回転させる
        transform.Rotate(new Vector3(0.0f, 0.0f, 360.0f * (Time.deltaTime / 2f)));
    }

    //保存した座標から弾を出す
    private IEnumerator BulletIns()
    {
        Debug.Log("はいったよ");
        var count =0;
        while(count < 2)
        {
            for (int x = 0; x < oldPos.Count; x++)
            {
                for (int i = 0; i < launchWayAngle.Length; i++)
                {
                    Vector3 dir = new Vector2(Mathf.Cos(launchWayAngle[i]), Mathf.Sin(launchWayAngle[i]));
                    dir.z = 0;
                    GameObject bullet_obj = (GameObject)Instantiate(bullet, oldPos[x], Quaternion.identity);
                    GaoukenBullet gaoukenBullet = bullet_obj.GetComponent<GaoukenBullet>();
                    gaoukenBullet.Dir = dir;
                    if (count == 0)
                        gaoukenBullet.Speed = 1.5f;
                    else
                        gaoukenBullet.Speed = 0.8f; 
                }
                yield return new WaitForSeconds(0.2f);
            }
            count++;
        }
        
        
        StopCoroutine(BulletIns());
    }

}
