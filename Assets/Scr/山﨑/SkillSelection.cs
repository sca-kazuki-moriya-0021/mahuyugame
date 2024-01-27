using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;
using UnityEngine.Playables;

public class SkillSelection : MonoBehaviour
{

    [SerializeField] Button button;//最初に選択中にするボタン
    //private Button barrageButton;
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
    private GameObject oldSelectedObj;
    [SerializeField] private Image outLine;
    [SerializeField] private GameObject title;//タイトルに行くボタン
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
    [SerializeField] private Animator outLineAnimator;
    [SerializeField] private IEnumerator outLineCoroutine;
    [SerializeField] GameObject skillButtonCanvas;
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] PlayableAsset skillTimeLine;
    [SerializeField] Canvas skillEffect;
    [SerializeField] Canvas barrageEffect;
    [SerializeField] private Button barrageButton1;
    [SerializeField] private GameObject barrageButtonCanvas;
    [SerializeField] private GameObject backSkillButton;
    [SerializeField]
    [Tooltip("球体のパーティクル")]
    private ParticleSystem[] skillParticle;
    [SerializeField]
    [Tooltip("下を円状に回る")]
    private ParticleSystem[] skillParticle1;
    [SerializeField]
    [Tooltip("球体のパーティクル")]
    private ParticleSystem[] barrageParticle;
    [SerializeField]
    [Tooltip("下を円状に回る")]
    private ParticleSystem[] barrageParticle1;
    [SerializeField]
    [Tooltip("球体のパーティクル")]
    private ParticleSystem Particle;
    [SerializeField]
    [Tooltip("下を円状に回る")]
    private ParticleSystem Particle1;
    bool check;
    [SerializeField] BarrageSelect barrageSelect;
    [SerializeField] private ParticleSystem[] fireSkill_Eff;
    [SerializeField] private GameObject fireSkill;
    [SerializeField] private GameObject fireBarrage;

    public Button[] SkillButtonIcon
    {
        get { return this.skill; }
    }

    private void Awake()
    {
        selectObjGetSet = FindObjectOfType<SelectObjGetSet>();
        totalGM = FindObjectOfType<TotalGM>();
        PlayerReset();
    }

    void Start()
    {
        
        //ボタンが選択された状態になる
        outLineCoroutine = OutLine();
        button.Select();
        goStageButton.SetActive(false);
        for(int i = 0; i < 3; i++)
        {
            fireSkill_Eff[i].Stop();
        }
        //ボタンの選択状態を解除
        /*
        for (int i = 0; i <= 3; i++)
        {
            skillSelect[i].SetActive(false);
            totalGM.PlayerSkill[i] = false;//念のため初期化する
        }*/
        oldSelectedObj = ev.currentSelectedGameObject;
        title.SetActive(true);
        SkillButton();
        GoStage();
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
        //Debug.Log(totalGM.PlayerSkill[2]);
    }

    private void LateUpdate()
    {
        //１ｆ前のオブジェクトと違ったら
        if(oldSelectedObj == selectedObj && !check)
        {
            StartCoroutine(outLineCoroutine);
            
        }
        else if(oldSelectedObj != selectedObj && check)
        {
            OutLineEnd();
        }
    }


    //押されたときの処理
    public void Skill_0_Click()
    {
        if (!skillJK[0] && !totalGM.PlayerSubSkill[0])
        {
            skillJK[0] = true;
            skillCount++;
            totalGM.PlayerSkill[0] = true;
            skillSelect[0].SetActive(false);
            skillSelectImage[0].sprite = skillIcon[0];
            fireSkill_Eff[0].Play();
        }
        else if (skillJK[0] && !skillJK[1] && !totalGM.PlayerSkill[0] && skillCount == 1)
        {
            skillJK[1] = true;
            skillCount++;
            totalGM.PlayerSubSkill[0] = true;
            skillSelect[0].SetActive(true);
            skillSelectImage[1].sprite = skillIcon[0];
            fireSkill_Eff[0].Play();
        }
        else if (totalGM.PlayerSkill[0])
        {
            skillJK[0] = false;
            skillSelect[0].SetActive(false);
            totalGM.PlayerSkill[0] = false;
            skillCount--;
            skillSelectImage[0].sprite = null;
        }
        else if (totalGM.PlayerSubSkill[0])
        {
            skillJK[1] = false;
            skillSelect[0].SetActive(false);
            totalGM.PlayerSubSkill[0] = false;
            skillCount--;
            skillSelectImage[1].sprite = null;
        }
        GoStage();
        SkillButton();
    }
    //押されたときの処理
    public void Skill_1_Click()
    {
        if (!skillJK[0] && !totalGM.PlayerSubSkill[1])
        {
            skillJK[0] = true;
            skillCount++;
            totalGM.PlayerSkill[1] = true;
            skillSelect[1].SetActive(false);
            skillSelectImage[0].sprite = skillIcon[1];
            fireSkill_Eff[1].Play();
        }
        else if (skillJK[0] && !skillJK[1] && !totalGM.PlayerSkill[1] && skillCount == 1)
        {
            skillJK[1] = true;
            skillCount++;
            totalGM.PlayerSubSkill[1] = true;
            skillSelect[1].SetActive(true);
            skillSelectImage[1].sprite = skillIcon[1];
            fireSkill_Eff[1].Play();
        }
        else if (totalGM.PlayerSkill[1])
        {
            skillJK[0] = false;
            skillSelect[1].SetActive(false);
            totalGM.PlayerSkill[1] = false;
            skillCount--;
            skillSelectImage[0].sprite = null;
        }
        else if (totalGM.PlayerSubSkill[1])
        {
            skillJK[1] = false;
            skillSelect[1].SetActive(false);
            totalGM.PlayerSubSkill[1] = false;
            skillCount--;
            skillSelectImage[1].sprite = null;
        }
        GoStage();
        SkillButton();
        //skillClip.ButtonPush = true;
        
    }
    //押されたときの処理
    public void Skill_2_Click()
    {
        if (!skillJK[0] && !totalGM.PlayerSubSkill[2])
        {
            skillJK[0] = true;
            skillCount++;
            totalGM.PlayerSkill[2] = true;
            skillSelect[2].SetActive(false);
            skillSelectImage[0].sprite = skillIcon[2];
            fireSkill_Eff[2].Play();
        }
        else if (skillJK[0] && !skillJK[1] && !totalGM.PlayerSkill[2] && skillCount == 1)
        {
            skillJK[1] = true;
            skillCount++;
            totalGM.PlayerSubSkill[2] = true;
            skillSelect[2].SetActive(true);
            skillSelectImage[1].sprite = skillIcon[2];
            fireSkill_Eff[2].Play();
        }
        else if (totalGM.PlayerSkill[2])
        {
            skillJK[0] = false;
            skillSelect[2].SetActive(false);
            totalGM.PlayerSkill[2] = false;
            skillCount--;
            skillSelectImage[0].sprite = null;
        }
        else if (totalGM.PlayerSubSkill[2])
        {
            skillJK[1] = false;
            skillSelect[2].SetActive(false);
            totalGM.PlayerSubSkill[2] = false;
            skillCount--;
            skillSelectImage[1].sprite = null;
        }
        GoStage();
        SkillButton();
        
    }
    //押されたときの処理
    public void Skill_3_Click()
    {
        if (!skillJK[0] && !totalGM.PlayerSubSkill[3])
        {
            skillJK[0] = true;
            skillCount++;
            totalGM.PlayerSkill[3] = true;
            skillSelect[3].SetActive(false);
            skillSelectImage[0].sprite = skillIcon[3];
            fireSkill_Eff[3].Play();
        }
        else if (skillJK[0] && !skillJK[1] && !totalGM.PlayerSkill[3] && skillCount == 1)
        {
            skillJK[1] = true;
            skillCount++;
            totalGM.PlayerSubSkill[3] = true;
            skillSelect[3].SetActive(true);
            skillSelectImage[1].sprite = skillIcon[3];
            fireSkill_Eff[3].Play();
        }
        else if (totalGM.PlayerSkill[3])
        {
            skillJK[0] = false;
            skillSelect[3].SetActive(false);
            totalGM.PlayerSkill[3] = false;
            skillCount--;
            skillSelectImage[0].sprite = null;
        }
        else if (totalGM.PlayerSubSkill[3])
        {
            skillJK[1] = false;
            skillSelect[3].SetActive(false);
            totalGM.PlayerSubSkill[3] = false;
            skillCount--;
            skillSelectImage[1].sprite = null;
        }
        GoStage();
        SkillButton();
        
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

    //押されたら弾幕に行く
    public void GoBarrage()
    {
        title.SetActive(false);
        skillButtonCanvas.SetActive(false);
        //skillExplanationImage.SetActive(false);
        skillEffect.enabled = true;
        StartCoroutine(SkillEffect_In());
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
    
    //スキルが２個選択されたときに選択されていないものを押せないようにする
    private void SkillButton()
    {
        for (int i = 0; i <= 3; i++)
        {
            if (!totalGM.PlayerSkill[i] && !totalGM.PlayerSubSkill[i] && skillCount == 2)
            {
                skill[i].enabled = false;//interactableにすると半透明化
                skillSelect[i].SetActive(true);
            }
            else
            {
                skill[i].enabled = true;//interactableにすると半透明化
                skillSelect[i].SetActive(false);
            }

            if (!totalGM.PlayerSkill[i] && skillCount == 1)
            {
                skillSelect[i].SetActive(false);
            }
            else if (totalGM.PlayerSkill[i] && skillCount == 1)
            {
                skillSelect[i].SetActive(true);
            }

            if (!skillJK[0] && !totalGM.PlayerSubSkill[i] && skillCount == 1)
            {
                skillSelect[i].SetActive(false);
            }
            else if (!skillJK[0] && totalGM.PlayerSubWeapon[i] && skillCount == 1)
            {
                skillSelect[i].SetActive(true);
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
            totalGM.PlayerSubSkill[i] = false;
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

    private void SkillExplanation()
    {
        switch (selectedObj.tag)
        {
            case "Icon1":
                skillExplanation.text = "バリア\n\n自機を守る障壁を展開する\n効果:5秒・CT: 20秒";
                break;
            case "Icon2":
                skillExplanation.text = "刀\n\n画面上にある弾を消す\nCT:20秒";
                break;
            case "Icon3":
                skillExplanation.text = "弓\n\nボスの防御力を下げる\n効果:10秒・CT: 10";
                break;
            case "Icon4":
                skillExplanation.text = "バフ\n\n自機の弾発射感覚を減らす\n効果:5秒・CT: 15";
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

    //アウトラインのアニメーションを止める、一応いろんなところでつかつかもなので作っといた
    void OutLineEnd()
    {
        StopCoroutine(outLineCoroutine);
        outLineCoroutine = null;
        outLineAnimator.SetBool("OutLine", false);
        outLineCoroutine = OutLine();
        oldSelectedObj = selectedObj;
        check = false;
    }


    IEnumerator OutLine()
    {
        check = true;
        yield return new WaitForSeconds(1);
        outLineAnimator.SetBool("OutLine", true);
    }

    IEnumerator SkillEffect_In()
    {
        fireSkill.SetActive(false);
        goBarrage.SetActive(false);
        playableDirector.Play(skillTimeLine);
        yield return new WaitForSeconds(2);
        
        skillEffect.enabled = false;
        barrageEffect.enabled = true;
        yield return new WaitForSeconds(0.3f);
        skillIconCanvas.enabled = false;
        barrageIconCanvas.enabled = true;
        yield return new WaitForSeconds(2.5f);
        
        barrageEffect.enabled = false;
        barregeCanvas.SetActive(true);
        barrageButtonCanvas.SetActive(true);
        fireBarrage.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            skillParticle[i].Stop();
            skillParticle1[i].Stop();
            barrageParticle[i].Stop();
            barrageParticle1[i].Stop();
            if(totalGM.PlayerWeapon[i])
            {
                barrageSelect.BarrageButton[i].Select();
            }
        }
        if(barrageSelect.BarrageCount==2)
        {
            goStageButton.SetActive(true);
        }
        Particle.Stop();
        Particle1.Stop();
        backSkillButton.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
