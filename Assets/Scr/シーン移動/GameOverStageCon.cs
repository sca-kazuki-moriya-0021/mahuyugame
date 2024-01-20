using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverStageCon : MonoBehaviour
{
    [SerializeField] Button button;
    //���ʉ��p
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip soundE;

    [SerializeField]
    private Sprite[] charaSprites;
    [SerializeField]
    private Image getImage;

    [SerializeField] private Image outLine;
    [SerializeField]
    private EventSystem ev = EventSystem.current;
    private TotalGM totalGM;
    private GameObject selectedObj;

    [SerializeField]
    private StageFadeOut fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        totalGM = FindObjectOfType<TotalGM>();
        button.Select();
        getImage.sprite = charaSprites[0];
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
            outLine.transform.position = selectedObj.transform.position;
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
        getImage.sprite = charaSprites[1];
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
