using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SkillSelection : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] GameObject goStageButton;//
    [SerializeField] GameObject barregeCanvas;//次に出すキャンバス
    [SerializeField] GameObject[] skillSelect;//選択しているとき上にかぶせるオブジェ
    [SerializeField] Button[] skill;//スキルのボタン
    [SerializeField] Image[] skillSelectImage;//プレイヤーの手に乗ってるImage
    [SerializeField] Sprite[] skillIcon;//スキルアイコン入れ（上の変数に代入するよう)
    [SerializeField]
    private EventSystem ev = EventSystem.current;
    private GameObject selectedObj;
    [SerializeField] private Image outLine;
    int skillCount;
    [Header("外枠＿サイズの値")]
    [SerializeField] float outLineSizeS_X;
    [SerializeField] float outLineSizeS_Y;
    [SerializeField] float outLineSizeB_X;
    [SerializeField] float outLineSizeB_Y;
    private TotalGM totalGM;
    private SelectObjGetSet selectObjGetSet;
    private SkillClip skillClip;
    [SerializeField] GameObject check;
    bool[] skillJK = {false,false};//Jキーのスキルが選択されてるかの確認
    bool[] skillJ = { false, false, false, false };
    bool[] skillK = { false, false, false, false };
    private void Awake()
    {
        selectObjGetSet = FindObjectOfType<SelectObjGetSet>();
        totalGM = FindObjectOfType<TotalGM>();
        PlayerReset();
    }

    void Start()
    {
        skillClip = FindObjectOfType<SkillClip>();
        //ボタンが選択された状態になる
        button.Select();
        goStageButton.SetActive(false);
        //ボタンの選択状態を解除
        for (int i = 0; i <= 3; i++)
        {
            skillSelect[i].SetActive(false);
            totalGM.PlayerSkill[i] = false;//念のため初期化する
        }
    }

    void FixedUpdate()
    {
        Debug.Log(skillJK[0]);
        Debug.Log(skillJK[1]);
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
        if(!skillJ[0]&&!totalGM.PlayerSkill[0] && !skillJK[0])
        {
            skillSelect[0].SetActive(true);
            skillJ[0] = true;
            skillJK[0] = true;
            skillCount++;
            totalGM.PlayerSkill[0] = true;
            skillSelectImage[0].sprite = skillIcon[0];
        }
        else if(!skillK[0] && !totalGM.PlayerSkill[0] && !skillJK[1] && skillCount == 1)
        {
            skillSelect[0].SetActive(true);
            skillK[0] = true;
            skillJK[1] = true;
            skillCount++;
            totalGM.PlayerSkill[0] = true;
            skillSelectImage[1].sprite = skillIcon[0];
        }
        else if(skillJ[0] && totalGM.PlayerSkill[0])
        {
            skillSelect[0].SetActive(false);
            skillJ[0] = false;
            skillJK[0] = false;
            skillCount--;
            totalGM.PlayerSkill[0] = false;
            skillSelectImage[0].sprite = null;
        }
        else if (skillK[0] && totalGM.PlayerSkill[0])
        {
            skillSelect[0].SetActive(false);
            skillK[0] = false;
            skillJK[1] = false;
            skillCount--;
            totalGM.PlayerSkill[0] = false;
            skillSelectImage[1].sprite = null;
        }
        GoStage();
        Test();
        //skillClip.ButtonPush = true;
    }
    //押されたときの処理
    public void Skill_1_Click()
    {
        if (!skillJ[1] && !totalGM.PlayerSkill[1] && !skillJK[0])
        {
            skillSelect[1].SetActive(true);
            skillJ[1] = true;
            skillJK[0] = true;
            skillCount++;
            totalGM.PlayerSkill[1] = true;
            skillSelectImage[0].sprite = skillIcon[1];
        }
        else if (!skillK[1] && !totalGM.PlayerSkill[1] && !skillJK[1] && skillCount == 1)
        {
            skillSelect[1].SetActive(true);
            skillK[1] = true;
            skillJK[1] = true;
            skillCount++;
            totalGM.PlayerSkill[1] = true;
            skillSelectImage[1].sprite = skillIcon[1];
        }
        else if (skillJ[1] && totalGM.PlayerSkill[1])
        {
            skillSelect[1].SetActive(false);
            skillJ[1] = false;
            skillJK[0] = false;
            skillCount--;
            totalGM.PlayerSkill[1] = false;
            skillSelectImage[0].sprite = null;
        }
        else if (skillK[1] && totalGM.PlayerSkill[1])
        {
            skillSelect[1].SetActive(false);
            skillK[1] = false;
            skillJK[1] = false;
            skillCount--;
            totalGM.PlayerSkill[1] = false;
            skillSelectImage[1].sprite = null;
        }
        GoStage();
        Test();
        //skillClip.ButtonPush = true;
        
    }
    //押されたときの処理
    public void Skill_2_Click()
    {
        if (totalGM.PlayerSkill[2])
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
        GoStage();
        Test();
        skillClip.ButtonPush = true;
        
    }
    //押されたときの処理
    public void Skill_3_Click()
    {
        if (totalGM.PlayerSkill[3])
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
        GoStage();
        Test();
        skillClip.ButtonPush = true;
        
    }

    //ステージに行くボタンの表示・非表示
    private void GoStage()
    {
        if (skillCount == 2)
        {
            check.SetActive(true);
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
        //nowTimeリセット
    }
    //スキルが２個選択されたときに選択されていないものを押せないようにする
    private void Test()
    {
        for (int i = 0; i <= 3; i++)
        {
            if (totalGM.PlayerSkill[i] && skillCount == 2)
            {
                
                skill[i].enabled = true;//interactableにすると半透明化
                skillSelect[i].SetActive(false);
            }
            else if (!totalGM.PlayerSkill[i] && skillCount == 2)
            {
                
                skill[i].enabled = false;
                skillSelect[i].SetActive(true);
            }

            if (!totalGM.PlayerSkill[i] && skillCount == 1)
            {
                //Debug.Log("スキル1ッコ");
                skillSelect[i].SetActive(false);
                skill[i].enabled = true;

            }
            else if (totalGM.PlayerSkill[i] && skillCount == 1)
            {
                skillSelect[i].SetActive(true);
                selectObjGetSet.LastSelectButton = skill[i];
            }

            if (skillCount == 0)
            {
                skillSelect[i].SetActive(false);
                skill[i].enabled = true;
            }
        }
    }
    //選択されたボタンによって外枠のサイズを変更
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

    //初期化
    private void PlayerReset()
    {
        totalGM.PlayerHp[0] = 3;
        totalGM.PlayerHp[1] = 0;

        for (int i = 0; i <= 3; i++)
        {
            totalGM.PlayerWeapon[i] = false;
            totalGM.PlayerSubWeapon[i] = false;
            totalGM.PlayerSkill[i] = false;
        }

        for (int i = 0; i < totalGM.NowScore.Length; i++)
        {
            totalGM.NowScore[i] = 0;
        }

        for (int i = 0; i < totalGM.SkillCoolTimeCount.Length; i++)
        {
            totalGM.SkillCoolTimeCount[i] = 0;
        }

        totalGM.GameOverCount = 0;
        totalGM.BackScene = TotalGM.StageCon.No;
    }

    public void GoBarrage()
    {
        this.gameObject.SetActive(false);
        barregeCanvas.SetActive(true);
    }

    private void SkillIcon()
    {
        for (int i = 0; i < totalGM.PlayerSkill.Length; i++)
        {
            if(totalGM.PlayerSkill[i])
            {

            }
        }
    }
}
