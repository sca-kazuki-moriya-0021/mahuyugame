using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class BossCollder : MonoBehaviour
{
    [SerializeField]
    private int hp = 0;
    //デバフ効果時間
    [SerializeField]
    private float debuffTime;
    private float debuffCountTime;

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
                debuffCountTime = 0;
                debuffFlag = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            hitObject(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("PlayerSkillBullet"))
        {
            hitObject(collision.gameObject);
            if (debuffFlag == false)
                debuffFlag = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            hitObject(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("PlayerSkillBullet"))
        {
            hitObject(collision.gameObject);
            if (debuffFlag == false)
                debuffFlag = true;
        }
    }


    private void hitObject(GameObject a)
    {
        Destroy(a);
        if (playerCollider.DeathFlag == false)
        {
            //デバフ中なら
            if (debuffFlag == true)
                hp -= 2;
            else
                hp--;
            //HPが0の時アイテムドロップさせる
            if (hp == 0)
                StartCoroutine(DropItemInstance());
        }
    }

    //アイテムドロップ
    private IEnumerator DropItemInstance()
    {
        bossDeathFlag = true;
        for (int i = 0; i < 10; i++)
        {
            Instantiate(dropItem, transform.position, Quaternion.identity);
            yield return null;
        }
        spineAnimationState.TimeScale = 0.2f;
        spineAnimationState.SetAnimation(0, deathAnimation, false);
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        nowLoading.FadeIn();
        StopCoroutine(DropItemInstance());
    }
}
