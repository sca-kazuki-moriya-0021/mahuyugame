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

    [SerializeField] Button button;//�ŏ��ɑI�𒆂ɂ���{�^��
    [SerializeField] GameObject goStageButton;
    [SerializeField] GameObject barregeCanvas;//���ɏo���L�����o�X
    [SerializeField] Canvas skillIconCanvas;//�X�L���A�C�R���L�����o�X
    [SerializeField] Canvas barrageIconCanvas;//����̃A�C�R���L�����o�X�i�e���j
    [SerializeField] GameObject[] skillSelect;//�I�����Ă���Ƃ���ɂ��Ԃ���I�u�W�F
    [SerializeField] Button[] skill;//�X�L���̃{�^��
    [SerializeField] Image[] skillSelectImage;//�v���C���[�̎�ɏ���Ă�Image
    [SerializeField] Sprite[] skillIcon;//�X�L���A�C�R������i��̕ϐ��ɑ������悤)
    [SerializeField]
    private EventSystem ev = EventSystem.current;
    private GameObject selectedObj;
    private GameObject oldSelectedObj;
    [SerializeField] private Image outLine;
    [SerializeField] private GameObject title;//�^�C�g���ɍs���{�^��
    [SerializeField] private Text skillExplanation;//�X�L���̃e�L�X�g
    int skillCount;
    [Header("�O�g�Q�T�C�Y�̒l")]
    [SerializeField] float outLineSizeS_X;
    [SerializeField] float outLineSizeS_Y;
    [SerializeField] float outLineSizeB_X;
    [SerializeField] float outLineSizeB_Y;
    [SerializeField] private VideoClip[] skillClip;//���������X�L���N���b�v��z��
    [SerializeField] private VideoPlayer videoPlayer;//Video���i�[
    private TotalGM totalGM;
    private SelectObjGetSet selectObjGetSet;
    //private SkillClip skillClip;
    [SerializeField] GameObject goBarrage;
    bool[] skillJK = {false,false};//J�L�[�̃X�L�����I������Ă邩�̊m�F
    bool[] skillJ = { false, false, false, false };
    bool[] skillK = { false, false, false, false };
    [SerializeField] private Light2D worldLight2d;//���o�p��2DworldLight
    [SerializeField] private Animator outLineAnimator;
    [SerializeField] private IEnumerator outLineCoroutine;
    [SerializeField] GameObject skillButtonCanvas;
    [SerializeField] Image skillExplanationImage;
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] PlayableAsset skillTimeLine;
    [SerializeField] Canvas skillEffect;
    [SerializeField] Canvas barrageEffect;
    [SerializeField] private Button barrageButton1;
    [SerializeField] private GameObject barrageButtonCanvas;
    [SerializeField]
    [Tooltip("���̂̃p�[�e�B�N��")]
    private ParticleSystem[] skillParticle;
    [SerializeField]
    [Tooltip("�����~��ɉ��")]
    private ParticleSystem[] skillParticle1;
    [SerializeField]
    [Tooltip("���̂̃p�[�e�B�N��")]
    private ParticleSystem[] barrageParticle;
    [SerializeField]
    [Tooltip("�����~��ɉ��")]
    private ParticleSystem[] barrageParticle1;
    bool check;

    private void Awake()
    {
        selectObjGetSet = FindObjectOfType<SelectObjGetSet>();
        totalGM = FindObjectOfType<TotalGM>();
        PlayerReset();
    }

    void Start()
    {
        
        //�{�^�����I�����ꂽ��ԂɂȂ�
        outLineCoroutine = OutLine();
        button.Select();
        goStageButton.SetActive(false);
        //�{�^���̑I����Ԃ�����
        /*
        for (int i = 0; i <= 3; i++)
        {
            skillSelect[i].SetActive(false);
            totalGM.PlayerSkill[i] = false;//�O�̂��ߏ���������
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
    }

    private void LateUpdate()
    {
        //�P���O�̃I�u�W�F�N�g�ƈ������
        if(oldSelectedObj == selectedObj && !check)
        {
            StartCoroutine(outLineCoroutine);
            
        }
        else if(oldSelectedObj != selectedObj && check)
        {
            OutLineEnd();
        }
    }


    //�����ꂽ�Ƃ��̏���
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
        SkillButton();
    }
    //�����ꂽ�Ƃ��̏���
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
        SkillButton();
        //skillClip.ButtonPush = true;
        
    }
    //�����ꂽ�Ƃ��̏���
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
        SkillButton();
        
    }
    //�����ꂽ�Ƃ��̏���
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
        SkillButton();
        
    }


    //�����ꂽ��^�C�g���V�[���ɍs��
    public void GoTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    //�����ꂽ��X�e�[�W�ɍs��
    public void GoStageScene()
    {
        SceneManager.LoadScene("Stage");
        //nowTime���Z�b�g
    }

    //�����ꂽ��e���ɍs��
    public void GoBarrage()
    {
        title.SetActive(false);
        skillButtonCanvas.SetActive(false);
        skillExplanationImage.enabled = false;
        skillEffect.enabled = true;
        StartCoroutine(SkillEffect_In());
    }

    //�X�e�[�W�ɍs���{�^���̕\���E��\��
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
    
    //�X�L�����Q�I�����ꂽ�Ƃ��ɑI������Ă��Ȃ����̂������Ȃ��悤�ɂ���
    private void SkillButton()
    {
        for (int i = 0; i <= 3; i++)
        {
            if (totalGM.PlayerSkill[i] && skillCount == 2)
            {
                
                skill[i].enabled = true;//interactable�ɂ���Ɣ�������
                skillSelect[i].SetActive(false);
            }
            else if (!totalGM.PlayerSkill[i] && skillCount == 2)
            {
                
                skill[i].enabled = false;
                skillSelect[i].SetActive(true);
            }

            if (!totalGM.PlayerSkill[i] && skillCount == 1)
            {
                //Debug.Log("�X�L��1�b�R");
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
    //�I�����ꂽ�{�^���ɂ���ĊO�g�̃T�C�Y��ύX
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

    //������
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

    private void SkillExplanation()
    {
        switch(selectedObj.tag)
        {
            case "Icon1":
                skillExplanation.text = "skill1�̐��������o�܂��B";
                break;
            case "Icon2":
                skillExplanation.text = "skill2�̐��������o�܂��B";
                break;
            case "Icon3":
                skillExplanation.text = "skill3�̐��������o�܂��B";
                break;
            case "Icon4":
                skillExplanation.text = "skill4�̐��������o�܂��B";
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

    //�A�E�g���C���̃A�j���[�V�������~�߂�A�ꉞ�����ȂƂ���ł������Ȃ̂ō���Ƃ���
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
        for (int i = 0; i < 4; i++)
        {
            skillParticle[i].Stop();
            skillParticle1[i].Stop();
            barrageParticle[i].Stop();
            barrageParticle1[i].Stop();
        }
        //this.gameObject.SetActive(false);
        barrageButton1.Select();
    }
}
