using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private float attackSpeed; // ボスの突撃速度
    [SerializeField] private float returnSpeed; // ボスが元の位置に戻る速度
    [SerializeField] private float returnDelay; // 元の位置に戻るまでの遅延時間

    [SerializeField] private GameObject danmakuPre1; // 弾幕1のプレハブ
    [SerializeField] private GameObject danmakuPre2; // 弾幕2のプレハブ

    private Transform cenPos1;
    private Transform cenPos2;
    private GameObject playerObject;
    private Vector3 initialPosition;
    private bool isAttacking = false;
    
    private BossMove bossMove;
    // 制限する弾の最大数
    [SerializeField] private int maxBulletCount = 5;
    private int currentBulletCount = 0;

    void Start()
    {
        bossMove = FindObjectOfType<BossMove>();
        playerObject = GameObject.FindWithTag("Player");
        initialPosition = transform.position;

        GameObject cenPosObject1 = GameObject.Find("cenPos1");
        GameObject cenPosObject2 = GameObject.Find("cenPos2");

        cenPos1 = cenPosObject1 != null ? cenPosObject1.transform : null;
        cenPos2 = cenPosObject2 != null ? cenPosObject2.transform : null;
        
    }

    void Update()
    {
        if (bossMove.BossAttack1 == true && isAttacking == false)
        {
            StartCoroutine(AttackPlayerCoroutine());
        }
        //スキル2
        if (bossMove.BossAttack2 == true)
        {
            if(currentBulletCount < maxBulletCount)
            {
                // Instantiate danmaku1
                GameObject danmakuInstance1 = Instantiate(danmakuPre1, transform.position, Quaternion.identity);

                // Get the Danmaku component and set the cenPos
                Danmaku danmaku1 = danmakuInstance1.GetComponent<Danmaku>();
                if (danmaku1 != null)
                {
                    danmaku1.SetCenPos(cenPos1);
                    StartCoroutine(DestroyDanmakuAfterDelay(danmakuInstance1, 15.0f)); // 2秒後に破棄
                }

                // Do the same for danmaku2
                GameObject danmakuInstance2 = Instantiate(danmakuPre2, transform.position, Quaternion.identity);
                Danmaku danmaku2 = danmakuInstance2.GetComponent<Danmaku>();
                if (danmaku2 != null)
                {
                    danmaku2.SetCenPos(cenPos2);
                    StartCoroutine(DestroyDanmakuAfterDelay(danmakuInstance2, 15.0f)); // 2秒後に破棄
                }

                // インスタンスされた弾の数を増やす
                currentBulletCount += 2; // 弾幕1と弾幕2で2つ生成するため
            }
            else
            {
                bossMove.BossAttack2 = false;
            }
        }
    }

    private IEnumerator DestroyDanmakuAfterDelay(GameObject danmakuInstance, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(danmakuInstance);
    }

    //スキルの移動本体
    /*
     private IEnumerator MoveToPosition(Vector3 targetPosition, float speed)
    {
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(transform.position, targetPosition);

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            float distCovered = (Time.time - startTime) * attackSpeed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, targetPosition, fracJourney);
            yield return null;
        }
    }*/


    /// <summary>
    /// スキル1の移動管理
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackPlayerCoroutine()
    {
        isAttacking = true;
        float startTime = 0.0f;

        Vector3 targetPosition = playerObject.transform.position;
        float preAttackDelay = 1.0f;
        yield return new WaitForSeconds(preAttackDelay);
        float journeyLength = Vector3.Distance(transform.position, targetPosition);
        while (startTime < 1)
        {
            startTime += Time.deltaTime;
            float journeyFraction = startTime * attackSpeed / journeyLength;
            transform.position = Vector3.Lerp(transform.position, targetPosition, journeyFraction);
            yield return null;
        }

        // プレイヤーに到達した後の処理
        yield return new WaitForSeconds(returnDelay);
        startTime = 0.0f;
        journeyLength = Vector3.Distance(transform.position, initialPosition);

        while (startTime < 1)
        {
            startTime += Time.deltaTime;
            float journeyFraction = startTime * returnSpeed / journeyLength;
            transform.position = Vector3.Lerp(transform.position, initialPosition, journeyFraction); 
            yield return null;
        }
        
        //yield return StartCoroutine(MoveToPosition(initialPosition, returnSpeed));
        bossMove.BossAttack1 = false;
        isAttacking = false;
        StopCoroutine(AttackPlayerCoroutine());
    }
}