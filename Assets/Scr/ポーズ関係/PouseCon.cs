using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class PouseCon : MonoBehaviour
{
    //���ʉ�
    [SerializeField]
    private AudioClip soundE;

    //�ŏ��ɑI������Ă���{�^��
    [SerializeField]
    private Button button;

    [SerializeField]
    private EventSystem ev = EventSystem.current;

    //�{�^���̐����擾����
    [SerializeField]
    private Button[] poseButton;

    [SerializeField, Tooltip("�����̃L�����p�X")]
    private Canvas myCanvas;
    [SerializeField]
    private CountDownCon countDownCon;

    //�Q�[���I�����Ɏg���L�����p�X
    [SerializeField]
    private Image quitImage;

    private AudioSource audioSource;
    private Animator anim;

    //���j���[��ʂ��J����Ă��邩
    private bool menuFlag = false;

    private TotalGM totalGM;

    //�I������Ă���I�u�W�F�N�g
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
        anim = GetComponent<Animator>();
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        myCanvas = this.GetComponent<Canvas>();
        myCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Pouse()
    {
        if (menuFlag == false)
        {
            anim.SetBool("OpenPouse", true);
            myCanvas.enabled = true;
            menuFlag = true;
            Time.timeScale = 0f;
            for (int i = 0; i < 3; i++)
             poseButton[i].enabled = true;

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
                    selectedObj = ev.currentSelectedGameObject;
            }
            yield return null;
        }
    }

    //�Q�[���I��
    public void GameEnd()
    { 
        audioSource.PlayOneShot(soundE);
        quitImage.DOFade(1f, 1f).SetUpdate(true).OnComplete(() => {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            //�G�f�B�^��̓���
        #else
            Application.Quit();
            //�G�f�B�^�ȊO�̑���
        #endif
        });
    }

    //�Q�[���ɖ߂�
    public void BackStage()
    {
        audioSource.PlayOneShot(soundE);
        selectedObj = null;
        anim.SetBool("OpenPouse", false);
        poseButton[0].enabled = false;
        myCanvas.enabled = false;
        countDownCon.CountDownFlag = true;
    }


    //�X�e�[�W�����[�h
    public void StageReload()
    {
        audioSource.PlayOneShot(soundE);
        var scene = totalGM.MyGetScene();
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

        quitImage.DOFade(1f, 1f).SetUpdate(true).OnComplete(() => {

            anim.SetBool("OpenPouse", false);
            Time.timeScale = 1f;
            myCanvas.enabled = false;
            menuFlag = false;
            totalGM.BackScene = totalGM.MyGetScene();
            totalGM.ReloadCurrentScene();
            poseButton[1].enabled = false;
        });
    }
}