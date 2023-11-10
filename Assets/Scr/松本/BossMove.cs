using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f; // 移動速度
    [SerializeField] private float amplitudeX = 3.0f; // X軸の振幅
    [SerializeField] private float amplitudeY = 1.0f; // Y軸の振幅
    [SerializeField] private GameObject[] skillPrefabs; // スキルの弾幕のプレハブ配列
    [SerializeField] private float skillSwitchInterval = 30.0f; // スキル切り替えの間隔（秒）
    [SerializeField] private GameObject normalBulletPrefab;
    private float skillSwitchTimer = 0.0f; //スキル経過時間
    private int currentSkillIndex = 0; // 現在のスキルのインデックス
    private GameObject skillInstance;
    private GameObject normalPrefab;
    private float angle;
    private Vector3 startPos;

    //プレイヤー取得
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        startPos = transform.position;
        // 通常弾幕を撃つ処理をここに追加
        normalPrefab = Instantiate(normalBulletPrefab, transform.position, Quaternion.identity);
        normalPrefab.transform.SetParent(transform);
    }

    void Update()
    {
        //プレイヤーの移動停止スキルが発動していなかった時は動く
        if(player.BussMoveStopFlag == false)
        {
            angle += Time.deltaTime * speed;
            float x = startPos.x + Mathf.Sin(angle * 2) * amplitudeX;
            float y = startPos.y + Mathf.Sin(angle) * amplitudeY;
            // Z軸の位置は固定（2D空間に固定）
            transform.position = new Vector3(x, y, 0);
        }
        skillSwitchTimer += Time.deltaTime;
        // スキル切り替えのタイミングを管理
        if (skillSwitchTimer >= skillSwitchInterval)
        {
            SwitchSkill();
            skillSwitchTimer = 0.0f;
        }
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
