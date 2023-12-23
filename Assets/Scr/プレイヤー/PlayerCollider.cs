using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine.Unity;
using Spine;

public class PlayerCollider : MonoBehaviour
{
    private TotalGM gm;
    private BossCollder bossCollder;

    //アイテム取得効果音
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] audioClips;

    //STATE型の変数
    STATE state;
    //点滅間隔
    [SerializeField] private float flashInterval;
    //点滅させるときのループカウント
    [SerializeField] private int loopCount;
    //コライダーをオンオフするためのCircleCollider2D
    [SerializeField]
    private CircleCollider2D collider;
    //当たったかどうかのフラグ
    private bool isHit;
    //死亡フラグ
    private bool deathFlag = false;

    //死亡するアニメーション名
    [SerializeField]
    private string deathAnimation;
    //ゲームオブジェクトに設定されているSkeletonAnimation
    [SerializeField]
    private SkeletonAnimation skeletonAnimation = default;
    //Spineアニメーションを適用するために必要なAnimationState
    private Spine.AnimationState spineAnimationState = default;

    // 画像描画用のコンポーネント
    [SerializeField]
    private MeshRenderer spineRenderer;
    [SerializeField]
    private SpriteRenderer colliderSprite;

    public bool DeathFlag
    {
        get { return this.deathFlag; }
        set { this.deathFlag = value; }
    }

    //プレイヤーの状態用列挙型（ノーマル、ダメージ、無敵の3種類）
    enum STATE
    {
        NOMAL,
        DAMAGED,
        MUTEKI
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<TotalGM>();
        bossCollder = FindObjectOfType<BossCollder>();

        // SkeletonAnimationからAnimationStateを取得
        spineAnimationState = skeletonAnimation.AnimationState;

        //プレイヤーのHp初期化
        var scene = gm.MyGetScene();
        if(gm.BackScene == scene)
        {
            gm.PlayerHp[0] = gm.PlayerHp[1];
        }
        if(gm.BackScene != scene || gm.BackScene == TotalGM.StageCon.No)
        {
            //体力0のまま、次ステージに移行したら体力1回復する
            if(gm.PlayerHp[0] == 0)
            {
                gm.PlayerHp[0]++;
            }
            gm.PlayerHp[1] = gm.PlayerHp[0];
        }
        skeletonAnimation.timeScale = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        // ステートがダメージならリターン
        if (state == STATE.DAMAGED)
        {
            return;
        }
        //プレイヤーHpが0以下かつフェードインしてなかったら
        if (gm.PlayerHp[0] <= 0 && deathFlag == false && bossCollder.BossDeathFlag == false)
        {
            deathFlag = true;
            StartCoroutine(PlayerDeath());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //敵の弾に当たった時
        if (collision.gameObject.CompareTag("Bullet"))
            //collision.gameObject.CompareTag("EnemySkillBullet") ||
            //collision.gameObject.CompareTag("DestoryBullet"))
        {
            if(bossCollder.BossDeathFlag == false)
            {
                gm.PlayerHp[0]--;
                if (gm.PlayerHp[0] > 0)
                {
                    state = STATE.DAMAGED;
                    audioSource.PlayOneShot(audioClips[1]);
                    StartCoroutine(PlayerDameged());
                }
            }
        }
        //スコア加算アイテム取った時
        if (collision.gameObject.CompareTag("ScoreAdditionItem"))
        {
            collision.gameObject.SetActive(false);
            var scene = gm.MyGetScene();
            audioSource.PlayOneShot(audioClips[0]);
            switch (scene)
            {
                case TotalGM.StageCon.First:
                    gm.NowScore[0] += 5000;
                    break;
                case TotalGM.StageCon.Secound:
                    gm.NowScore[1] += 5000;
                    break;
                case TotalGM.StageCon.Thead:
                    gm.NowScore[2] += 5000;
                    break;
            }
        }
    }
    //プレイヤーがダメージ受けたら
    private IEnumerator PlayerDameged()
    {
        isHit = true;
        collider.enabled = false;

        spineRenderer.material.color = Color.black;
        colliderSprite.color = Color.black;
        
        //点滅ループ開始
        for (int i = 0; i < loopCount; i++)
        {
            if (isHit == false)
            {
                continue;
            }
            //flashInterval待ってから
            yield return new WaitForSeconds(flashInterval);
            //spriteRendererをオフ
            spineRenderer.enabled = false;
            colliderSprite.enabled = false;

            //flashInterval待ってから
            yield return new WaitForSeconds(flashInterval);
            //spriteRendererをオン
            spineRenderer.enabled = true;
            colliderSprite.enabled = true;
        }

        //デフォルト状態にする
        state = STATE.NOMAL;
        //当たり判定をオンにする
        collider.enabled = true;
        //色を白にする
        spineRenderer.material.color = Color.white;
        colliderSprite.color = Color.white;
        
        //点滅ループが抜けたら当たりフラグをfalse(当たってない状態)
        isHit = false;
    }

    //プレイヤーが死んだとき
    private IEnumerator PlayerDeath()
    {
        if(bossCollder.BossDeathFlag == true)
        {
            StopCoroutine(PlayerDeath());
        }
        colliderSprite.enabled = false;
        gm.PlayerTransForm = transform.position;
        gm.BackScene = gm.MyGetScene();

        spineAnimationState.SetAnimation(0, deathAnimation, false);
        
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("GameOver");
        deathFlag = false;
        StopCoroutine(PlayerDeath());
    }
}

