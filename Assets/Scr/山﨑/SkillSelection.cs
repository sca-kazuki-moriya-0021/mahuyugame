using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SkillSelection : MonoBehaviour
{
    [SerializeField]�@Button button;
    [SerializeField] GameObject goStageButton;
    [SerializeField] GameObject[] skillSelect;//�I�����Ă���Ƃ���ɂ��Ԃ���I�u�W�F
    [SerializeField] Button[] skill;//�X�L���̃{�^��
    [SerializeField]
    private EventSystem ev = EventSystem.current;
    private GameObject selectedObj;
    [SerializeField] private Image outLine;
    int skillCount;
    [Header("�O�g�Q�T�C�Y�̒l")]
    [SerializeField] float outLineSizeS_X;
    [SerializeField] float outLineSizeS_Y;
    [SerializeField] float outLineSizeB_X;
    [SerializeField] float outLineSizeB_Y;
    private TotalGM totalGM;
    private SkillClip skillClip;

    private void Awake()
    {
        totalGM = FindObjectOfType<TotalGM>();       
        PlayerReset();
    }

    void Start()
    {
        skillClip = FindObjectOfType<SkillClip>();
        //�{�^�����I�����ꂽ��ԂɂȂ�
        button.Select();
        goStageButton.SetActive(false);
        //�{�^���̑I����Ԃ�����
        for(int i=0; i<=3;i++)
        {
            skillSelect[i].SetActive(false);
            totalGM.PlayerSkill[i] = false;//�O�̂��ߏ���������
        }
        
    }
    
    void FixedUpdate()
    {
        //Debug.Log(ev.currentSelectedGameObject);
        //�I�΂�Ă���I�u�W�F�N�g���i�[���Ă���
       
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
    //�����ꂽ�Ƃ��̏���
    public void Skill_0_Click()
    {
        if (totalGM.PlayerSkill[0]) { 
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
        //OneSkillCheck();
        Test();
        skillClip.ButtonPush = true;
    }
    //�����ꂽ�Ƃ��̏���
    public void Skill_1_Click()
    {
        if (totalGM.PlayerSkill[1])
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
        GoStage();  
        //OneSkillCheck(); 
        Test();
        skillClip.ButtonPush = true;
    }
    //�����ꂽ�Ƃ��̏���
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
        //OneSkillCheck(); 
        Test();
        skillClip.ButtonPush = true;
    }
    //�����ꂽ�Ƃ��̏���
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
        //OneSkillCheck(); 
        Test(); //NoSkillCheck();
        skillClip.ButtonPush = true;
    }

    void NoSkillCheck()
    {
        for (int i = 0; i <= 3; i++)
        {
            if (!totalGM.PlayerSkill[i]&&skillCount==0)
            {
                skillSelect[i].SetActive(false);
            }
        }
    }

    void OneSkillCheck()
    {
        for (int i = 0; i <= 3; i++)
        {
            if (!totalGM.PlayerSkill[i])
            {
                skillSelect[i].SetActive(false);
            }
            else
            {
                skillSelect[i].SetActive(true);
            }
        }
    }
    //�X�e�[�W�ɍs���{�^���̕\���E��\��
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
    //�X�L�����Q�I�����ꂽ�Ƃ��ɑI������Ă��Ȃ����̂������Ȃ��悤�ɂ���
    private void Test()
    {
        for (int i = 0; i <= 3; i++)
        {
            if(!totalGM.PlayerSkill[i] && skillCount == 2)
            {
                //Debug.Log(skillSelect[i]);
                skill[i].enabled = false;
                skillSelect[i].SetActive(true);
            }
            else if(totalGM.PlayerSkill[i] && skillCount == 2)
            {
                skill[i].enabled = true;//interactable�ɂ���Ɣ�������
                skillSelect[i].SetActive(false);
            }

            if(!totalGM.PlayerSkill[i]&&skillCount==1)
            {
                //Debug.Log("�X�L��1�b�R");
                skillSelect[i].SetActive(false);
                skill[i].enabled = true;
            }
            else if(totalGM.PlayerSkill[i] && skillCount == 1)
            {
                skillSelect[i].SetActive(true);
            }

            if(skillCount==0)
            {
                skillSelect[i].SetActive(false);
                skill[i].enabled = true;
            }
        }
    }
    //�I�����ꂽ�{�^���ɂ���ĊO�g�̃T�C�Y��ύX
    private void OutLineSize()
    {
        if(selectedObj.gameObject.CompareTag("Button"))
        {
            outLine.transform.localScale = new Vector2 (outLineSizeS_X,outLineSizeS_Y);
        }
        else
        {
            outLine.transform.localScale = new Vector2 (outLineSizeB_X,outLineSizeB_Y);
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

        for(int i = 0; i < totalGM.NowScore.Length; i++)
        {
            totalGM.NowScore[i] = 0;
        }

        for(int i = 0; i < totalGM.SkillCoolTimeCount.Length; i++)
        {
            totalGM.SkillCoolTimeCount[i] = 0;
        }

        totalGM.GameOverCount = 0;
        totalGM.BackScene = TotalGM.StageCon.No;
    }
}
