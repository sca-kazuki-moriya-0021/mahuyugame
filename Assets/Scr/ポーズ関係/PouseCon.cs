using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PouseCon : MonoBehaviour
{
    //���ʉ�
    [SerializeField]
    private AudioClip soundE;

    [SerializeField,Tooltip("�����̃L�����p�X")]
    private Canvas myCanvas;
    [SerializeField,Header("�J�E���g�_�E���L�����p�X")]
    private Canvas countDownCanvas;

    private AudioSource audioSource;

    private bool menuFlag = false;

    private TotalGM totalGM;

    public bool MenuFlag
    {
        get { return this.menuFlag; }
        set { this.menuFlag = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        totalGM = FindObjectOfType<TotalGM>();
        audioSource = GetComponent<AudioSource>();
        myCanvas = this.GetComponent<Canvas>();
        countDownCanvas = GetComponent<Canvas>();
        myCanvas.enabled = false;
        countDownCanvas.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            myCanvas.enabled = true;
        }
    }

    //�Q�[���I��
    public void GameEnd()
    {
        audioSource.PlayOneShot(soundE);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        //�G�f�B�^��̓���
#else
            Application.Quit();
            //�G�f�B�^�ȊO�̑���
#endif

    }

    //�Q�[���ɖ߂�
    public void BackStage()
    {
        audioSource.PlayOneShot(soundE);
        Time.timeScale = 1f;
        myCanvas.enabled = false;
        countDownCanvas.enabled = false;
    }


    //�X�e�[�W�����[�h
    public void StageReload()
    {
        audioSource.PlayOneShot(soundE);

        myCanvas.enabled = true;
        //if (timeGM.TimeFlag == false)
        {
            Time.timeScale = 1f;
        }

        totalGM.BackScene = totalGM.MyGetScene();
        totalGM.ReloadCurrentScene();

    }
}
