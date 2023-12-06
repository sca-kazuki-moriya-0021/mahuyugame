using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f; // 移動速度
    [SerializeField] private float amplitudeX; // X軸の振幅
    [SerializeField] private float amplitudeY; // Y軸の振幅
    [SerializeField] private float stopTime;
    [SerializeField] private Transform centerObject;
    private float stopCountTime;
    private bool bossAttack1 = false;
    private bool bossAttack2 = false;

    private float angle;
    
    //private Vector3 startPos;
    private bool isMoving = true;

    //スクリプト取得
    private Player player;
    private BossCollder bossCollder;

    public bool BossAttack1
    {
        get { return this.bossAttack1; }
        set { this.bossAttack1 = value; }
    }
    public bool BossAttack2
    {
        get { return this.bossAttack2; }
        set { this.bossAttack2 = value; }
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        bossCollder = FindObjectOfType<BossCollder>();
    }

    void Update()
    {
        //Debug.Log(isMoving);
        //Debug.Log(player.BussMoveStopFlag);
        
        if(bossAttack1 == false && isMoving == true && bossCollder.BossDeathFlag == false)
        {
            Move();
        }
        //プレイヤーの移動停止スキルが発動していなかった時、死んでいなかった時は動く
        if (player.BussMoveStopFlag == true)
        {
            StopMove();
        }
    }

    private void Move()
    {
        if (bossCollder.BossDebuffFlag == true)
        {
            angle += Time.deltaTime * speed * 0.1f;
            float x = Mathf.Sin(angle * 2) * amplitudeX * 0.5f;
            float y = Mathf.Sin(angle) * amplitudeY * 0.5f;
            // Z軸の位置は固定（2D空間に固定）
            Vector3 offset = new Vector3(x, y, 0);
            Vector3 newPosition = centerObject.position + offset;

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 1f);
        }
        else
        {
            angle += Time.deltaTime * speed;
            float x = Mathf.Sin(angle * 2) * amplitudeX;
            float y = Mathf.Sin(angle) * amplitudeY;
            // Z軸の位置は固定（2D空間に固定）
            Vector3 offset = new Vector3(x, y, 0);
            Vector3 newPosition = centerObject.position + offset;

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 10f);
        }
    }     

    private void StopMove()
    {
        if(stopCountTime <= stopTime)
        {
            stopCountTime +=Time.deltaTime;
            isMoving = false;
            if (stopCountTime >= stopTime)
            {
                stopCountTime = 0;
                player.BussMoveStopFlag = false;
                isMoving = true;
            }
        }
    }

}
