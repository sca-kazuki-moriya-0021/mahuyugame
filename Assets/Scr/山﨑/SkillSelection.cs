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
    [SerializeField]float outLineSizeS;
    [SerializeField]float outLineSizeB;
     private TotalGM totalGM;

    void Start()
    {
        totalGM = FindObjectOfType<TotalGM>();
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
        //�I�΂�Ă���I�u�W�F�N�g���i�[���Ă���
        selectedObj = ev.currentSelectedGameObject;
        outLine.transform.position = selectedObj.transform.position;
        OutLineSize();
        //Debug.Log(totalGM.PlayerSkill[1]);
    }
    //�����ꂽ�Ƃ��̏���
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
    //�����ꂽ�Ƃ��̏���
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
    //�����ꂽ�Ƃ��̏���
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
    //�����ꂽ�Ƃ��̏���
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
    }
    //�X�L�����Q�I�����ꂽ�Ƃ��ɑI������Ă��Ȃ����̂������Ȃ��悤�ɂ���
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
    //�I�����ꂽ�{�^���ɂ���ĊO�g�̃T�C�Y��ύX
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
}
