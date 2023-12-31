using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFairyMove : MonoBehaviour
{
    private float speed = 1f;
    [SerializeField]
    private float stopTime;
    private float stopCountTime;

    //移動指定の親オブジェクト
    [SerializeField]
    private GameObject movePos;
    private GameObject[] movePosChild = new GameObject[] { null, null, null, null };
    //動き制御
    private int moveCount = 0;

    private Rigidbody2D rigidbody;
    private bool moveColFlag;

    private Player player;
    private BossCollder bossCollder;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        rigidbody = GetComponent<Rigidbody2D>();
        bossCollder = FindObjectOfType<BossCollder>();
        for(int i = 0; i < movePosChild.Length;i++)
        {
            movePosChild[i] = movePos.transform.GetChild(i).gameObject;
        }
        transform.position = movePosChild[0].transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossCollder.BossDeathFlag == false &&  player.BussMoveStopFlag == false)
        {
            if (moveCount <= 120)
            {
                //移動管理
                switch (moveCount % 4)
                {
                    case 0 when moveColFlag == false:
                        StartCoroutine(MovePosition(0, 1));
                        break;
                    case 1 when moveColFlag == false:
                        StartCoroutine(MovePosition(1, 2));
                        break;
                    case 2 when moveColFlag == false:
                        StartCoroutine(MovePosition(2, 3));
                        break;
                    case 3 when moveColFlag == false:
                        StartCoroutine(MovePosition(3, 0));
                        break;
                }
            }
            else
            {
                //移動管理
                switch (moveCount % 4)
                {
                    case 0 when moveColFlag == false:
                        StartCoroutine(MovePosition(0, 2));
                        break;
                    case 1 when moveColFlag == false:
                        StartCoroutine(MovePosition(2, 1));
                        break;
                    case 2 when moveColFlag == false:
                        StartCoroutine(MovePosition(1, 3));
                        break;
                    case 3 when moveColFlag == false:
                        StartCoroutine(MovePosition(3, 0));
                        break;
                }
            }
        }
        //時とめしたら
        if(player.BussMoveStopFlag == true)
        StopMove();
    }

    //動き停止
    private void StopMove()
    {
        if (stopCountTime <= stopTime)
        {
            stopCountTime += Time.deltaTime;
            if (stopCountTime >= stopTime)
            {
                stopCountTime = 0;
                player.BussMoveStopFlag = false;
            }
        }
    }
    
    //移動調整
    private IEnumerator MovePosition(int a ,int b)
    {
        float time = 0;
        var i = 0; 
        moveColFlag = true;
        float dir = Mathf.Abs(Vector3.Distance(movePosChild[a].transform.position,movePosChild[b].transform.position));
        if (bossCollder.BossDebuffFlag == true)
            i = 2;
        else
            i = 4;

        //float pos = (Time.time * speed) / dir;
        while (movePosChild[b].transform.position != transform.position)
        {
            time += Time.deltaTime;
            float pos = (time * speed * i) / dir;
            transform.position = Vector3.Lerp(movePosChild[a].transform.position, movePosChild[b].transform.position, pos);
            yield return null;
        }
        moveCount++;
        moveColFlag = false;
        StopCoroutine(MovePosition(a,b));
    }
}
