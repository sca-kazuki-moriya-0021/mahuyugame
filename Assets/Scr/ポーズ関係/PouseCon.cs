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

    [SerializeField]
    private Button[] poseButton;

    [SerializeField, Tooltip("�����̃L�����p�X")]
    private Canvas myCanvas;
    private CountDownCon countDownCon;

    private AudioSource audioSource;

    private bool menuFlag = false;

    private TotalGM totalGM;

    private GameObject selectedObj;

    private Coroutine _currentCoroutine;

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
        //Debug.Log(myCanvas.enabled);
        if (Input.GetKeyDown(KeyCode.Escape) && menuFlag == false)
        {
            menuFlag = true;
            Time.timeScale = 0f;
            myCanvas.enabled = true;
            for (int i = 0; i < 3; i++)
            {
                poseButton[i].enabled = true;
            }
            StartCoroutine(SelectedObj());
        }
    }

    private IEnumerator SelectedObj()
    {
        while (true)
        {
            if (menuFlag == true && countDownCon.CountDownFlag == false)
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
            yield return null;
        }


    }

    //�Q�[���I��
    public void GameEnd()
    {
        audioSource.PlayOneShot(soundE);
        poseButton[2].enabled = false;
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
        selectedObj = null;
        myCanvas.enabled = false;
        audioSource.PlayOneShot(soundE);
        countDownCon.CountDownFlag = true;
        poseButton[0].enabled = false;
        //Debug.Log("���ƂȂ��[");
    }


    //�X�e�[�W�����[�h
    public void StageReload()
    {
        audioSource.PlayOneShot(soundE);
        myCanvas.enabled = false;
        menuFlag = false;
        //if (timeGM.TimeFlag == false)
        {
            Time.timeScale = 1f;
        }
        totalGM.BackScene = totalGM.MyGetScene();
        totalGM.ReloadCurrentScene();
        poseButton[1].enabled = false;

    }
}