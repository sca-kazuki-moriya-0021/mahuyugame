using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f; // �ړ����x
    [SerializeField] private float amplitudeX = 3.0f; // X���̐U��
    [SerializeField] private float amplitudeY = 1.0f; // Y���̐U��
    [SerializeField] private GameObject[] skillPrefabs; // �X�L���̒e���̃v���n�u�z��
    [SerializeField] private float skillSwitchInterval = 30.0f; // �X�L���؂�ւ��̊Ԋu�i�b�j
    [SerializeField] private GameObject normalBulletPrefab;
    [SerializeField] private float stopTime;
    [SerializeField] private float debuffTime;
    private float stopCountTime;
    private float debuffCountTime;
    private float skillSwitchTimer = 0.0f; //�X�L���o�ߎ���
    private int currentSkillIndex = 0; // ���݂̃X�L���̃C���f�b�N�X
    private GameObject skillInstance;
    private GameObject normalPrefab;
    private float angle;
    private Vector3 startPos;
    private bool isMoving = true;
    private bool debuffFlag = false;

    [SerializeField]
    private float hp;

    //�v���C���[�擾
    private Player player;
    private SoundManager soundManager;

    void Start()
    {
        player = FindObjectOfType<Player>();
        soundManager = FindObjectOfType<SoundManager>();
        startPos = transform.position;
        // �ʏ�e�����������������ɒǉ�
        normalPrefab = Instantiate(normalBulletPrefab, transform.position, Quaternion.identity);
        normalPrefab.transform.SetParent(transform);
        soundManager.BossActiveFlag = true;
        //�~�܂邩�e�X�g�悤
        // player.BussMoveStopFlag = true;
    }

    void Update()
    {
        Debug.Log(isMoving);
        Debug.Log(player.BussMoveStopFlag);

        if(isMoving == true)
        {
            Move();
        }
        //�v���C���[�̈ړ���~�X�L�����������Ă��Ȃ��������͓���
        if (player.BussMoveStopFlag == true)
        {
            StopMove();
        }

        if (debuffFlag == true)
        {
            Debuff();
        }
        
        skillSwitchTimer += Time.deltaTime;
        // �X�L���؂�ւ��̃^�C�~���O���Ǘ�
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
            angle += Time.deltaTime * 0.5f;
            float x = startPos.x + Mathf.Sin(angle) * amplitudeX * 0.5f;
            float y = startPos.y + Mathf.Sin(angle) * amplitudeY * 0.5f;
            // Z���̈ʒu�͌Œ�i2D��ԂɌŒ�j
            transform.position = new Vector3(x, y, 0);
            //transform.Translate(x,y,0);
        }
        else
        {
            angle += Time.deltaTime * speed;
            float x = startPos.x + Mathf.Sin(angle) * amplitudeX;
            float y = startPos.y + Mathf.Sin(angle) * amplitudeY;
            // Z���̈ʒu�͌Œ�i2D��ԂɌŒ�j
            transform.position = new Vector3(x, y, 0);
            //transform.Translate(x, y, 0);
        }
    }

    private void StopMove()
    {
        if(stopCountTime <= stopTime)
        {
            stopCountTime +=Time.deltaTime;
            isMoving = false;
            if (stopCountTime > stopTime)
            {
                stopCountTime = 0;
                isMoving = true;
                player.BussMoveStopFlag = false;
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
            Debug.Log("�f�o�t������");
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


    // �X�L����؂�ւ��郁�\�b�h
    private void SwitchSkill()
    {
        // ���݂̃X�L����j��
        DestroyCurrentSkill();

        // ���̃X�L���ɐ؂�ւ�
        currentSkillIndex = (currentSkillIndex + 1) % skillPrefabs.Length;

        // �V�����X�L���𐶐�
        InstantiateSkill();
    }

    // ���݂̃X�L���𐶐�
    private void InstantiateSkill()
    {
        GameObject skillPrefab = skillPrefabs[currentSkillIndex];
        // �X�L���̐����Ə������������Ɏ���
        skillInstance = Instantiate(skillPrefab, transform.position, Quaternion.identity);
        skillInstance.transform.SetParent(transform);
    }

    // ���݂̃X�L����j��
    private void DestroyCurrentSkill()
    {
        if (skillInstance != null)
        {
            Destroy(skillInstance);
        }
    }

}
