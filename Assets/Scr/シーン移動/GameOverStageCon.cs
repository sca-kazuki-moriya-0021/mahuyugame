using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverStageCon : MonoBehaviour
{
    [SerializeField] Button button;
    //���ʉ��p
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip soundE;

    [SerializeField]
    private EventSystem ev = EventSystem.current;
    private TotalGM totalGM;
    private GameObject selectedObj;

    [SerializeField]
    private StageFadeOut fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        totalGM = FindObjectOfType<TotalGM>();
        button.Select();
    }

    // Update is called once per frame
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
            //�A�E�g���C���������œ����
        }
    }

    //�Q�[���I��
    public void GameEnd()
    {
        audioSource.PlayOneShot(soundE);
        fadeOut.GameOverFadeOut("GameEnd");
    }


    //�Q�[���I�[�o�[����X�e�[�W�ɖ߂鎞�Ɏg���֐�
    //���O�Ƀv���C���[�Ƃ���BackScene�ɒl�𓊂��Ă����Ă���𔭓����銴��
    public void ReloadStage()
    {
        audioSource.PlayOneShot(soundE);
        var scene = totalGM.BackScene;
        switch (scene)
        {
            case TotalGM.StageCon.First:
                totalGM.NowScore[0] = 0;
                break;

            case TotalGM.StageCon.Secound:
                totalGM.NowScore[1] = 0;
                break;

            case TotalGM.StageCon.Thead:
                totalGM.NowScore[2] = 0;
                break;
        }
        totalGM.GameOverCount++;
        fadeOut.GameOverFadeOut("ReloadStage");
    }

    public void SkillSelect()
    {
        audioSource.PlayOneShot(soundE);
        fadeOut.GameOverFadeOut("SkillSelect");
    }
}
