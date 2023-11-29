using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

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

    private float angle;
    
    //private Vector3 startPos;
    private bool isMoving = true;
    private bool debuffFlag = false;
    //���S�t���O
    private bool bossDeathFlag = false;

    [SerializeField]
    private float hp;
    //���������j���ꂽ���Ƀh���b�v����A�C�e��
    [SerializeField]
    private GameObject dropItem;
    //���S����A�j���[�V������
    [SerializeField]
    private string deathAnimation;
    //�Q�[���I�u�W�F�N�g�ɐݒ肳��Ă���SkeletonAnimation
    [SerializeField]
    private SkeletonAnimation skeletonAnimation = default;
    //Spine�A�j���[�V������K�p���邽�߂ɕK�v��AnimationState
    private Spine.AnimationState spineAnimationState = default;

    //�X�N���v�g�擾
    private Player player;
    private PlayerCollider playerCollider;
    private AreaManager areaManager;
    private NowLoading nowLoading;

    public bool BossDeathFlag
    {
        get { return this.bossDeathFlag; }
        set { this.bossDeathFlag = value; }
    }

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
        nowLoading = FindObjectOfType<NowLoading>();
        playerCollider = FindObjectOfType<PlayerCollider>();

        // SkeletonAnimation����AnimationState���擾
        spineAnimationState = skeletonAnimation.AnimationState;
    }

    void Update()
    {
        //Debug.Log(isMoving);
        //Debug.Log(player.BussMoveStopFlag);
        
        if(bossAttack1 == false && isMoving == true)
        {
            Move();
        }
        //�v���C���[�̈ړ���~�X�L�����������Ă��Ȃ��������A����ł��Ȃ��������͓���
        if (player.BussMoveStopFlag == true || bossDeathFlag == true)
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
            //�v���C���[�̗̑͂�0����Ȃ���
            if(playerCollider.DeathFlag == false)
            HitBullet();
        }

        if (collision.gameObject.CompareTag("PlayerSkillBullet"))
        {
            Destroy(collision.gameObject);
            //��Ɠ���
            if (playerCollider.DeathFlag == false)
            {
                debuffFlag = true;
                Debug.Log("�f�o�t������");
            }
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
        }
        if (debuffFlag == true)
            hp -= 2;
        else
            hp--;
    }

    //�A�C�e���h���b�v
    private IEnumerator DropItemInstance()
    {
        bossDeathFlag = true;
        for(int i = 0;i < 10; i++)
        {
            Instantiate(dropItem,transform.position,Quaternion.identity);
            yield return null;
        }
        spineAnimationState.SetAnimation(0, deathAnimation, false);
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        nowLoading.FadeIn();
        StopCoroutine(DropItemInstance());
    }
}
