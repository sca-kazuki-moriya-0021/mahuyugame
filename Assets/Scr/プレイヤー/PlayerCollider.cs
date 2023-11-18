using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine.Unity;
using Spine;

public class PlayerCollider : MonoBehaviour
{
    private TotalGM gm;

    //STATE型の変数
    STATE state;
    //点滅間隔
    [SerializeField] private float flashInterval;
    //点滅させるときのループカウント
    [SerializeField] private int loopCount;
    //コライダーをオンオフするためのCircleCollider2D
    private CircleCollider2D collider2D;
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
    private MeshRenderer SpineRenderer;
    private SpriteRenderer colliderSprite;

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
        collider2D = GetComponent<CircleCollider2D>();
        colliderSprite = GetComponent<SpriteRenderer>();

        // SkeletonAnimationからAnimationStateを取得
        spineAnimationState = skeletonAnimation.AnimationState;

        var scene = gm.MyGetScene();
        if(gm.BackScene == scene)
        {
            gm.PlayerLevel[0] = gm.PlayerLevel[1];
            gm.PlayerHp[0] = gm.PlayerHp[1];
        }
        if(gm.BackScene != scene || gm.BackScene == TotalGM.StageCon.No)
        {
            gm.PlayerHp[1] = gm.PlayerHp[0];
            gm.PlayerLevel[1] = gm.PlayerLevel[0];
        }
    }

    // Update is called once per frame
    void Update()
    {

        // ステートがダメージならリターン
        if (state == STATE.DAMAGED)
        {
            return;
        }

        if (gm.PlayerHp[0] <= 0 && deathFlag == false)
        {
            deathFlag = true;
            StartCoroutine(PlayerDeath());
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            //collision.gameObject.CompareTag("EnemySkillBullet") ||
            //collision.gameObject.CompareTag("DestoryBullet"))
        {
            Destroy(collision.gameObject);
            
            gm.PlayerHp[0]--;
            if(gm.PlayerHp[0] > 0)
            {
                state = STATE.DAMAGED;
                StartCoroutine(PlayerDameged());
            }
        }

        if (collision.gameObject.CompareTag("BaffItem"))
        {
            collision.gameObject.SetActive(false);
            gm.PlayerLevel[0]++;
        }

        if (collision.gameObject.CompareTag("WeaponSwitchItem0"))
        {
            collision.gameObject.SetActive(false);
            for (int i = 0; i < gm.PlayerWeapon.Length; i++)
            {
                gm.PlayerWeapon[i] = false;
            }

            gm.PlayerWeapon[0] = true;
        }

        if (collision.gameObject.CompareTag("WeaponSwitchItem1"))
        {
            collision.gameObject.SetActive(false);
            for (int i = 0; i < gm.PlayerWeapon.Length; i++)
            {
                gm.PlayerWeapon[i] = false;
            }

            gm.PlayerWeapon[1] = true;
        }

        if (collision.gameObject.CompareTag("WeaponSwitchItem2"))
        {
            collision.gameObject.SetActive(false);
            for (int i = 0; i < gm.PlayerWeapon.Length; i++)
            {
                gm.PlayerWeapon[i] = false;
            }

            gm.PlayerWeapon[2] = true;
        }

        if (collision.gameObject.CompareTag("WeaponSwitchItem3"))
        {
            collision.gameObject.SetActive(false);
            for (int i = 0; i < gm.PlayerWeapon.Length; i++)
            {
                gm.PlayerWeapon[i] = false;
            }

            gm.PlayerWeapon[3] = true;
        }
    }

    private IEnumerator PlayerDameged()
    {
        isHit = true;
        collider2D.enabled = false;

        SpineRenderer.material.color = Color.black;
        colliderSprite.color = Color.black;
        
        Debug.Log("a");

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
            SpineRenderer.enabled = false;
            colliderSprite.enabled = false;

            //flashInterval待ってから
            yield return new WaitForSeconds(flashInterval);
            //spriteRendererをオン
            SpineRenderer.enabled = true;
            colliderSprite.enabled = true;

            //ループが5回まわったら
            /* (i > 5)
            {
                //stateをMUTEKIにする（点滅しながら動けるようになる）
                state = STATE.MUTEKI;
                //色を緑にする
                characterSprite.color = Color.green;
            }*/
        }

        //デフォルト状態にする
        state = STATE.NOMAL;
        //当たり判定をオンにする
        collider2D.enabled = true;
        //色を白にする
        SpineRenderer.material.color = Color.white;
        colliderSprite.color = Color.white;
        

        //点滅ループが抜けたら当たりフラグをfalse(当たってない状態)
        isHit = false;
    }

    private IEnumerator PlayerDeath()
    {
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

