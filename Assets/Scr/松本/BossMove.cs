using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f; // 移動速度
    [SerializeField] private float amplitudeX; // X軸の振幅
    [SerializeField] private float amplitudeY; // Y軸の振幅
    [SerializeField] private GameObject[] skillPrefabs; // スキルの弾幕のプレハブ配列
    [SerializeField] private float skillSwitchInterval = 30.0f; // スキル切り替えの間隔（秒）
    [SerializeField] private GameObject normalBulletPrefab;
    [SerializeField] private float stopTime;
    [SerializeField] private float debuffTime;
    [SerializeField] private Transform centerObject;
    private float stopCountTime;
    private float debuffCountTime;
    private float skillSwitchTimer = 0.0f; //スキル経過時間
    private int currentSkillIndex = 0; // 現在のスキルのインデックス
    private float bossAttack1Timer = 45.0f;
    private float bossAttack2Timer = 80.0f;
    private float angle;
    private GameObject skillInstance;
    private GameObject normalPrefab;
    private Vector3 startPos;
    private bool isMoving = true;
    private bool debuffFlag = false;

    [SerializeField]
    private float hp;

    //プレイヤー取得
    private Player player;
    private SoundManager soundManager;
    private AreaManager areaManager;

    public bool BossAttack1 { get; set; } = false;
    public bool BossAttack2 { get; set; } = true;

    void Start()
    {
        player = FindObjectOfType<Player>();
        soundManager = FindObjectOfType<SoundManager>();
        areaManager = FindObjectOfType<AreaManager>();
        normalPrefab = Instantiate(normalBulletPrefab, transform.position, Quaternion.identity);
        normalPrefab.transform.SetParent(transform);
        soundManager.BossPhaseFlag = true;
        areaManager.BossActiveFlag = true;
    }

    void Update()
    {
        Debug.Log(isMoving);
        Debug.Log(player.BussMoveStopFlag);
        bossAttack1Timer -= Time.deltaTime;
        bossAttack2Timer -= Time.deltaTime;
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
        if(bossAttack1Timer <= 0)
        {
            BossAttack1 = true;
            isMoving = false;
            bossAttack1Timer = 5.0f;
        }
        else
        {
            BossAttack1 = false;
            isMoving = true;
        }
        if(bossAttack2Timer <= 0)
        {
            BossAttack2 = true;
            bossAttack2Timer = 10.0f;
        }
        else
        {
            BossAttack2 = false;
        }
        
        skillSwitchTimer += Time.deltaTime;
        // スキル切り替えのタイミングを管理
        if (skillSwitchTimer >= skillSwitchInterval)
        {
            SwitchSkill();
            skillSwitchTimer = 0.0f;
        }
    }

    private void Move()
    {
        
        if(debuffFlag == true)
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

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 1f);
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


    // スキルを切り替えるメソッド
    private void SwitchSkill()
    {
        // 現在のスキルを破棄
        DestroyCurrentSkill();

        // 次のスキルに切り替え
        currentSkillIndex = (currentSkillIndex + 1) % skillPrefabs.Length;

        // 新しいスキルを生成
        InstantiateSkill();
    }

    // 現在のスキルを生成
    private void InstantiateSkill()
    {
        GameObject skillPrefab = skillPrefabs[currentSkillIndex];
        // スキルの生成と初期化をここに実装
        skillInstance = Instantiate(skillPrefab, transform.position, Quaternion.identity);
        skillInstance.transform.SetParent(transform);
    }

    // 現在のスキルを破棄
    private void DestroyCurrentSkill()
    {
        if (skillInstance != null)
        {
            Destroy(skillInstance);
        }
    }
}
