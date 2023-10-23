using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{

    //それぞれの位置を保存する変数
    //スタート地点
    private Vector2 charaPos;
    public Vector2 CharaPos { set { charaPos = value; } }
    //ゴール地点
    private Vector2 playerPos;
    public Vector2 PlayerPos { set { playerPos = value; } }
    //中継地点
    private Vector2 greenPos;
    public Vector2 GreenPos { set { greenPos = value; } }
    //進む割合を管理する変数
    private float time;



    // Update is called once per frame
    void Update()
    {
        //弾の進む割合をTime.deltaTimeで決める
        time += Time.deltaTime;

        //二次ベジェ曲線
        //スタート地点から中継地点までのベクトル上を通る点の現在の位置
        var a = Vector3.Lerp(charaPos, greenPos, time);
        //中継地点からターゲットまでのベクトル上を通る点の現在の位置
        var b = Vector3.Lerp(greenPos, playerPos, time);
        //上の二つの点を結んだベクトル上を通る点の現在の位置（弾の位置）
        this.transform.position = Vector3.Lerp(a, b, time);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
