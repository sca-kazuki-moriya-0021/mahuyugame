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
    private Vector2 inputV;
    private TotalGM gm;

    private bool[] skillAtkFlag = new bool[]{false,false };

    private bool buttonPish = false;

    private SkillDisplay_Stage skillDisplay;
    private IEnumerator coroutine;
    private bool jKey;
   

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
        if(buttonPish && (skillAtkFlag[0] || skillAtkFlag[1]))
        {
            buttonPish = false;
            //coroutine = SkillAtk();
            StartCoroutine(SkillAtk());
        }
        InputSystemMove();
        
        if (gm.PlayerHp[0] == 0)
        {
            gm.PlayerTransForm = this.transform.position;
            gm.BackScene = gm.MyGetScene();
            SceneManager.LoadScene("GameOver");
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
        if(skillAtkFlag[0] == false && skillDisplay.SkillCoolTime[0] == false)
        {
            jKey = true;
            skillAtkFlag[0] = true;
            buttonPish = true;
        }
    }

    public void OnSecondSkill(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        if (skillAtkFlag[1] == false && skillDisplay.SkillCoolTime[1] ==false)
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
        var test = false;
        if(jKey)
        {
            //キャンパスなり、アニメーションでスキルカットイン起動
            for (int i = 0; i < gm.PlayerSkill.Length; i++)
            {
                if (gm.PlayerSkill[i] == true && !test)
                {
                    test = true;
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
                if (gm.PlayerSkill[i] == true && !test)
                {
                 
                    test = true;
                    //pImage[x].enabled = true;
                    yield return new WaitForSeconds(3.0f);

                }
            }
        }
        if(jKey) skillDisplay.SkillCoolTime[0] = true;
        else skillDisplay.SkillCoolTime[1] = true;
        //coroutine = null;
        jKey=false;
        StopCoroutine(SkillAtk());
    }
}
