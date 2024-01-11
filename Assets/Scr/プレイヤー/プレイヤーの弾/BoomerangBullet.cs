using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet : MonoBehaviour
{
    //角度計算と速度
    private Vector3 angle;
    private int number;
    private Vector3 endPosition;

    //中間位置
    private Vector3 middlePostion;

    //プレイヤー取得
    private GameObject player;

    //初期位置
    private Vector3 stratPos;

    //状態遷移
    private STATE state;

    //時間計測用
    private float time;

    private Rigidbody2D rb2d;

    public Vector3 Angle { get => angle; set => angle = value; }
    public int Number {get => number; set => number = value; }
    public Vector3 EndPosition {get => endPosition; set => endPosition = value;}

    enum STATE
    {
        Start,
        End,
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        stratPos = transform.position;

        state = STATE.Start;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; 
        var p = player.transform.position;
        //行き
        if(state == STATE.Start)
        {
            //発射位置から中心座標を求める
            switch (number)
            {
                case 0:
                    middlePostion = new Vector3(angle.x,angle.y+5f,0);
                    break;
                case 1:
                    break;
                case 2:
                    middlePostion = new Vector3(angle.x,angle.y-5f,0);
                 break;
            }
            //移動
            var a = Vector3.Lerp(stratPos,middlePostion,time);
            var b = Vector3.Lerp(middlePostion,endPosition,time);
            this.transform.position = Vector3.Lerp(a,b,time);

            //終点位置まで行ったらいったんtimeリセットとstate変更
            if(transform.position == endPosition)
            {
                state = STATE.End;
                time = 0;
            }
        }
        //帰り
        if(state == STATE.End)
        {
            //帰ってくるときに使う中間位置指定
            switch (number)
            {
                case 0:
                    middlePostion = new Vector3(angle.x, angle.y - 5f, 0);
                    break;
                case 1:
                    break;
                case 2:
                    middlePostion = new Vector3(angle.x, angle.y + 5f, 0);
                    break;
            }

            //移動
            var a = Vector3.Lerp(endPosition, middlePostion, time);
            var b = Vector3.Lerp(middlePostion, p, time);
            this.transform.position = Vector3.Lerp(a, b,time);

            //プレイヤーの位置になったら弾を消す
            if(player.transform.position == transform.position)
                Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss") && state == STATE.End)
            Destroy(this.gameObject);
    }
}
