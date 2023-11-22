using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TransferStage : MonoBehaviour
{
    [SerializeField]
    GameObject Panel = null;
    [SerializeField] Button button;
    //���ʉ��p
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip soundE;

    [SerializeField]
    private EventSystem ev = EventSystem.current;
    private TotalGM totalGM;
    private GameObject selectedObj;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        totalGM = FindObjectOfType<TotalGM>();
        if(Panel != null)
        Panel.SetActive(false);
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

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        //�G�f�B�^��̓���
#else
            Application.Quit();
            //�G�f�B�^�ȊO�̑���
#endif

    }

    //�X�L���Z���N�g��ʂɍs���Ƃ�
    public void SkillSelect()
    {
        audioSource.PlayOneShot(soundE);
        SceneManager.LoadScene("SkillSelect", LoadSceneMode.Single);
    }

    //�X�e�[�W�����[�h
    public void StageReload()
    {
        audioSource.PlayOneShot(soundE);

        //if (timeGM.TimeFlag == false)
        {
            Time.timeScale = 1f;
        }

        totalGM.BackScene = totalGM.MyGetScene();
        totalGM.ReloadCurrentScene();

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
        totalGM.ReloadClearScene();
    }

    //�N���A��ʂɂ�����
    //�����g��Ȃ�
    public void Clear()
    {
        audioSource.PlayOneShot(soundE);
        SceneManager.LoadScene("Clear", LoadSceneMode.Single);
    }

    //�X�e�[�W1�Ɉړ�
    public void Stage()
    {
        audioSource.PlayOneShot(soundE);
        SceneManager.LoadScene("Stage", LoadSceneMode.Single);
    }

    /*public void ResetStage()
    {
        audioSource.PlayOneShot(SE);
        SceneManager.LoadScene("Stage", LoadSceneMode.Single);
    }*/
    public void Opetrue()
    {
        if (Panel.activeSelf)
        {
            Panel.SetActive(false);
            audioSource.PlayOneShot(soundE);
            //Time.timeScale = 1.0f;
        }
        else
        {
            Panel.SetActive(true);
            audioSource.PlayOneShot(soundE);
            //Time.timeScale = 0.0f;
        }
    }
}
