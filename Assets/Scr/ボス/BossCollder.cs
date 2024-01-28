using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class BossCollder : MonoBehaviour
{
    [SerializeField]
    private float hp = 0;
    //デバフ効果時間
    [SerializeField]
    private float debuffTime;
    private float debuffCountTime;
    [SerializeField]
    private GameObject debuffEffect;
    private GameObject debuffObject;

    //スクリプト取得
    private PlayerCollider playerCollider;
    private NowLoading nowLoading;

    //自分が撃破された時にドロップするアイテム
    [SerializeField]
    private GameObject dropItem;
    //死亡するアニメーション名
    [SerializeField]
    private string deathAnimation;
    //ゲームオブジェクトに設定されているSkeletonAnimation
    [SerializeField]
    private SkeletonAnimation skeletonAnimation = default;
    //Spineアニメーションを適用するために必要なAnimationState
    private Spine.AnimationState spineAnimationState = default;

    //死亡フラグ
    private bool bossDeathFlag = false;
    //デバフフラグ
    private bool debuffFlag = false;

    public float BossHp {
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
        // SkeletonAnimationからAnimationStateを取得
        spineAnimationState = skeletonAnimation.AnimationState;
    }

    // Update is called once per frame
    void Update()
    {
        if (debuffFlag == true)
            Debuff();
    }

    private void Debuff()
    {
        if (debuffCountTime <= debuffTime)
        {
            debuffCountTime += Time.deltaTime;
            if (debuffCountTime > debuffTime || bossDeathFlag == true)
            {
                debuffCountTime = 0;
                debuffFlag = false;
                StartCoroutine(DebuffEnd());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
            hitObject(false);

        if (collision.gameObject.CompareTag("BossTargetBullet"))
            hitObject(true);

        if (collision.gameObject.CompareTag("PlayerSkillBullet"))
        {
            hitObject(false);
            DebuffActive();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
            hitObject(false);

        if (collision.gameObject.CompareTag("PlayerSkillBullet"))
        {
            hitObject(false);
            DebuffActive();
        }
    }

    private void DebuffActive()
    {
        if (debuffFlag == false)
        {
            debuffFlag = true;
            debuffObject = Instantiate(debuffEffect,transform.position,Quaternion.identity,transform);
        }
    }

    private void hitObject(bool target )
    {
        if (playerCollider.DeathFlag == false)
        {
            if(target == true && debuffFlag == true)
               hp -= 0.6f;
            else if (target == true && debuffFlag == false)
               hp -= 0.3f;
            else if(target == false && debuffFlag == true)
               hp -= 2;
            else hp--;

            //HPが0の時アイテムドロップさせる
            if (hp <= 0 && bossDeathFlag == false)
                StartCoroutine(DropItemInstance());
        }
    }

    //アイテムドロップ
    private IEnumerator DropItemInstance()
    {
        bossDeathFlag = true;
        spineAnimationState.TimeScale = 0.2f;
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

    //デバフ状態が終わった時
    private IEnumerator DebuffEnd()
    {
        var v = debuffObject.GetComponent<ParticleSystem>();
        var main = v.main;
        main.simulationSpeed = 1f;
        main.loop = false;

        yield return new WaitForSeconds(1f);

        Destroy(debuffObject);
        StopCoroutine(DebuffEnd());
    }
}
