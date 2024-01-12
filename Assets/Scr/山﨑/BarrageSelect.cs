using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BarrageSelect : MonoBehaviour
{
    private TotalGM totalGM;
    [SerializeField] Button button;
    [SerializeField] Image[] barrageSelect;//�I�����Ă���Ƃ���ɂ��Ԃ���I�u�W�F
    [SerializeField] Button[] barrage;//�X�L���̃{�^��
    [SerializeField]
    private EventSystem ev = EventSystem.current;
    [SerializeField] private Image outLine;
    private GameObject selectedObj;
    int barrageCount;
    [Header("�O�g�Q�T�C�Y�̒l")]
    [SerializeField] float outLineSizeS_X;
    [SerializeField] float outLineSizeS_Y;
    [SerializeField] float outLineSizeB_X;
    [SerializeField] float outLineSizeB_Y;
    [SerializeField] GameObject goStageButton;
    [SerializeField] Image[] barrageSelectImage;//�v���C���[�̎�ɏ���Ă�Image
    [SerializeField] Sprite[] barrageIcon;//�e���A�C�R������i��̕ϐ��ɑ������悤)
    bool[] mainSubWepon = { false, false };
    [SerializeField] private VideoClip[] barrageClip;//�����������N���b�v��z��
    [SerializeField] private VideoPlayer videoPlayer;//Video���i�[
    [SerializeField] private Text barrageExplanation;//���̃e�L�X�g

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
            totalGM.PlayerWeapon[i] = false;//�O�̂��ߏ���������
            totalGM.PlayerSubWeapon[i] = false;//�O�̂��ߏ���������
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
            BarrageExplanation();
            VideoClip();
        }
    }

    //�����ꂽ�Ƃ��̏���
    public void Skill_0_Click()
    {
        if (!mainSubWepon[0] && !totalGM.PlayerSubWeapon[0])
        {
            mainSubWepon[0] = true;
            barrageCount++;
            totalGM.PlayerWeapon[0] = true;
            barrageSelect[0].enabled = true;
            barrageSelectImage[0].sprite = barrageIcon[0];
        }
        else if (mainSubWepon[0] && !mainSubWepon[1] && !totalGM.PlayerWeapon[0] && barrageCount == 1)
        {
            mainSubWepon[1] = true;
            barrageCount++;
            totalGM.PlayerSubWeapon[0] = true;
            barrageSelect[0].enabled = true;
            barrageSelectImage[1].sprite = barrageIcon[0];
        }
        else if (totalGM.PlayerWeapon[0])
        {
            //Debug.Log("AA");
            mainSubWepon[0] = false;
            barrageSelect[0].enabled = false;
            totalGM.PlayerWeapon[0] = false;
            barrageCount--;
            barrageSelectImage[0].sprite = null;
        }
        else if (totalGM.PlayerSubWeapon[0])
        {
            mainSubWepon[1] = false;
            barrageSelect[0].enabled = false;
            totalGM.PlayerSubWeapon[0] = false;
            barrageCount--;
            barrageSelectImage[1].sprite = null;
        }
        TwoSelect();
    }
    //�����ꂽ�Ƃ��̏���
    public void Skill_1_Click()
    {
        if (!mainSubWepon[0] && !totalGM.PlayerSubWeapon[1])
        {
            mainSubWepon[0] = true;
            barrageCount++;
            totalGM.PlayerWeapon[1] = true;
            barrageSelect[1].enabled = true;
            barrageSelectImage[0].sprite = barrageIcon[1];
        }
        else if (mainSubWepon[0] && !mainSubWepon[1] && !totalGM.PlayerWeapon[1] && barrageCount == 1)
        {

            barrageCount++;
            totalGM.PlayerSubWeapon[1] = true;
            barrageSelect[1].enabled = true;
            mainSubWepon[1] = true;
            barrageSelectImage[1].sprite = barrageIcon[1];
        }
        else if (totalGM.PlayerWeapon[1])
        {
            mainSubWepon[0] = false;
            barrageSelect[0].enabled = false;
            totalGM.PlayerWeapon[1] = false;
            barrageCount--;
            barrageSelectImage[0].sprite = null;

        }
        else if (totalGM.PlayerSubWeapon[1])
        {
            mainSubWepon[1] = false;
            barrageSelect[0].enabled = false;
            totalGM.PlayerSubWeapon[1] = false;
            barrageCount--;
            barrageSelectImage[1].sprite = null;
        }
        TwoSelect();
    }
    //�����ꂽ�Ƃ��̏���
    public void Skill_2_Click()
    {
        if (!mainSubWepon[0] && !totalGM.PlayerSubWeapon[2])
        {
            mainSubWepon[0] = true;
            barrageCount++;
            totalGM.PlayerWeapon[2] = true;
            barrageSelect[2].enabled = true;
            barrageSelectImage[0].sprite = barrageIcon[2];
        }
        else if (mainSubWepon[0] && !mainSubWepon[1] && !totalGM.PlayerWeapon[2] && barrageCount == 1)
        {
            mainSubWepon[1] = true;
            barrageCount++;
            totalGM.PlayerSubWeapon[2] = true;
            barrageSelect[2].enabled = true;
            barrageSelectImage[1].sprite = barrageIcon[2];
        }
        else if (totalGM.PlayerWeapon[2])
        {
            mainSubWepon[0] = false;
            barrageSelect[2].enabled = false;
            totalGM.PlayerWeapon[2] = false;
            barrageCount--;
            barrageSelectImage[0].sprite = null;
        }
        else if (totalGM.PlayerSubWeapon[2])
        {
            mainSubWepon[1] = false;
            barrageSelect[2].enabled = false;
            totalGM.PlayerSubWeapon[2] = false;
            barrageCount--;
            barrageSelectImage[1].sprite = null;

        }
        TwoSelect();
    }
    //�����ꂽ�Ƃ��̏���
    public void Skill_3_Click()
    {
        if (!mainSubWepon[0] && !totalGM.PlayerSubWeapon[3])
        {
            mainSubWepon[0] = true;
            barrageCount++;
            totalGM.PlayerWeapon[3] = true;
            barrageSelect[3].enabled = true;
            barrageSelectImage[0].sprite = barrageIcon[3];
        }
        else if (mainSubWepon[0] && !mainSubWepon[1] && !totalGM.PlayerWeapon[3] && barrageCount == 1)
        {
            mainSubWepon[1] = true;
            barrageCount++;
            totalGM.PlayerSubWeapon[3] = true;
            barrageSelect[3].enabled = true;
            barrageSelectImage[1].sprite = barrageIcon[3];
        }
        else if (totalGM.PlayerWeapon[3])
        {
            mainSubWepon[0] = false;
            barrageSelect[3].enabled = false;
            totalGM.PlayerWeapon[3] = false;
            barrageCount--;
            barrageSelectImage[0].sprite = null;
        }
        else if (totalGM.PlayerSubWeapon[3])
        {
            mainSubWepon[1] = false;
            barrageSelect[3].enabled = false;
            totalGM.PlayerSubWeapon[3] = false;
            barrageCount--;
            barrageSelectImage[1].sprite = null;
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
                barrage[i].enabled = false;//interactable�ɂ���Ɣ�������
                barrageSelect[i].enabled = true;
            }
            else
            {
                barrage[i].enabled = true;//interactable�ɂ���Ɣ�������
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
                Debug.Log("�͂�������[");
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
    private void BarrageExplanation()
    {
        switch (selectedObj.tag)
        {
            case "Icon1":
                barrageExplanation.text = "�e��1�̐��������o�܂��B";
                break;
            case "Icon2":
                barrageExplanation.text = "�e��2�̐��������o�܂��B";
                break;
            case "Icon3":
                barrageExplanation.text = "�e��3�̐��������o�܂��B";
                break;
            case "Icon4":
                barrageExplanation.text = "�e��4�̐��������o�܂��B";
                break;
        }
    }

    void VideoClip()
    {
        switch (selectedObj.tag)
        {
            case "Icon1":
                videoPlayer.clip = barrageClip[0];
                break;
            case "Icon2":
                videoPlayer.clip = barrageClip[1];
                break;
            case "Icon3":
                videoPlayer.clip = barrageClip[2];
                break;
            case "Icon4":
                videoPlayer.clip = barrageClip[3];
                break;
        }
    }
}
