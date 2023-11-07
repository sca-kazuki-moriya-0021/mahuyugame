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

    //低速ボタンが押されているかどうか
    private bool decelerationFlag = false;

    //スキル使った時に使用するフラグ
    private bool[] skillAtkFlag = new bool[]{false,false };
    //プレイヤーキャラのバフを使った時のフラグ
    private bool pBaffSkillFlag = false;
    private float pBaffSkillTime;

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
    
    public bool PBaffSkillFlag
    {
        get { return this.pBaffSkillFlag; }
        set { this.pBaffSkillFlag = value; }
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
        //pBulletPool = FindObjectOfType<PlayerBulletPool>();
 
        gm.PlayerWeapon[1] = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //pBulletPool.CreatePool(10);
        for(int i = 0;i < screenWithinChird.Length;i++)
        {
            screenWithinChird[i] = screenWithin.transform.GetChild(i).gameObject;
        }

        //pBaffSkillFlag = true;

        //skillAtkFlag[0] = true;
        //gm.PlayerSkill[0] = true;
    }

    // Update is called once per frame
    void Update()
    { 
        if (buttonPish && (skillAtkFlag[0] || skillAtkFlag[1]))
        {
            buttonPish = false;
            //coroutine = SkillAtk();
            StartCoroutine(SkillAtk());
        }

        if(pBaffSkillFlag == true)
        {
            pBaffSkillTime += Time.deltaTime;
            if(pBaffSkillTime > 10)
            {
                pBaffSkillTime = 0;
                pBaffSkillFlag = false;
            }
        }

       InputSystemMove();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        ///Debug.Log(context);
        inputV = context.ReadValue<Vector2>();
    }

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

    public void OnSecondSkill(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        if (skillAtkFlag[1] == false && skillDisplay.SkillCoolFlag[1] ==false)
        {
            skillAtkFlag[1] = true;
            buttonPish = true;   
        }
    }

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


    public void InputSystemMove()
    {  

        if (screenWithinChird[0].transform.position.x  <= transform.position.x &&
            screenWithinChird[1].transform.position.x  >= transform.position.x &&
            screenWithinChird[0].transform.position.y >= transform.position.y &&
            screenWithinChird[1].transform.position.y <= transform.position.y)
        {
            //低速移動
            if (decelerationFlag == true) { 
                Debug.Log("低速");
                transform.Translate(inputV.x * Time.deltaTime * 2f, inputV.y * Time.deltaTime * 2f, 0);
            }
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

    private IEnumerator SkillAtk()
    {
        //var x = -1;
        var skill = false;
        if(jKey)
        {
            //キャンパスなり、アニメーションでスキルカットイン起動
            for (int i = 0; i < gm.PlayerSkill.Length; i++)
            {
                if (i == 3)
                {
                    Debug.Log("無効な数字です");
                    break;
                }

                if (gm.PlayerSkill[i] == true && skill == false)
                {
                    skill = true;
                    //pImage[x].enabled = true;
                    yield return new WaitForSeconds(3.0f);
                    skillDisplay.SkillCoolFlag[0] = true;
                }
            }
        }
        else
        {
            //キャンパスなり、アニメーションでスキルカットイン起動
            for (int i = 3; i >= 0; i--)
            {
                if (i == 0)
                {
                    Debug.Log("無効な数字です");
                    break;
                }

                if (gm.PlayerSkill[i] == true && skill == false)
                {
                    skill = true;
                    //pImage[x].enabled = true;
                    if(i == 3)
                    {
                        pBaffSkillFlag = true;
                    }
                    yield return new WaitForSeconds(3.0f);
                    skillDisplay.SkillCoolFlag[1] = true;
                }
            }
        }
        skill = false;
        jKey =false;
        StopCoroutine(SkillAtk());
    }
}
