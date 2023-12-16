using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class BarrageSelect : MonoBehaviour
{
    private TotalGM totalGM;
    [SerializeField] Button button;
    [SerializeField] Image[] barrageSelect;//選択しているとき上にかぶせるオブジェ
    [SerializeField] Button[] barrage;//スキルのボタン
    [SerializeField]
    private EventSystem ev = EventSystem.current;
    [SerializeField] private Image outLine;
    private GameObject selectedObj;
    int barrageCount;
    [Header("外枠＿サイズの値")]
    [SerializeField] float outLineSizeS_X;
    [SerializeField] float outLineSizeS_Y;
    [SerializeField] float outLineSizeB_X;
    [SerializeField] float outLineSizeB_Y;
    [SerializeField] GameObject goStageButton;

    private void Awake()
    {
        totalGM = FindObjectOfType<TotalGM>();
    }
    // Start is called before the first frame update
    void Start()
    {
        button.Select();
        for (int i = 0; i <= 3; i++)
        {
            barrageSelect[i].enabled = false;
            totalGM.PlayerWeapon[i] = false;//念のため初期化する
            totalGM.PlayerSubWeapon[i] = false;//念のため初期化する
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (selectedObj == null)
        {
            button.Select();
            selectedObj = ev.currentSelectedGameObject;
        }
        else
        {
            selectedObj = ev.currentSelectedGameObject;
            outLine.transform.position = selectedObj.transform.position;
            //OutLineSize();
        }
    }

    //押されたときの処理
    public void Skill_0_Click()
    {
        if(barrageCount == 0 && !totalGM.PlayerWeapon[0])
        {
            barrageCount++;
            totalGM.PlayerWeapon[0] = true;
            barrageSelect[0].enabled = true;
        }
        else if(barrageCount == 1 && !totalGM.PlayerWeapon[0] && !totalGM.PlayerSubWeapon[0])
        {
            barrageCount++;
            totalGM.PlayerSubWeapon[0] = true;
            barrageSelect[0].enabled = true;
        }
        else
        {
            barrageCount--;
            barrageSelect[0].enabled = false;
            totalGM.PlayerWeapon[0] = false;
            totalGM.PlayerSubWeapon[0] = false;
        }
        TwoSelect();
    }
    //押されたときの処理
    public void Skill_1_Click()
    {
        if (barrageCount == 0 && !totalGM.PlayerWeapon[1])
        {
            barrageCount++;
            totalGM.PlayerWeapon[1] = true;
            barrageSelect[1].enabled = true;
        }
        else if (barrageCount == 1 && !totalGM.PlayerWeapon[1] && !totalGM.PlayerSubWeapon[1])
        {
            barrageCount++;
            totalGM.PlayerSubWeapon[1] = true;
            barrageSelect[1].enabled = true;
        }
        else
        {
            barrageCount--;
            barrageSelect[1].enabled = false;
            totalGM.PlayerWeapon[1] = false;
            totalGM.PlayerSubWeapon[1] = false;
        }
        TwoSelect();
    }
    //押されたときの処理
    public void Skill_2_Click()
    {
        if (barrageCount == 0 && !totalGM.PlayerWeapon[2])
        {
            barrageCount++;
            totalGM.PlayerWeapon[2] = true;
            barrageSelect[2].enabled = true;
        }
        else if (barrageCount == 1 && !totalGM.PlayerWeapon[2] && !totalGM.PlayerSubWeapon[2])
        {
            barrageCount++;
            totalGM.PlayerSubWeapon[2] = true;
            barrageSelect[2].enabled = true;
        }
        else
        {
            barrageCount--;
            barrageSelect[2].enabled = false;
            totalGM.PlayerWeapon[2] = false;
            totalGM.PlayerSubWeapon[2] = false;

        }
        TwoSelect();
    }
    //押されたときの処理
    public void Skill_3_Click()
    {
        if (barrageCount == 0 && !totalGM.PlayerWeapon[3])
        {
            barrageCount++;
            totalGM.PlayerWeapon[3] = true;
            barrageSelect[3].enabled = true;
        }
        else if (barrageCount == 1 && !totalGM.PlayerWeapon[3] && !totalGM.PlayerSubWeapon[3])
        {
            barrageCount++;
            totalGM.PlayerSubWeapon[3] = true;
            barrageSelect[3].enabled = true;
        }
        else
        {
            barrageCount--;
            barrageSelect[3].enabled = false;
            totalGM.PlayerWeapon[3] = false;
            totalGM.PlayerSubWeapon[3] = false;
        }
        TwoSelect();
    }
    void TwoSelect()
    {
        for (int i = 0; i <= 3; i++)
        {
            if (!totalGM.PlayerWeapon[i] && !totalGM.PlayerSubWeapon[i] && barrageCount == 2)
            {
                barrage[i].enabled = false;//interactableにすると半透明化
                barrageSelect[i].enabled = true;
            }
            else
            {
                barrage[i].enabled = true;//interactableにすると半透明化
                barrageSelect[i].enabled = false;
            }

            if (!totalGM.PlayerWeapon[i] && barrageCount == 1)
            {
                //Debug.Log("スキル1ッコ");
                //barrage[i].enabled = false;//interactableにすると半透明化
                barrageSelect[i].enabled = false;
            }
            else if (totalGM.PlayerWeapon[i] && barrageCount == 1)
            {
                barrageSelect[i].enabled = true;
            }

            if (barrageCount == 0)
            {
                barrageSelect[i].enabled= false;
                barrage[i].enabled = true;
            }
        }
        if (barrageCount == 2)
        {
            goStageButton.SetActive(true);
        }
        else
        {
            goStageButton.SetActive(false);
        }

    }
}
