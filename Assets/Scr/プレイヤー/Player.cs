using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;
using Spine.Unity;
using Spine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //方向取得用
    private Vector2 inputV;

    //画面内
    [SerializeField]
    private GameObject screenWithin;
    private GameObject[] screenWithinChird =new GameObject[2] {null,null};

    //使うよう
    //private PlayerBulletPool pBulletPool;
    private TotalGM gm;
    private SkillDisplay_Stage skillDisplay;
    private PouseCon pouseCon;
    private PlayerSkillCutInCon skillCutinCon;
    private AreaManager areaManager;

    //低速ボタンが押されているかどうか
    private bool decelerationFlag = false;

    //スキル使った時に使用するフラグ
    private bool[] skillAtkFlag = new bool[]{false,false};

    //移動停止用フラグ
    private bool bossMoveStopFlag;
    //たま消し用のフラグ
    private bool bulletSeverFlag;
    //プレイヤーキャラのバフ使用時のフラグ
    private bool pBaffSkillFlag = false;
    private float pBaffSkillTime;
    //デバフ発射時のフラグ
    private bool debuffSkillFlag;

    //ボタンが押されたときに使うフラグ
    private bool jKey;
    private bool buttonPish = false;

    //使うかは知らん
    private IEnumerator coroutine;
 
    //private Image[] pImage;

    public bool[] SkillAtkFlag
    {
        get { return this.skillAtkFlag; }
        set { this.skillAtkFlag = value; }
    }

    public bool BussMoveStopFlag
    {
        get { return this.bossMoveStopFlag; }
        set { this.bossMoveStopFlag = value; }
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

    private void OnEnable()
    {
        gm = FindObjectOfType<TotalGM>();
        skillDisplay = FindObjectOfType<SkillDisplay_Stage>();
        pouseCon = FindObjectOfType<PouseCon>();
        skillCutinCon = FindObjectOfType<PlayerSkillCutInCon>();
        areaManager = FindObjectOfType<AreaManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //pBulletPool.CreatePool(10);
        for(int i = 0;i < screenWithinChird.Length;i++)
        {
            screenWithinChird[i] = screenWithin.transform.GetChild(i).gameObject;
        }

        //jKey = true;
        //skillAtkFlag[0] = true;
        //gm.PlayerSkill[0] = true;
    }

    // Update is called once per frame
    void Update()
    { 
        //スキル実行
        if (buttonPish && (skillAtkFlag[0] || skillAtkFlag[1]))
        {
            buttonPish = false;
            //coroutine = SkillAtk();
            StartCoroutine(SkillAtk());
        }
        //バフスキル時
        if(pBaffSkillFlag == true)
        {
            pBaffSkillTime += Time.deltaTime;
            if(pBaffSkillTime > 10)
            {
                pBaffSkillTime = 0;
                pBaffSkillFlag = false;
            }
        }
        //移動用関数
        InputSystemMove();
    }

    //WASD移動の入力取得
    public void OnMove(InputAction.CallbackContext context)
    {
        ///Debug.Log(context);
        inputV = context.ReadValue<Vector2>();
    }

    //スキル1発動トリガー
    public void OnFirstSkill(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        if(skillAtkFlag[0] == false && skillDisplay.SkillCoolFlag[0] == false)
        {
            jKey = true;
            skillAtkFlag[0] = true;
            buttonPish = true;
        }
    }

    //スキル2発動トリガー
    public void OnSecondSkill(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        if (skillAtkFlag[1] == false && skillDisplay.SkillCoolFlag[1] ==false)
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
            case InputActionPhase.Performed:
                // ボタンが押された時の処理
                decelerationFlag = true;
                break;

            case InputActionPhase.Canceled:
                // ボタンが離された時の処理
                decelerationFlag = false;
                break;
        }
    }

    //移動用関数
    public void InputSystemMove()
    {  

        if (screenWithinChird[0].transform.position.x  <= transform.position.x &&
            screenWithinChird[1].transform.position.x  >= transform.position.x &&
            screenWithinChird[0].transform.position.y >= transform.position.y &&
            screenWithinChird[1].transform.position.y <= transform.position.y)
        {
            //低速移動
            if (decelerationFlag == true) 
               transform.Translate(inputV.x * Time.deltaTime * 2f, inputV.y * Time.deltaTime * 2f, 0);
            else
               transform.Translate(inputV.x * Time.deltaTime * 5f, inputV.y * Time.deltaTime * 5f, 0);
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
        if(jKey)
        {
            jKey = false;
            //キャンパスなり、アニメーションでスキルカットイン起動
            for (int i = 0; i < gm.PlayerSkill.Length -1; i++)
            {
                if (gm.PlayerSkill[i] == true)
                {
                    if(i == 0 && areaManager.BossActiveFlag == true)
                    {
                        Debug.Log("ボス発見");
                        skillCutinCon.PlayerCutInDisplay(i);
                        bossMoveStopFlag = true;
                    }
                    else if(i == 0 && areaManager.BossActiveFlag == false)
                    {
                        Debug.Log("意味ないよ");
                        skillAtkFlag[0] = false;
                        break;
                    }
                    if (i == 1)
                    {
                        skillCutinCon.PlayerCutInDisplay(i);
                        bulletSeverFlag =true;
                        yield return new WaitForSeconds(1f);
                        bulletSeverFlag = false;
                    }
                    if(i == 2)
                    {
                        skillCutinCon.PlayerCutInDisplay(i);
                        debuffSkillFlag = true;
                    }
                    yield return new WaitForSeconds(3.0f);
                    skillDisplay.SkillCoolFlag[0] = true;
                    break;
                }
            }
        }
        else
        {
            //キャンパスなり、アニメーションでスキルカットイン起動
            for (int i = 3; i > 0; i--)
            {
                if (gm.PlayerSkill[i] == true)
                {
                    skillCutinCon.PlayerCutInDisplay(i);
                    if (i == 3)
                    {
                        pBaffSkillFlag = true;
                    }
                    else if(i == 2)
                    {
                        debuffSkillFlag = true;
                    }
                    else if(i == 1)
                    {
                        Debug.Log(i);
                        bulletSeverFlag = true;
                        yield return new WaitForSeconds(1f);
                        bulletSeverFlag = false;
                    }
                    yield return new WaitForSeconds(3.0f);
                    skillDisplay.SkillCoolFlag[1] = true;
                    break;
                }
            }
        }
        StopCoroutine(SkillAtk());
    }
}
