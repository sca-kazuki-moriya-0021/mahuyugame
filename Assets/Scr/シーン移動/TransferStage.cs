using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransferStage : MonoBehaviour
{
    [SerializeField]
    GameObject Panel = null;
    [SerializeField] Button button;
    //���ʉ��p
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip soundE;
    
    private TotalGM totalGM;

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
    void Update()
    {
        
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
