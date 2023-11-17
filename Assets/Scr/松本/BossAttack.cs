using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossAttack : MonoBehaviour
{
    [SerializeField] private float attackSpeed; // �{�X�̓ˌ����x
    [SerializeField] private float returnSpeed; // �{�X�����̈ʒu�ɖ߂鑬�x
    [SerializeField] private float returnDelay; // ���̈ʒu�ɖ߂�܂ł̒x������
    [SerializeField] private float distanceToPlayer; // �v���C���[����̋���

    [SerializeField] private GameObject danmakuPre1; // �e��1�̃v���n�u
    [SerializeField] private GameObject danmakuPre2; // �e��2�̃v���n�u

    private Transform cenPos1;
    private Transform cenPos2;
    private GameObject playerObject;
    private Vector3 initialPosition;
    private bool isAttacking = false;
    
    private BossMove bossMove;
    // ��������e�̍ő吔
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

            // �C���X�^���X���ꂽ�e�̐��𑝂₷
            currentBulletCount += 2; // �e��1�ƒe��2��2�������邽��
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