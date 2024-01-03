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
    bool[] mainSubWepon = { false, false };

    private void Awake()
    {
        totalGM = FindObjectOfType<TotalGM>();
    }
    // Start is called before the first frame update
    void Start()
    {
        mainSubWepon[0] = false;
        mainSubWepon[1] = false;
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
        Debug.Log(mainSubWepon[0]);
        Debug.Log(mainSubWepon[1]);
        Debug.Log(barrageCount);
        if (selectedObj == null)
        {
            button.Select();
            selectedObj = ev.currentSelectedGameObject;
        }
        else
        {
            selectedObj = ev.currentSelectedGameObject;
            outLine.transform.position = selectedObj.transform.position;
            OutLineSize();
        }
    }

    //押されたときの処理
    public void Skill_0_Click()
    {
        if (!mainSubWepon[0] && !totalGM.PlayerSubWeapon[0])
        {
            mainSubWepon[0] = true;
            barrageCount++;
            totalGM.PlayerWeapon[0] = true;
            barrageSelect[0].enabled = true;
        }
        else if (mainSubWepon[0] && !mainSubWepon[1] && !totalGM.PlayerWeapon[0] && barrageCount == 1)
        {
            mainSubWepon[1] = true;
            barrageCount++;
            totalGM.PlayerSubWeapon[0] = true;
            barrageSelect[0].enabled = true;
        }
        else if (totalGM.PlayerWeapon[0])
        {
            //Debug.Log("AA");
            mainSubWepon[0] = false;
            barrageSelect[0].enabled = false;
            totalGM.PlayerWeapon[0] = false;
            barrageCount--;
        }
        else if (totalGM.PlayerSubWeapon[0])
        {
            mainSubWepon[1] = false;
            barrageSelect[0].enabled = false;
            totalGM.PlayerSubWeapon[0] = false;
            barrageCount--;

        }
        TwoSelect();
    }
    //押されたときの処理
    public void Skill_1_Click()
    {
        if (!mainSubWepon[0] && !totalGM.PlayerSubWeapon[1])
        {
            mainSubWepon[0] = true;
            barrageCount++;
            totalGM.PlayerWeapon[1] = true;
            barrageSelect[1].enabled = true;
        }
        else if (mainSubWepon[0] && !mainSubWepon[1] && !totalGM.PlayerWeapon[1] && barrageCount == 1)
        {

            barrageCount++;
            totalGM.PlayerSubWeapon[1] = true;
            barrageSelect[1].enabled = true;
            mainSubWepon[1] = true;
            //Debug.Log("A");
        }
        else if (totalGM.PlayerWeapon[1])
        {
            mainSubWepon[0] = false;
            barrageSelect[0].enabled = false;
            totalGM.PlayerWeapon[1] = false;
            barrageCount--;


        }
        else if (totalGM.PlayerSubWeapon[1])
        {
            mainSubWepon[1] = false;
            barrageSelect[0].enabled = false;
            totalGM.PlayerSubWeapon[1] = false;
            barrageCount--;

        }
        TwoSelect();
    }
    //押されたときの処理
    public void Skill_2_Click()
    {
        if (!mainSubWepon[0] && !totalGM.PlayerSubWeapon[2])
        {
            mainSubWepon[0] = true;
            barrageCount++;
            totalGM.PlayerWeapon[2] = true;
            barrageSelect[2].enabled = true;
        }
        else if (mainSubWepon[0] && !mainSubWepon[1] && !totalGM.PlayerWeapon[2] && barrageCount == 1)
        {
            mainSubWepon[1] = true;
            barrageCount++;
            totalGM.PlayerSubWeapon[2] = true;
            barrageSelect[2].enabled = true;
        }
        else if (totalGM.PlayerWeapon[2])
        {
            mainSubWepon[0] = false;
            barrageSelect[2].enabled = false;
            totalGM.PlayerWeapon[2] = false;
            barrageCount--;
        }
        else if (totalGM.PlayerSubWeapon[2])
        {
            mainSubWepon[1] = false;
            barrageSelect[2].enabled = false;
            totalGM.PlayerSubWeapon[2] = false;
            barrageCount--;

        }
        TwoSelect();
    }
    //押されたときの処理
    public void Skill_3_Click()
    {
        if (!mainSubWepon[0] && !totalGM.PlayerSubWeapon[3])
        {
            mainSubWepon[0] = true;
            barrageCount++;
            totalGM.PlayerWeapon[3] = true;
            barrageSelect[3].enabled = true;
        }
        else if (mainSubWepon[0] && !mainSubWepon[1] && !totalGM.PlayerWeapon[3] && barrageCount == 1)
        {
            mainSubWepon[1] = true;
            barrageCount++;
            totalGM.PlayerSubWeapon[3] = true;
            barrageSelect[3].enabled = true;
        }
        else if (totalGM.PlayerWeapon[3])
        {
            mainSubWepon[0] = false;
            barrageSelect[3].enabled = false;
            totalGM.PlayerWeapon[3] = false;
            barrageCount--;
        }
        else if (totalGM.PlayerSubWeapon[3])
        {
            mainSubWepon[1] = false;
            barrageSelect[3].enabled = false;
            totalGM.PlayerSubWeapon[3] = false;
            barrageCount--;

        }
        TwoSelect();
    }
    private void OutLineSize()
    {
        if (selectedObj.gameObject.CompareTag("Button"))
        {
            outLine.enabled = false;
            outLine.transform.localScale = new Vector2(outLineSizeS_X, outLineSizeS_Y);
        }
        else
        {
            outLine.enabled = true;
            outLine.transform.localScale = new Vector2(outLineSizeB_X, outLineSizeB_Y);

        }
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
                barrageSelect[i].enabled = false;
            }
            else if (totalGM.PlayerWeapon[i] && barrageCount == 1)
            {
                barrageSelect[i].enabled = true;
            }
            
            if (!mainSubWepon[0] &&!totalGM.PlayerSubWeapon[i] && barrageCount == 1)
            {
                Debug.Log("はいったよー");
                barrageSelect[i].enabled = false;
            }
            else if (!mainSubWepon[0] && totalGM.PlayerSubWeapon[i] && barrageCount == 1)
            {
                barrageSelect[i].enabled = true;
            }

            if (barrageCount == 0)
            {
                barrageSelect[i].enabled = false;
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
