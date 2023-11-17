using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private float speed; // 移動速度
    [SerializeField] private float amplitudeX; // X軸の振幅
    [SerializeField] private float amplitudeY; // Y軸の振幅
    [SerializeField] private float stopTime;
    [SerializeField] private float debuffTime;
    [SerializeField] private GameObject centerObject;
    private float stopCountTime;
    private float debuffCountTime;
    private bool bossAttack1 = false;
    private bool bossAttack2 = false;
   
    private float angle;
    
    private Vector3 startPos;
    private bool isMoving = true;
    private bool debuffFlag = false;

    [SerializeField]
    private float hp;

    //プレイヤー取得
    private Player player;
    private SoundManager soundManager;
    private AreaManager areaManager;

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
        soundManager = FindObjectOfType<SoundManager>();
        areaManager = FindObjectOfType<AreaManager>();
        
        soundManager.BossPhaseFlag = true;
        areaManager.BossActiveFlag = true;
    }

    void Update()
    {
        //Debug.Log(isMoving);
        //Debug.Log(player.BussMoveStopFlag);
        
        if(isMoving == true)
        {
            Move();
        }
        //プレイヤーの移動停止スキルが発動していなかった時は動く
        if (player.BussMoveStopFlag == true)
        {
            StopMove();
        }
        //デバフで移動速度の低下
        if (debuffFlag == true)
        {
            Debuff();
        }
    }

    private void Move()
    {
        if (debuffFlag == true)
        {
            angle += Time.deltaTime * speed * 0.1f;
            float x = Mathf.Sin(angle * 2) * amplitudeX * 0.5f;
            float y = Mathf.Sin(angle) * amplitudeY * 0.5f;
            // Z軸の位置は固定（2D空間に固定）
            Vector3 offset = new Vector3(x, y, 0);
            Vector3 newPosition = centerObject.transform.position + offset;

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 1f);
        }
        else
        {
            angle += Time.deltaTime * speed;
            float x = Mathf.Sin(angle * 2) * amplitudeX;
            float y = Mathf.Sin(angle) * amplitudeY;
            //Debug.Log(y);
            // Z軸の位置は固定（2D空間に固定）
            Vector3 offset = new Vector3(x, y, 0);
            Vector3 newPosition = centerObject.transform.position + offset;

            this.transform.position = Vector3.Lerp(this.transform.position, newPosition, Time.deltaTime * 1f);
            
            
            //Debug.Log(transform.position);
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

    private void Debuff()
    {
        if (debuffCountTime <= debuffTime)
        {
            debuffCountTime += Time.deltaTime;

            if (debuffCountTime > debuffTime)
            {
                debuffCountTime = 0;
                debuffFlag = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            HitBullet();
        }

        if (collision.gameObject.CompareTag("PlayerSkillBullet"))
        {
            Debug.Log("デバフ入った");
            Destroy(collision.gameObject);
            debuffFlag = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            HitBullet();
        }
    }

    private void HitBullet()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
        if (debuffFlag == true)
            hp -= 2;
        else
            hp--;
    }
}
