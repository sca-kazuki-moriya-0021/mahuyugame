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
    //private Button barrageButton;
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
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] PlayableAsset skillTimeLine;
    [SerializeField] Canvas skillEffect;
    [SerializeField] Canvas barrageEffect;
    [SerializeField] private Button barrageButton1;
    [SerializeField] private GameObject barrageButtonCanvas;
    [SerializeField] private GameObject backSkillButton;
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
    [SerializeField]
    [Tooltip("���̂̃p�[�e�B�N��")]
    private ParticleSystem Particle;
    [SerializeField]
    [Tooltip("�����~��ɉ��")]
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
        
        //�{�^�����I�����ꂽ��ԂɂȂ�
        outLineCoroutine = OutLine();
        button.Select();
        goStageButton.SetActive(false);
        for(int i = 0; i < 3; i++)
        {
            fireSkill_Eff[i].Stop();
        }
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
        //Debug.Log(totalGM.PlayerSkill[2]);
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
    //�����ꂽ�Ƃ��̏���
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
    //�����ꂽ�Ƃ��̏���
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
    //�����ꂽ�Ƃ��̏���
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
        //skillExplanationImage.SetActive(false);
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
            if (!totalGM.PlayerSkill[i] && !totalGM.PlayerSubSkill[i] && skillCount == 2)
            {
                skill[i].enabled = false;//interactable�ɂ���Ɣ�������
                skillSelect[i].SetActive(true);
            }
            else
            {
                skill[i].enabled = true;//interactable�ɂ���Ɣ�������
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
                skillExplanation.text = "�o���A\n\n���@������ǂ�W�J����\n����:5�b�ECT: 20�b";
                break;
            case "Icon2":
                skillExplanation.text = "��\n\n��ʏ�ɂ���e������\nCT:20�b";
                break;
            case "Icon3":
                skillExplanation.text = "�|\n\n�{�X�̖h��͂�������\n����:10�b�ECT: 10";
                break;
            case "Icon4":
                skillExplanation.text = "�o�t\n\n���@�̒e���ˊ��o�����炷\n����:5�b�ECT: 15";
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
