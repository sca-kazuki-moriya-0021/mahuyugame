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
    [SerializeField] private float debuffTime;
    [SerializeField] private Transform centerObject;
    private float stopCountTime;
    private float debuffCountTime;
    private bool bossAttack1 = false;
    private bool bossAttack2 = false;

    private float angle;
    
    //private Vector3 startPos;
    private bool isMoving = true;
    private bool debuffFlag = false;
    //死亡フラグ
    private bool bossDeathFlag = false;

    [SerializeField]
    private float hp;
    //自分が撃破された時にドロップするアイテム
    [SerializeField]
    private GameObject dropItem;
    //死亡するアニメーション名
    [SerializeField]
    private string deathAnimation;
    //ゲームオブジェクトに設定されているSkeletonAnimation
    [SerializeField]
    private SkeletonAnimation skeletonAnimation = default;
    //Spineアニメーションを適用するために必要なAnimationState
    private Spine.AnimationState spineAnimationState = default;

    //スクリプト取得
    private Player player;
    private PlayerCollider playerCollider;
    private AreaManager areaManager;
    private NowLoading nowLoading;

    public bool BossDeathFlag
    {
        get { return this.bossDeathFlag; }
        set { this.bossDeathFlag = value; }
    }

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
        areaManager = FindObjectOfType<AreaManager>();
        nowLoading = FindObjectOfType<NowLoading>();
        playerCollider = FindObjectOfType<PlayerCollider>();

        // SkeletonAnimationからAnimationStateを取得
        spineAnimationState = skeletonAnimation.AnimationState;
    }

    void Update()
    {
        //Debug.Log(isMoving);
        //Debug.Log(player.BussMoveStopFlag);
        
        if(bossAttack1 == false && isMoving == true)
        {
            Move();
        }
        //プレイヤーの移動停止スキルが発動していなかった時、死んでいなかった時は動く
        if (player.BussMoveStopFlag == true || bossDeathFlag == true)
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
        var a = 0f;
        if (debuffFlag == true)
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
            //プレイヤーの体力が0じゃない時
            if(playerCollider.DeathFlag == false)
            HitBullet();
        }

        if (collision.gameObject.CompareTag("PlayerSkillBullet"))
        {
            Destroy(collision.gameObject);
            //上と同じ
            if (playerCollider.DeathFlag == false)
            {
                debuffFlag = true;
                Debug.Log("デバフ入った");
            }
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
            StartCoroutine(DropItemInstance());
        }
        if (debuffFlag == true)
            hp -= 2;
        else
            hp--;
    }

    //アイテムドロップ
    private IEnumerator DropItemInstance()
    {
        bossDeathFlag = true;
        for(int i = 0;i < 10; i++)
        {
            Instantiate(dropItem,transform.position,Quaternion.identity);
            yield return null;
        }
        spineAnimationState.SetAnimation(0, deathAnimation, false);
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        nowLoading.FadeIn();
        StopCoroutine(DropItemInstance());
    }
}
