using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;
using DG.Tweening;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //方向取得用
    private Vector2 inputV;
    //移動制限用
    [SerializeField]
    private GameObject screenWithin;
    private GameObject[] screenWithinChird =new GameObject[2] {null,null};

    //コライダー取得用
    [SerializeField]
    private PlayerCollider playerCollider;

    //スクリプト取得用
    private TotalGM gm;
    private SkillDisplay_Stage skillDisplay;
    private PouseCon pouseCon;
    private PlayerSkillCutInCon skillCutinCon;
    private BossCollder bossCollder;
    [SerializeField]
    private NowLoading nowLoading;

    //スキル発動音
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField,Header("スキル発動音")]
    private AudioClip audioClips;

    [SerializeField]
    private GameObject buffEffect;
    private GameObject buffObject;
    [SerializeField]
    private GameObject barrierEffect;
    private GameObject barrierObject;

    [SerializeField]
    private Rigidbody2D rb;

    private float time;
    //低速ボタンが押されているかどうか
    private bool decelerationFlag = false;
    //スキル使った時に使用するフラグ
    private bool[] skillAtkFlag = new bool[]{false,false};
    //バリア用フラグ
    private bool barrierFlag;
    private bool barrierBlinkingFlag = false;
    private float  pBarrierTime;
    //たま消し用のフラグ
    private bool bulletSeverFlag;
    //プレイヤーキャラのバフ使用時のフラグ
    private bool pBaffSkillFlag = false;
    private float pBaffSkillTime;
    private bool baffEndflag = true;
    //デバフ発射時のフラグ
    private bool debuffSkillFlag;
    //スキルボタンが押されたときに使うフラグ
    private bool jKey;
    private bool buttonPish = false;

    //使うかは知らん
    private IEnumerator coroutine;
 
    public bool[] SkillAtkFlag
    {
        get { return this.skillAtkFlag; }
        set { this.skillAtkFlag = value; }
    }

    public bool PBaffSkillFlag
    {
        get { return this.pBaffSkillFlag; }
        set { this.pBaffSkillFlag = value; }
    }

    public bool BulletSeverFlag
    {
        get { return this.bulletSeverFlag; }
        set { this.bulletSeverFlag = value; }
    }

    public bool DebuffSkillFlag
    {
        get { return this.debuffSkillFlag; }
        set { this.debuffSkillFlag = value; }
    }

    public IEnumerator Coroutine
    {
        get { return this.coroutine; }
        set { this.coroutine = value; }
    }

    public bool BarrierFlag { get => barrierFlag; set => barrierFlag = value; }

    private void OnEnable()
    {
        gm = FindObjectOfType<TotalGM>();
        skillDisplay = FindObjectOfType<SkillDisplay_Stage>();
        pouseCon = FindObjectOfType<PouseCon>();
        skillCutinCon = FindObjectOfType<PlayerSkillCutInCon>();
        bossCollder = FindObjectOfType<BossCollder>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //移動制限の座標検知
        for(int i = 0;i < screenWithinChird.Length;i++)
          screenWithinChird[i] = screenWithin.transform.GetChild(i).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(pouseCon.MenuFlag == false && nowLoading.FadeOutFlag == false)
        {
            //スキル実行
            if (buttonPish && (skillAtkFlag[0] || skillAtkFlag[1]))
            {
                buttonPish = false;
                StartCoroutine(SkillAtk());
            }

            //バフスキル時
            if (pBaffSkillFlag == true)
            {
                pBaffSkillTime += Time.deltaTime;
                if (pBaffSkillTime > 5)
                {
                    pBaffSkillTime = 0;
                    pBaffSkillFlag = false;
                    StartCoroutine(BuffEnd());
                }
            }

            //バリア中の減り方
            if(barrierFlag == true)
            {
                pBarrierTime += Time.deltaTime;
                //点滅処理
                if(pBarrierTime > 3 && barrierBlinkingFlag == false)
                {
                    barrierBlinkingFlag = true;
                    StartCoroutine(BarrierBlinking());
                }
                if (pBarrierTime > 5)
                {
                    Destroy(barrierObject);
                    pBarrierTime = 0;
                    barrierFlag = false;
                    barrierBlinkingFlag = false;
                }
            }

            //移動用関数
            if (playerCollider.DeathFlag == false)
                InputSystemMove();

            //ゲームオーバーになったら放物線を描いて落ちる
            else if (playerCollider.DeathFlag == true)
            {
                rb.velocity = Vector3.zero;
                time += Time.deltaTime *0.1f;
                var endpos = new Vector3(transform.position.x - 5f, transform.position.y - 10f);
                var midpos = new Vector3(transform.position.x - 3f, transform.position.y - 5f);
                Vector3 a = Vector3.Lerp(transform.position, midpos, time);
                Vector3 b = Vector3.Lerp(midpos, endpos, time);
                rb.velocity = Vector3.Lerp(a, b, time);
            }
        }
    }

    //WASD移動の入力取得
    public void OnMove(InputAction.CallbackContext context)
    {
        inputV = context.ReadValue<Vector2>();
    }

    //ポーズ発動
    public void OnPouse(InputAction.CallbackContext context)
    {
        if (skillCutinCon.CutInFlag == false)
        pouseCon.Pouse();
    }

    //スキル1発動トリガー
    public void OnFirstSkill(InputAction.CallbackContext context)
    {
        if(skillAtkFlag[0] == false && skillDisplay.SkillCoolFlag[0] == false && playerCollider.DeathFlag == false
            && skillCutinCon.CutInFlag == false && bossCollder.BossDeathFlag == false && nowLoading.FadeOutFlag == false)
        {
            jKey = true;
            skillAtkFlag[0] = true;
            buttonPish = true;
        }
    }

    //スキル2発動トリガー
    public void OnSecondSkill(InputAction.CallbackContext context)
    {
        if (skillAtkFlag[1] == false && skillDisplay.SkillCoolFlag[1] == false && playerCollider.DeathFlag == false
            && skillCutinCon.CutInFlag == false && bossCollder.BossDeathFlag == false && nowLoading.FadeOutFlag == false)
        {
            skillAtkFlag[1] = true;
            buttonPish = true;   
        }
    }

    //低速移動
    public void OnDeceleration(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            //ボタンが押されたらTrue・離されたらFalse
            case InputActionPhase.Performed:
                decelerationFlag = true;
                break;

            case InputActionPhase.Canceled:
                decelerationFlag = false;
                break;
        }
    }

    //移動用関数
    public void InputSystemMove()
    {  　//移動できる時
        if (screenWithinChird[0].transform.position.x  <= transform.position.x &&
            screenWithinChird[1].transform.position.x  >= transform.position.x &&
            screenWithinChird[0].transform.position.y >= transform.position.y &&
            screenWithinChird[1].transform.position.y <= transform.position.y)
        {
            //低速移動
            if (decelerationFlag == true) 
               rb.velocity = inputV * 3f;
            else
               rb.velocity = inputV * 5f;
        }
        else if(screenWithinChird[0].transform.position.x > transform.position.x)
        {
            Vector3 a = transform.position;
            a.x = screenWithinChird[0].transform.position.x;
            transform.position = a;
        }
        else if (screenWithinChird[0].transform.position.y < transform.position.y)
        {
            Vector3 a = transform.position;
            a.y = screenWithinChird[0].transform.position.y;
            transform.position = a;
        }
        else if (screenWithinChird[1].transform.position.x < transform.position.x)
        {
            Vector3 a = transform.position;
            a.x = screenWithinChird[1].transform.position.x;
            transform.position = a;
        }
        else if (screenWithinChird[1].transform.position.y > transform.position.y)
        {
            Vector3 a = transform.position;
            a.y = screenWithinChird[1].transform.position.y;
            transform.position = a;
        }
    }

    //スキル発動本体
    private IEnumerator SkillAtk()
    {
         audioSource.PlayOneShot(audioClips);
        //メインスキルが使われたら
        if(jKey)
        {
            jKey = false;
            //スキル発動とスキルカットインするかどうか
            for (int i = 0; i < gm.PlayerSkill.Length; i++)
            {
                if (gm.PlayerSkill[i] == true)
                {
                    switch (i)
                    {
                        case 0:
                            if(gm.CutinWhetherFlag == true)
                                skillCutinCon.PlayerCutInDisplay(i);
                            barrierFlag = true;
                            barrierObject = Instantiate(barrierEffect, transform.position, Quaternion.identity, transform);
                            break;
                        case 1:
                            if (gm.CutinWhetherFlag == true)
                                skillCutinCon.PlayerCutInDisplay(i);
                            bulletSeverFlag = true;
                            yield return new WaitForSeconds(1f);
                            bulletSeverFlag = false;
                            break;
                        case 2:
                            if (gm.CutinWhetherFlag == true)
                                skillCutinCon.PlayerCutInDisplay(i);
                            debuffSkillFlag = true;
                            break;
                        case 3:
                            if (gm.CutinWhetherFlag == true)
                                skillCutinCon.PlayerCutInDisplay(i);
                            buffObject = Instantiate(buffEffect, transform.position, Quaternion.identity, transform);
                            pBaffSkillFlag = true;
                            break;
                    }
                    yield return new WaitForSeconds(1.5f);
                    skillDisplay.SkillCoolFlag[0] = true;
                    break;
                }
            }
        }
        else
        {
            //スキル発動とスキルカットインするかどうか
            for (int i =0; i < gm.PlayerSubSkill.Length; i++)
            {
                if (gm.PlayerSubSkill[i] == true)
                {
                    switch (i)
                    {
                        case 3:
                            if (gm.CutinWhetherFlag == true)
                                skillCutinCon.PlayerCutInDisplay(i);
                            buffObject =  Instantiate(buffEffect,transform.position,Quaternion.identity,transform);
                            pBaffSkillFlag = true;
                            break;
                        case 2:
                            if (gm.CutinWhetherFlag == true)
                                skillCutinCon.PlayerCutInDisplay(i);
                            debuffSkillFlag = true;
                            break;
                        case 1:
                            if (gm.CutinWhetherFlag == true)
                                skillCutinCon.PlayerCutInDisplay(i);
                            bulletSeverFlag = true;
                            yield return new WaitForSeconds(1f);
                            bulletSeverFlag = false;
                            break;
                        case 0:
                            if (gm.CutinWhetherFlag == true)
                                skillCutinCon.PlayerCutInDisplay(i);
                            barrierFlag = true;
                            barrierObject = Instantiate(barrierEffect, transform.position, Quaternion.identity, transform);
                            break;
                    }
                    yield return new WaitForSeconds(1.5f);
                    skillDisplay.SkillCoolFlag[1] = true;
                    break;
                }
            }
        }
       StopCoroutine(SkillAtk());
    }

    //バリアの点滅
    private IEnumerator BarrierBlinking()
    {
        while (barrierObject != null)
        {
            barrierObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            if(barrierObject != null)
            barrierObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        StopCoroutine(BarrierBlinking());
    }

    //バフ状態が終わったとき
    private IEnumerator BuffEnd()
    {
        var v = buffObject.GetComponent<ParticleSystem>();
        var main = v.main;
        main.simulationSpeed = 1f;
        main.loop = false;

        yield return new WaitForSeconds(1f);

        Destroy(buffObject);
        StopCoroutine(BuffEnd());
    }
}
