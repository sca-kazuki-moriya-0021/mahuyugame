using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PouseCon : MonoBehaviour
{
    //���ʉ�
    [SerializeField]
    private AudioClip soundE;

    [SerializeField]
    private Button button;

    [SerializeField]
    private EventSystem ev = EventSystem.current;

    [SerializeField,Tooltip("�����̃L�����p�X")]
    private Canvas myCanvas;
    private CountDownCon countDownCon;

    private AudioSource audioSource;

    private bool menuFlag = false;

    private TotalGM totalGM;

    private GameObject selectedObj;

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
        countDownCon = FindObjectOfType<CountDownCon>();
        myCanvas.enabled = false;
        //button.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            myCanvas.enabled = true;
            button.Select();
            
        }
    }

    private void FixedUpdate()
    {
        if (selectedObj == null && myCanvas.enabled == true)
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
        countDownCon.CountDownFlag = true;
        myCanvas.enabled = false;
        //Debug.Log("���ƂȂ��[");
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
