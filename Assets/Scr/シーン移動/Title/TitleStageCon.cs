using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TitleStageCon: MonoBehaviour
{

    [SerializeField] private Image outLine;
    [SerializeField] Button button;
    [SerializeField]
    private GameObject[] titleBtton;
    //���݂̃A�j���X�e�[�V������
    private string currentStateName;
    //��������̃A�j���[�V����
    [SerializeField]
    private Animator operationAnim;
    [SerializeField]
    private Animator settingAnim;
    private bool animEndFlag;

    private bool activeFlag = true;

    //���ʉ��p
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip soundE;

    //�Q�[���I���Ɏg������Image
    [SerializeField]
    private Image quitImage;

    [SerializeField]
    private EventSystem ev = EventSystem.current;
    private GameObject selectedObj;

    public bool AnimEndFlag { get => animEndFlag; set => animEndFlag = value; }
    public bool ActiveFlag { get => activeFlag; set => activeFlag = value; }
    public GameObject SelectedObj { get => selectedObj; set => selectedObj = value; }

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        button.Select();
        currentStateName = "Idle";
    }

    private void Update()
    {
        if(activeFlag == true)
        {
            for(int i =0;i < titleBtton.Length; i++)
            titleBtton[i].SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if (activeFlag == true)
        {
            outLine.enabled = true;
            if (SelectedObj == null)
            {
                button.Select();
                SelectedObj = ev.currentSelectedGameObject;
            }
            else
            {
                SelectedObj = ev.currentSelectedGameObject;
                outLine.transform.position = SelectedObj.transform.position;
            }
        }
    }


    //�Q�[���I��
    public void GameEnd()
    {
        quitImage.DOFade(2.55f,0.5f).OnComplete(() => {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            //�G�f�B�^��̓���
        #else
            Application.Quit();
            //�G�f�B�^�ȊO�̑���
        #endif
        });
    }

    //�X�L���Z���N�g��ʂɍs���Ƃ�
    public void SkillSelect()
    {
        audioSource.PlayOneShot(soundE);
        quitImage.DOFade(2.55f, 1f).OnComplete(() => {
            SceneManager.LoadScene("SkillSelect", LoadSceneMode.Single);
        });
    }

    //�p�l������
    public void Opetrue()
    {
        switch (currentStateName)
        {
            //�ŏ��ɑ���������j���[���J����
            case "Idle":
                //titleButton.SetActive(false);
                AnimationCon(true,false);
                currentStateName = "OpenOperation";
                break;
            //����������j���[����鎞
            case "OpenOperation" when animEndFlag == true:
                AnimationCon(false,true);
                currentStateName = "CloseOperation";
                //titleButton.SetActive(true);
                break;
            //����������j���[���J����
            case "CloseOperation" when animEndFlag == true:
                //titleButton.SetActive(false);
                AnimationCon(true, false);
                currentStateName = "OpenOperation";
            break;
        }
    }

    //�p�l�����J���Ă��鎞�̏���
    private void AnimationCon(bool set,bool active)
    {
        audioSource.PlayOneShot(soundE);
        animEndFlag = false;
        operationAnim.SetBool("Open", set);
        titleBtton[0].SetActive(active);
        titleBtton[2].SetActive(active);
        titleBtton[3].SetActive(active);
    }


    //�ݒ���J���Ƃ�
    public void OpenSetting()
    {
        audioSource.PlayOneShot(soundE);
        settingAnim.SetBool("Open", true);
        activeFlag = false;
        for (int i = 0; i < titleBtton.Length; i++)
            titleBtton[i].SetActive(false);

        //�Z���N�g����Ă���I�u�W�F�N�g������������
        selectedObj = null;
        outLine.enabled = false;
    }

}
