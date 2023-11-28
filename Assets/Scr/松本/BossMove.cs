using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f; // �ړ����x
    [SerializeField] private float amplitudeX; // X���̐U��
    [SerializeField] private float amplitudeY; // Y���̐U��
    [SerializeField] private float stopTime;
    [SerializeField] private float debuffTime;
    [SerializeField] private Transform centerObject;
    private float stopCountTime;
    private float debuffCountTime;
    private bool bossAttack1 = false;
    private bool bossAttack2 = false;
    private float a = 0f;
   
    private float angle;
    
    //private Vector3 startPos;
    private bool isMoving = true;
    private bool debuffFlag = false;

    [SerializeField]
    private float hp;
    //���������j���ꂽ���Ƀh���b�v����A�C�e��
    [SerializeField]
    private GameObject dropItem;

    //�v���C���[�擾
    private Player player;
    private AreaManager areaManager;

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
    }

    void Update()
    {
        //Debug.Log(isMoving);
        //Debug.Log(player.BussMoveStopFlag);
        
        if(bossAttack1 == false && isMoving == true)
        {
            Move();
        }
        //�v���C���[�̈ړ���~�X�L�����������Ă��Ȃ��������͓���
        if (player.BussMoveStopFlag == true)
        {
            StopMove();
        }
        //�f�o�t�ňړ����x�̒ቺ
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
            // Z���̈ʒu�͌Œ�i2D��ԂɌŒ�j
            Vector3 offset = new Vector3(x, y, 0);
            Vector3 newPosition = centerObject.position + offset;

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 1f);
        }
        else
        {
            angle += Time.deltaTime * speed;
            float x = Mathf.Sin(angle * 2) * amplitudeX;
            float y = Mathf.Sin(angle) * amplitudeY;
            // Z���̈ʒu�͌Œ�i2D��ԂɌŒ�j
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
            StartCoroutine(DropItemInstance());
            Destroy(this.gameObject);
        }
        if (debuffFlag == true)
            hp -= 2;
        else
            hp--;
    }

    //�A�C�e���h���b�v
    private IEnumerator DropItemInstance()
    {
        for(int i = 0;i < 10; i++)
        {
            Instantiate(dropItem,transform.position,Quaternion.identity);
            yield return null;
        }

        StopCoroutine(DropItemInstance());
    }
}
