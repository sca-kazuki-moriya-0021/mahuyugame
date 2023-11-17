using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    [SerializeField] private GameObject[] skillPrefabs; // スキルの弾幕のプレハブ配列
    [SerializeField] private float skillSwitchInterval = 30.0f; // スキル切り替えの間隔（秒）
    [SerializeField] private GameObject normalBulletPrefab;

    private float skillSwitchTimer = 0.0f; //スキル経過時間
    private int currentSkillIndex = 0; // 現在のスキルのインデックス
    private float skillTimerCount = 0f;// スキル発動タイミング
    private GameObject skillInstance;
    private GameObject normalPrefab;

    private BossMove bossMove;
    // Start is called before the first frame update
    void Start()
    {
        bossMove = FindObjectOfType<BossMove>();
        normalPrefab = Instantiate(normalBulletPrefab, transform.position, Quaternion.identity);
        normalPrefab.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        skillTimerCount += Time.deltaTime;

        if (skillTimerCount >= 5f)
        {
           bossMove.BossAttack1 = true;
        }

        if(skillTimerCount >= 10f)
        {
            bossMove.BossAttack2 = true;
        }

        skillSwitchTimer += Time.deltaTime;
        // スキル切り替えのタイミングを管理
        if (skillSwitchTimer >= skillSwitchInterval)
        {
            SwitchSkill();
            skillSwitchTimer = 0.0f;
        }
    }

    // 弾幕を切り替えるメソッド
    private void SwitchSkill()
    {
        // 現在の弾幕を破棄
        DestroyCurrentSkill();

        // 次の弾幕に切り替え
        currentSkillIndex = (currentSkillIndex + 1) % skillPrefabs.Length;

        // 新しい弾幕を生成
        InstantiateSkill();
    }

    // 現在の弾幕を生成
    private void InstantiateSkill()
    {
        GameObject skillPrefab = skillPrefabs[currentSkillIndex];
        // スキルの弾幕と初期化をここに実装
        skillInstance = Instantiate(skillPrefab, transform.position, Quaternion.identity);
        skillInstance.transform.SetParent(transform);
    }

    // 現在の弾幕を破棄
    private void DestroyCurrentSkill()
    {
        if (skillInstance != null)
        {
            Destroy(skillInstance);
        }
    }
}
