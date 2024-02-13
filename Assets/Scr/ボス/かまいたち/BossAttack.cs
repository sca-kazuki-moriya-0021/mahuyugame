using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private float attackSpeed; // �{�X�̓ˌ����x
    [SerializeField] private float returnSpeed; // �{�X�����̈ʒu�ɖ߂鑬�x
    [SerializeField] private float returnDelay; // ���̈ʒu�ɖ߂�܂ł̒x������

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
        
    }

    void Update()
    {
        if (bossMove.BossAttack1 == true && isAttacking == false)
        {
            StartCoroutine(AttackPlayerCoroutine());
        }
        //�X�L��2
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
                    StartCoroutine(DestroyDanmakuAfterDelay(danmakuInstance1, 15.0f)); // 2�b��ɔj��
                }

                // Do the same for danmaku2
                GameObject danmakuInstance2 = Instantiate(danmakuPre2, transform.position, Quaternion.identity);
                Danmaku danmaku2 = danmakuInstance2.GetComponent<Danmaku>();
                if (danmaku2 != null)
                {
                    danmaku2.SetCenPos(cenPos2);
                    StartCoroutine(DestroyDanmakuAfterDelay(danmakuInstance2, 15.0f)); // 2�b��ɔj��
                }

                // �C���X�^���X���ꂽ�e�̐��𑝂₷
                currentBulletCount += 2; // �e��1�ƒe��2��2�������邽��
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

    //�X�L���̈ړ��{��
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
    /// �X�L��1�̈ړ��Ǘ�
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

        // �v���C���[�ɓ��B������̏���
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