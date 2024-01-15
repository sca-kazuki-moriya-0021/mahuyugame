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

    //���݂̃A�j���X�e�[�V������
    private string currentStateName;

    [SerializeField]
    private Animator anim;

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
        SceneManager.LoadScene("SkillSelect", LoadSceneMode.Single);
    }

    //�p�l������
    public void Opetrue()
    {
        switch (currentStateName)
        {
            case "Idle":

                anim.SetBool("Open", true);

                titleBtton[0].SetActive(false);
                titleBtton[2].SetActive(false);

                audioSource.PlayOneShot(soundE);
                currentStateName = "OpenOperation";
 
            break;

            case "OpenOperation":
                anim.SetBool("Open", false);
                titleBtton[0].SetActive(true);
                titleBtton[2].SetActive(true);

                audioSource.PlayOneShot(soundE);
                currentStateName = "Idle";
                break;

        }
    }
}
