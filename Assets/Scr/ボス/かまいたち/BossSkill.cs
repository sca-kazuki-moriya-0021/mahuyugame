using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    [SerializeField] private GameObject[] skillPrefabs; // スキルの弾幕のプレハブ配列
    [SerializeField] private float skillSwitchInterval = 30.0f; // スキル切り替えの間隔（秒）

    private float skillSwitchTimer = 0.0f; //スキル経過時間
    private int currentSkillIndex = 0; // 現在のスキルのインデックス
    private float skillTimerCount = 0f;// スキル発動タイミング
    private GameObject skillInstance;
    private bool[] test = new bool[]{false,false };
    private BossMove bossMove;
    private BossCollder bossCollder;
    // Start is called before the first frame update
    void Start()
    {
        GameObject skillPrefab = skillPrefabs[0];
        // スキルの弾幕と初期化をここに実装
        skillInstance = Instantiate(skillPrefab, transform.position,Quaternion.identity);
        skillInstance.transform.SetParent(transform);
        bossMove = FindObjectOfType<BossMove>();
        bossCollder = FindObjectOfType<BossCollder>();
    }

    // Update is called once per frame
    void Update()
    {
        skillTimerCount += Time.deltaTime;
        if (bossCollder.BossDeathFlag == false)
        {
            if (skillTimerCount > 45f && test[0] == false)
            {
                bossMove.BossAttack1 = true;
                test[0] = true;
            }

            if (skillTimerCount > 100f && test[1] == false)
            {
                bossMove.BossAttack2 = true;
                test[1] = true;
            }

            skillSwitchTimer += Time.deltaTime;
            // スキル切り替えのタイミングを管理
            if (skillSwitchTimer >= skillSwitchInterval)
            {
                SwitchSkill();
                skillSwitchTimer = 0.0f;
            }
        }

        if (bossCollder.BossDeathFlag == true)
        {
            DestroyCurrentSkill();
        }
    }

    // 弾幕を切り替えるメソッド
    private void SwitchSkill()
    {
        // 現在の弾幕を破棄
        DestroyCurrentSkillWithDelay(0.5f);

        // 次の弾幕に切り替え
        currentSkillIndex = (currentSkillIndex + 1) % skillPrefabs.Length;

        // 新しい弾幕を生成
        InstantiateSkill();
    }

    // 現在の弾幕を生成
    private void InstantiateSkill()
    {
        Quaternion rot = Quaternion.Euler(0,0,270);
        GameObject skillPrefab = skillPrefabs[currentSkillIndex];
        // スキルの弾幕と初期化をここに実装
        skillInstance = Instantiate(skillPrefab, transform.position, rot);
        //skillInstance.transform.SetParent(transform);
    }

    // 現在の弾幕を破棄
    private void DestroyCurrentSkillWithDelay(float delay)
    {
        StartCoroutine(DelayedDestroy(delay));
    }

    private IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (skillInstance != null)
        {
            Destroy(skillInstance);
        }
    }

    private void DestroyCurrentSkill()
    {
        if (skillInstance != null)
        {
            Destroy(skillInstance);
        }
    }
}
