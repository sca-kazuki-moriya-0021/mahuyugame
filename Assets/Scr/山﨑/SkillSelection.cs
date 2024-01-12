using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class SkillSelection : MonoBehaviour
{

    [SerializeField] Button button;//最初に選択中にするボタン
    [SerializeField] GameObject goStageButton;
    [SerializeField] GameObject barregeCanvas;//次に出すキャンバス
    [SerializeField] Canvas skillIconCanvas;//スキルアイコンキャンバス
    [SerializeField] Canvas barrageIconCanvas;//武器のアイコンキャンバス（弾幕）
    [SerializeField] GameObject[] skillSelect;//選択しているとき上にかぶせるオブジェ
    [SerializeField] Button[] skill;//スキルのボタン
    [SerializeField] Image[] skillSelectImage;//プレイヤーの手に乗ってるImage
    [SerializeField] Sprite[] skillIcon;//スキルアイコン入れ（上の変数に代入するよう)
    [SerializeField]
    private EventSystem ev = EventSystem.current;
    private GameObject selectedObj;
    [SerializeField] private Image outLine;
    [SerializeField] private Text skillExplanation;//スキルのテキスト
    int skillCount;
    [Header("外枠＿サイズの値")]
    [SerializeField] float outLineSizeS_X;
    [SerializeField] float outLineSizeS_Y;
    [SerializeField] float outLineSizeB_X;
    [SerializeField] float outLineSizeB_Y;
    [SerializeField] private VideoClip[] skillClip;//流したいスキルクリップを配列
    [SerializeField] private VideoPlayer videoPlayer;//Videoを格納
    private TotalGM totalGM;
    private SelectObjGetSet selectObjGetSet;
    //private SkillClip skillClip;
    [SerializeField] GameObject goBarrage;
    bool[] skillJK = {false,false};//Jキーのスキルが選択されてるかの確認
    bool[] skillJ = { false, false, false, false };
    bool[] skillK = { false, false, false, false };
    [SerializeField] private Light2D worldLight2d;//演出用の2DworldLight

    private void Awake()
    {
        selectObjGetSet = FindObjectOfType<SelectObjGetSet>();
        totalGM = FindObjectOfType<TotalGM>();
        PlayerReset();
    }

    void Start()
    {
        //skillClip = FindObjectOfType<SkillClip>();
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
            VideoClip();
            SkillExplanation();
            
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
            SkillEffect();
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
        if (!skillJ[2] && !totalGM.PlayerSkill[2] && !skillJK[0])
        {
            skillSelect[2].SetActive(true);
            skillJ[2] = true;
            skillJK[0] = true;
            skillCount++;
            totalGM.PlayerSkill[2] = true;
            skillSelectImage[0].sprite = skillIcon[2];
        }
        else if (!skillK[2] && !totalGM.PlayerSkill[2] && !skillJK[1] && skillCount == 1)
        {
            skillSelect[2].SetActive(true);
            skillK[2] = true;
            skillJK[1] = true;
            skillCount++;
            totalGM.PlayerSkill[2] = true;
            skillSelectImage[1].sprite = skillIcon[2];
        }
        else if (skillJ[2] && totalGM.PlayerSkill[2])
        {
            skillSelect[2].SetActive(false);
            skillJ[2] = false;
            skillJK[0] = false;
            skillCount--;
            totalGM.PlayerSkill[2] = false;
            skillSelectImage[0].sprite = null;
        }
        else if (skillK[2] && totalGM.PlayerSkill[2])
        {
            skillSelect[2].SetActive(false);
            skillK[2] = false;
            skillJK[1] = false;
            skillCount--;
            totalGM.PlayerSkill[2] = false;
            skillSelectImage[1].sprite = null;
        }
        GoStage();
        Test();
        
    }
    //押されたときの処理
    public void Skill_3_Click()
    {
        if (!skillJ[3] && !totalGM.PlayerSkill[3] && !skillJK[0])
        {
            skillSelect[3].SetActive(true);
            skillJ[3] = true;
            skillJK[0] = true;
            skillCount++;
            totalGM.PlayerSkill[3] = true;
            skillSelectImage[0].sprite = skillIcon[3];
        }
        else if (!skillK[3] && !totalGM.PlayerSkill[3] && !skillJK[1] && skillCount == 1)
        {
            skillSelect[3].SetActive(true);
            skillK[3] = true;
            skillJK[1] = true;
            skillCount++;
            totalGM.PlayerSkill[3] = true;
            skillSelectImage[1].sprite = skillIcon[3];
        }
        else if (skillJ[3] && totalGM.PlayerSkill[3])
        {
            skillSelect[3].SetActive(false);
            skillJ[3] = false;
            skillJK[0] = false;
            skillCount--;
            totalGM.PlayerSkill[3] = false;
            skillSelectImage[0].sprite = null;
        }
        else if (skillK[3] && totalGM.PlayerSkill[3])
        {
            skillSelect[3].SetActive(false);
            skillK[3] = false;
            skillJK[1] = false;
            skillCount--;
            totalGM.PlayerSkill[3] = false;
            skillSelectImage[1].sprite = null;
        }
        GoStage();
        Test();
        
    }

    //ステージに行くボタンの表示・非表示
    private void GoStage()
    {
        if (skillCount == 2)
        {
            goBarrage.SetActive(true);
        }
        else
        {
            goBarrage.SetActive(false);
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
        if (selectedObj.gameObject.layer == LayerMask.NameToLayer("SkillOutLine"))
        {
            outLine.enabled = true;
            outLine.transform.localScale = new Vector2(outLineSizeB_X, outLineSizeB_Y);
            
        }
        else
        {
            outLine.enabled = false;
            //outLine.transform.localScale = new Vector2(outLineSizeS_X, outLineSizeS_Y);
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
        skillIconCanvas.enabled = false;
        barrageIconCanvas.enabled = true;
    }

    private void SkillExplanation()
    {
        switch(selectedObj.tag)
        {
            case "Icon1":
                skillExplanation.text = "skill1の説明文が出ます。";
                break;
            case "Icon2":
                skillExplanation.text = "skill2の説明文が出ます。";
                break;
            case "Icon3":
                skillExplanation.text = "skill3の説明文が出ます。";
                break;
            case "Icon4":
                skillExplanation.text = "skill4の説明文が出ます。";
                break;
        }
    }

    void VideoClip()
    {
        switch (selectedObj.tag)
        {
            case "Icon1":
                videoPlayer.clip = skillClip[0];
                break;
            case "Icon2":
                videoPlayer.clip = skillClip[1];
                break;
            case "Icon3":
                videoPlayer.clip = skillClip[2];
                break;
            case "Icon4":
                videoPlayer.clip = skillClip[3];
                break;
        }
    }
    
    void SkillEffect()
    {
        
    }
}
