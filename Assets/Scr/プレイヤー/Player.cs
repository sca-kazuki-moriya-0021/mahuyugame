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

    //弾保存用
    [SerializeField]
    //private GameObject[] bullets;

    //時間計測用
    private float waitTime = 0;  
    //使うよう
    //private PlayerBulletPool pBulletPool;
    private TotalGM gm;
    private SkillDisplay_Stage skillDisplay;
    private PouseCon pouseCon;

    private bool decelerationFlag = false;

    //スキル使った時に使用するフラグ
    private bool[] skillAtkFlag = new bool[]{false,false };

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
 
        gm.PlayerWeapon[0] = true;

        //var scene = gm.MyGetScene();

        /*if (scene == gm.BackScene)
        {
           gm.PlayerHp[0] = gm.PlayerHp[1];
        }
        else if (scene != gm.BackScene)
        {
           gm.PlayerHp[1] = gm.PlayerHp[0];
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        //pBulletPool.CreatePool(10);
    }

    // Update is called once per frame
    void Update()
    {
        waitTime += Time.deltaTime;
        if(waitTime > 1.0f)
        {
            //pBulletPool.GetObject(transform.position);
            //Instantiate(bullets[0]);
            //Instantiate(bullets[1]);
            waitTime = 0f;
        }

        if (buttonPish && (skillAtkFlag[0] || skillAtkFlag[1]))
        {
            buttonPish = false;
            //coroutine = SkillAtk();
            StartCoroutine(SkillAtk());
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
        var dire = new Vector3(inputV.x,inputV.y,0);
        if(decelerationFlag == true)
        transform.Translate(inputV.x * Time.deltaTime * 2f, inputV.y * Time.deltaTime * 2f, 0);
        else
        transform.Translate(inputV.x * Time.deltaTime * 5f, inputV.y * Time.deltaTime * 5f, 0);
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
                if (gm.PlayerSkill[i] == true && skill == false)
                {
                    skill = true;
                    //pImage[x].enabled = true;
                    yield return new WaitForSeconds(3.0f);
                }
            }
        }
        else
        {
            //キャンパスなり、アニメーションでスキルカットイン起動
            for (int i = 3; i >= 0; i--)
            {
                if (gm.PlayerSkill[i] == true && skill == false)
                {
                 
                    skill = true;
                    //pImage[x].enabled = true;
                    yield return new WaitForSeconds(3.0f);

                }
            }
        }
        if(jKey) 
        {
            skillDisplay.SkillCoolFlag[0] = true; 
        }
        else 
        {
           skillDisplay.SkillCoolFlag[1] = true;
        }
        skill = false;
        jKey =false;
        StopCoroutine(SkillAtk());
    }
}
