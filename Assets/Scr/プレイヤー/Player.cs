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

    private Vector3 target;
    [SerializeField]
    private GameObject bullet;
    //弾を0.1秒ごとに打ち出す
    private float targetTime = 2f;
    private float currentTime = 0;
    //中継地点を割り振るための変数
    private int count = 0;

    private TotalGM gm;
    private SkillDisplay_Stage skillDisplay;
    private PouseCon pouseCon;

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
       
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.PlayerWeapon[0] == true)
        {
            Debug.Log("aki");
           /* currentTime += Time.deltaTime;
            if (targetTime - gm.PlayerLevel[0] * 1f < currentTime )
            {
                currentTime = 0;
                var pos = transform.position;
                var t = Instantiate(bullet) as GameObject;
                t .gameObject.CompareTag("PlayerBullet");
                t.transform.position = this.transform.position;
                var cash = t.GetComponent<TestBullet>();
                cash.CharaPos = this.transform.position;
                //弾を一つ打ち出すたびに中継地点を変える
                var point = transform.position +new Vector3(transform.position.x + 5f,transform.position.y + 5f ,transform.position.z);
                cash.GreenPos = point;
                cash.PlayerPos = transform.position+new Vector3(transform.position.x + 10f, transform.position.y, transform.position.z);

            }*/
        }
        else if (gm.PlayerWeapon[1] == true)
        {
            Debug.Log("rinka");
            currentTime += Time.deltaTime;
           /* if (targetTime - gm.PlayerLevel[0] * 1f < currentTime)
            {
                currentTime = 0;
                var pos = transform.position;
                var t = Instantiate(bullet) as GameObject;
                t.gameObject.CompareTag("PlayerBullet");
                t.transform.position = this.transform.position;
                var cash = t.GetComponent<TestBullet>();
                cash.CharaPos = this.transform.position;
                //弾を一つ打ち出すたびに中継地点を変える
                var point = transform.position + new Vector3(transform.position.x - 5f, transform.position.y - 5f, transform.position.z);
                cash.GreenPos = point;
                cash.PlayerPos = transform.position + new Vector3(transform.position.x - 10f, transform.position.y, transform.position.z);
            }*/
        }
        else if (gm.PlayerWeapon[2] == true)
        {
            Debug.Log("arika");
        }
        else if (gm.PlayerWeapon[3] == true)
        {
            Debug.Log("sakina");
        }


        if (buttonPish && (skillAtkFlag[0] || skillAtkFlag[1]))
        {
            buttonPish = false;
            //coroutine = SkillAtk();
            StartCoroutine(SkillAtk());
        }

        //if(pouseCon.MenuFlag == false)
        {
            InputSystemMove();
        }
     
       
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

    public void InputSystemMove()
    {  
        var dire = new Vector3(inputV.x,inputV.y,0);
        transform.Translate(inputV.x * Time.deltaTime * 2f, inputV.y * Time.deltaTime * 5f, 0);
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
