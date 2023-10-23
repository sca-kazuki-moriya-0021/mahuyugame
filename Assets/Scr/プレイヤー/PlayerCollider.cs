using System.Collections;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private TotalGM gm;
    
    private bool test;

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

    // 画像描画用のコンポーネント
    [SerializeField]
    private SpriteRenderer characterSprite;
    private SpriteRenderer colliderSprite;

    #region//無敵関係
    //private bool invincibleFlag = false;
    private static float invincibleCount = 5.0f;
    //private static float invincibleCounttime = 0;
    #endregion

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
    }

    // Update is called once per frame
    void Update()
    {
       

        // ステートがダメージならリターン
        if (state == STATE.DAMAGED)
        {
            return;
        }

        /*if (test == false)
        {
            if (isHit == false)
            {
                gm.PlayerHp[0]--;
                state = STATE.DAMAGED;
                StartCoroutine(PlayerDameged());
            }
            test = true;
        }*/
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bellet") ||
            collision.gameObject.CompareTag("EnemySkillBullet") ||
            collision.gameObject.CompareTag("DestoryBullet"))
        {
            collision.gameObject.SetActive(false);
            gm.PlayerHp[0]--;
            state = STATE.DAMAGED;
            StartCoroutine(PlayerDameged());
        }

        /*if (collision.gameObject.CompareTag("weaponItem0"))
        {
            for(int i = 0; i < gm.PlayerWeapon.Length; i++)
            {
                gm.PlayerWeapon[i] = false;
            }

            gm.PlayerWeapon[0] = true;
        }

        if (collision.gameObject.CompareTag("weaponItem1"))
        {
            for (int i = 0; i < gm.PlayerWeapon.Length; i++)
            {
                gm.PlayerWeapon[i] = false;
            }

            gm.PlayerWeapon[1] = true;
        }

        if (collision.gameObject.CompareTag("weaponItem2"))
        {
            for (int i = 0; i < gm.PlayerWeapon.Length; i++)
            {
                gm.PlayerWeapon[i] = false;
            }

            gm.PlayerWeapon[2] = true;
        }

        if (collision.gameObject.CompareTag("weaponItem3"))
        {
            for (int i = 0; i < gm.PlayerWeapon.Length; i++)
            {
                gm.PlayerWeapon[i] = false;
            }

            gm.PlayerWeapon[3] = true;
        }*/
    }

    private IEnumerator PlayerDameged()
    {
        isHit = true;
        collider2D.enabled = false;

        characterSprite.color = Color.black;
        colliderSprite .color = Color.black;

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
            characterSprite.enabled = false;
            colliderSprite.enabled = false;

            //flashInterval待ってから
            yield return new WaitForSeconds(flashInterval);
            //spriteRendererをオン
            characterSprite.enabled = true;
            colliderSprite.enabled = true;

            //ループが5回まわったら
            if (i > 5)
            {
                //stateをMUTEKIにする（点滅しながら動けるようになる）
                state = STATE.MUTEKI;
                //色を緑にする
                characterSprite.color = Color.green;
            }
        }

        //デフォルト状態にする
        state = STATE.NOMAL;
        //当たり判定をオンにする
        collider2D.enabled = true;
        //色を白にする
        characterSprite.color = Color.white;
        characterSprite.color = Color.white;
        

        //点滅ループが抜けたら当たりフラグをfalse(当たってない状態)
        isHit = false;
    }


}

