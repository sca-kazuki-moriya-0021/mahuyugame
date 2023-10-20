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

    private SkillDisplay_Stage skillDisplay;


    public bool[] SkillAtkFlag
    {
        get { return this.skillAtkFlag; }
        set { this.skillAtkFlag = value; }
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
        InputSystemMove();
        
        if(skillAtkFlag[0] == true)
        {
            skillAtkFlag[0] = false;
        }

        if (skillAtkFlag[1] == true)
        {
            skillAtkFlag[1] =  false;
        }

        if (gm.PlayerHp[0] == 0)
        {
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
        Debug.Log(context);
        if(skillAtkFlag[0] == false && skillDisplay.SkillCoolTime[0])
        {
            skillAtkFlag[0] = true;
            Debug.Log("asik");
        }
    }

    public void OnSecondSkill(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (skillAtkFlag[1] == false && skillDisplay.SkillCoolTime[1])
        {
            skillAtkFlag[1] = true;
            Debug.Log("skill");
        }
    }

    public void InputSystemMove()
    {  
        var dire = new Vector3(inputV.x,inputV.y,0);
        transform.Translate(inputV.x * Time.deltaTime * 2f, inputV.y * Time.deltaTime * 5f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bellet"))
        {
            gm.PlayerHp[0]--;
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

    private IEnumerator SkillAtack()
    {
      
        yield return null;
    }
}
