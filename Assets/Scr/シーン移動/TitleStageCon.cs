using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TitleStageCon: MonoBehaviour
{

    [SerializeField] Button button;
    [SerializeField]
    private GameObject[] titleBtton;
    [SerializeField]
    private GameObject titleToggle;
    //���݂̃A�j���X�e�[�V������
    private string currentStateName;

    //��������̃A�j���[�V����
    [SerializeField]
    private Animator anim;
    private bool animEndFlag;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (selectedObj == null)
        {
            button.Select();
            selectedObj = ev.currentSelectedGameObject;
        }
        else
         selectedObj = ev.currentSelectedGameObject;
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
                titleToggle.SetActive(true);
                AnimationCon(true,false);
                currentStateName = "OpenOperation";
                break;
            //����������j���[����鎞
            case "OpenOperation" when animEndFlag == true:
                AnimationCon(false,true);
                currentStateName = "CloseOperation";
                //titleButton.SetActive(true);
                titleToggle.SetActive(false);
                break;
            //����������j���[���J����
            case "CloseOperation" when animEndFlag == true:
                //titleButton.SetActive(false);
                titleToggle.SetActive(true);
                AnimationCon(true, false);
                currentStateName = "OpenOperation";
            break;
        }
    }

    private void AnimationCon(bool set,bool active)
    {
        audioSource.PlayOneShot(soundE);
        animEndFlag = false;
        anim.SetBool("Open", set);

        titleBtton[0].SetActive(active);
        titleBtton[2].SetActive(active);

    }
}
