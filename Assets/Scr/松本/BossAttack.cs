using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossAttack : MonoBehaviour
{
    [SerializeField] private float attackSpeed; // ボスの突撃速度
    [SerializeField] private float returnSpeed; // ボスが元の位置に戻る速度
    [SerializeField] private float returnDelay; // 元の位置に戻るまでの遅延時間
    [SerializeField] private float distanceToPlayer; // プレイヤーからの距離

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

        if (bossMove.BossAttack1 == true)
        {
            StartCoroutine(AttackPlayerCoroutine());
        }
    }

    void Update()
    {
        if (bossMove.BossAttack2 && currentBulletCount < maxBulletCount)
        {
            // Instantiate danmaku1
            GameObject danmakuInstance1 = Instantiate(danmakuPre1, transform.position, Quaternion.identity);

            // Get the Danmaku component and set the cenPos
            Danmaku danmaku1 = danmakuInstance1.GetComponent<Danmaku>();
            if (danmaku1 != null)
            {
                danmaku1.SetCenPos(cenPos1);
            }

            // Do the same for danmaku2
            GameObject danmakuInstance2 = Instantiate(danmakuPre2, transform.position, Quaternion.identity);
            Danmaku danmaku2 = danmakuInstance2.GetComponent<Danmaku>();
            if (danmaku2 != null)
            {
                danmaku2.SetCenPos(cenPos2);
            }

            // インスタンスされた弾の数を増やす
            currentBulletCount += 2; // 弾幕1と弾幕2で2つ生成するため
        }
    }

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
    }

    private IEnumerator AttackPlayerCoroutine()
    {
        while (true)
        {
            Vector3 targetPosition = playerObject.transform.position - playerObject.transform.right * distanceToPlayer;

            yield return StartCoroutine(MoveToPosition(targetPosition, attackSpeed));

            isAttacking = true;

            yield return new WaitForSeconds(returnDelay);

            yield return StartCoroutine(MoveToPosition(initialPosition, returnSpeed));

            isAttacking = false;
        }
    }

    void MoveDanmaku(GameObject danmakuInstance, Transform cenPos)
    {
        if (danmakuInstance != null && cenPos != null)
        {
            float x = cenPos.position.x;
            float y = cenPos.position.y;

            danmakuInstance.transform.position = new Vector3(x, y, 0f);
        }
    }
}