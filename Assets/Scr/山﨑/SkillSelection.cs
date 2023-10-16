using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SkillSelection : MonoBehaviour
{
    [SerializeField]　Button button;
    [SerializeField] GameObject goStageButton;
    [SerializeField] GameObject[] skillSelect;//選択しているとき上にかぶせるオブジェ
    [SerializeField] Button[] skill;//スキルのボタン
    [SerializeField]
    private EventSystem ev = EventSystem.current;
    private GameObject selectedObj;
    [SerializeField] private Image outLine;
    int skillCount;
    [Header("外枠＿サイズの値")]
    [SerializeField]float outLineSizeS;
    [SerializeField]float outLineSizeB;
     private TotalGM totalGM;

    private void Awake()
    {
        totalGM = FindObjectOfType<TotalGM>();
        PlayerReset();
    }

    void Start()
    {
        //ボタンが選択された状態になる
        button.Select();
        goStageButton.SetActive(false);
        //ボタンの選択状態を解除
        for(int i=0; i<=3;i++)
        {
            skillSelect[i].SetActive(false);
            totalGM.PlayerSkill[i] = false;//念のため初期化する
        }
        
    }
    
    void FixedUpdate()
    {
        //Debug.Log(Cursor.lockState);
        //選ばれているオブジェクトを格納している
       
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
        if (skillSelect[0].activeSelf) { 
            skillSelect[0].SetActive(false);
            skillCount--;
            totalGM.PlayerSkill[0] = false;
        }
        else { 
            skillSelect[0].SetActive(true);
            skillCount++;
            totalGM.PlayerSkill[0] = true;
        }
        GoStage();
        Test();
    }
    //押されたときの処理
    public void Skill_1_Click()
    {
        if (skillSelect[1].activeSelf)
        {
            skillSelect[1].SetActive(false);
            skillCount--;
            totalGM.PlayerSkill[1] = false;
        }
        else
        {
            skillSelect[1].SetActive(true);
            skillCount++;
            totalGM.PlayerSkill[1] = true;
        }
        GoStage(); Test();
    }
    //押されたときの処理
    public void Skill_2_Click()
    {
        if (skillSelect[2].activeSelf)
        {
            skillSelect[2].SetActive(false);
            skillCount--;
            totalGM.PlayerSkill[2] = false;
        }
        else
        {
            skillSelect[2].SetActive(true);
            skillCount++;
            totalGM.PlayerSkill[2] = true;
        }
        GoStage(); Test();
    }
    //押されたときの処理
    public void Skill_3_Click()
    {
        if (skillSelect[3].activeSelf)
        {
            skillSelect[3].SetActive(false);
            skillCount--;
            totalGM.PlayerSkill[3] = false;
        }
        else
        {
            skillSelect[3].SetActive(true);
            skillCount++;
            totalGM.PlayerSkill[3] = true;
        }
        GoStage(); Test();
    }
    //ステージに行くボタンの表示・非表示
    private void GoStage()
    {
        if(skillCount==2)
        {
            goStageButton.SetActive(true);
        }
        else
        {
            goStageButton.SetActive(false);
        }
    }
    //押されたらタイトルシーンに行く
    public void GoTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
    //押されたらステージに行く
    public void GoStageScene()
    {
        SceneManager.LoadScene("Stage");
    }
    //スキルが２個選択されたときに選択されていないものを押せないようにする
    private void Test()
    {
        for (int i = 0; i <= 3; i++)
        {
            if(!skillSelect[i].activeSelf&&skillCount==2)
            {
                skill[i].interactable = false;
            }
            else
            {
                skill[i].interactable = true;
            }
        }
    }
    //選択されたボタンによって外枠のサイズを変更
    private void OutLineSize()
    {
        if(selectedObj.gameObject.CompareTag("Button"))
        {
            outLine.transform.localScale = new Vector2 (outLineSizeS,outLineSizeS);
        }
        else
        {
            outLine.transform.localScale = new Vector2 (outLineSizeB,outLineSizeB);
        }
    }

    private void PlayerReset()
    {
        totalGM.PlayerHp[0] = 3;
        totalGM.PlayerHp[1] = 0;

        totalGM.PlayerLevel[0] = 0;
        totalGM.PlayerLevel[1] = 0;

        for (int i = 0; i <= 3; i++)
        {
            totalGM.PlayerWeapon[i] = false;
            totalGM.PlayerSkill[i] = false;
        }

        for(int i = 0; i < totalGM.SkillTime.Length; i++)
        {
            totalGM.SkillTime[i] = 0;
        }
    }
}
