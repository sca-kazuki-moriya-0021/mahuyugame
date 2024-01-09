using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class BossCollder : MonoBehaviour
{
    [SerializeField]
    private int hp = 0;
    //�f�o�t���ʎ���
    [SerializeField]
    private float debuffTime;
    private float debuffCountTime;
    [SerializeField]
    private GameObject debuffEffect;

    //�X�N���v�g�擾
    private PlayerCollider playerCollider;
    private NowLoading nowLoading;

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

    //���S�t���O
    private bool bossDeathFlag = false;
    //�f�o�t�t���O
    private bool debuffFlag = false;

    public int BossHp {
        get { return this.hp; }
        set { this.hp = value; }
    }

    public bool BossDeathFlag
    {
        get { return this.bossDeathFlag; }
        set { this.bossDeathFlag = value; }
    }

    public bool BossDebuffFlag
    {
        get { return this.debuffFlag; }
        set { this.debuffFlag = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        nowLoading = FindObjectOfType<NowLoading>();
        playerCollider = FindObjectOfType<PlayerCollider>();
        // SkeletonAnimation����AnimationState���擾
        spineAnimationState = skeletonAnimation.AnimationState;
    }

    // Update is called once per frame
    void Update()
    {

        if (debuffFlag == true)
        {
            Debuff();
        }
    }

    private void Debuff()
    {
        if (debuffCountTime <= debuffTime)
        {
            debuffCountTime += Time.deltaTime;
            if (debuffCountTime > debuffTime)
            {
                Destroy(debuffEffect);
                debuffCountTime = 0;
                debuffFlag = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            hitObject();
        }

        if (collision.gameObject.CompareTag("PlayerSkillBullet"))
        {
            hitObject();
            DebuffActive();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            hitObject();
        }

        if (collision.gameObject.CompareTag("PlayerSkillBullet"))
        {
            hitObject();
            DebuffActive();
        }
    }

    private void DebuffActive()
    {
        if (debuffFlag == false)
        {
            debuffFlag = true;
            Instantiate(debuffEffect,transform.position,Quaternion.identity,transform);
        }
    }

    private void hitObject()
    {
        if (playerCollider.DeathFlag == false)
        {
            //�f�o�t���Ȃ�
            if (debuffFlag == true)
                hp -= 2;
            else
                hp--;
            //HP��0�̎��A�C�e���h���b�v������
            if (hp <= 0)
                StartCoroutine(DropItemInstance());
        }
    }

    //�A�C�e���h���b�v
    private IEnumerator DropItemInstance()
    {
        bossDeathFlag = true;
        //spineAnimationState.TimeScale = 0.2f;
        spineAnimationState.SetAnimation(0, deathAnimation, false);
        for (int i = 0; i < 10; i++)
        {
            Instantiate(dropItem, transform.position, Quaternion.identity);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        nowLoading.FadeIn();
        StopCoroutine(DropItemInstance());
    }
}
