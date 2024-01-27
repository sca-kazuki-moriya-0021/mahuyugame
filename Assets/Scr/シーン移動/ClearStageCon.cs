using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClearStageCon : MonoBehaviour
{
    [SerializeField] Button button;
    //���ʉ��p
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip soundE;

    [SerializeField]
    private ResultScoreDisplay[] nowScoreTexts;

    //private Text[] nowScoreText;
    [SerializeField]
    private ResultScoreDisplay[] highScoreTexts;

    [SerializeField]
    private Animator anim;
    //�A�j���[�V�����N�������ۂ̃t���O
    private bool animFlag;
    private bool animEndFlag = false;

    [SerializeField]
    private EventSystem ev = EventSystem.current;
    private TotalGM totalGM;
    private GameObject selectedObj;

    [SerializeField]
    private StageFadeOut fadeOut;
    [SerializeField]
    private StageFadeIn fadeIn;
    [SerializeField]
    private SelectbuttonMove sb;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        totalGM = FindObjectOfType<TotalGM>();
        string score;
        string highScore;

        for(int i=0;i < 3; i++)
        {
            if(totalGM.HighScore[i] <= totalGM.NowScore[i])
                totalGM.HighScore[i] = totalGM.NowScore[i];

            score = totalGM.NowScore[i].ToString("00000000");
            Debug.Log(totalGM.NowScore[i]);
            nowScoreTexts[i].Set(score);

            highScore = totalGM.HighScore[i].ToString("00000000");
            highScoreTexts[i].Set(highScore);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeIn.FadeInFlag == false && animFlag == false)
        {
            animFlag = true;
            anim.SetBool("ResultSet",true);
        }

    }

    void FixedUpdate()
    {
        //�X�R�A�\���I�������
        if(sb.SelectFlag == true)
        {
            if (selectedObj == null)
            {
                button.Select();
                selectedObj = ev.currentSelectedGameObject;
            }
            else
                selectedObj = ev.currentSelectedGameObject;
        }
    }

    //�Q�[���I��
    public void GameEnd()
    {
        if(sb.SelectFlag == true)
        {
            audioSource.PlayOneShot(soundE);
            fadeOut.ClearFadeOut("GameEnd");
        }
    }

    //�X�L���Z���N�g��ʂɍs���Ƃ�
    public void SkillSelect()
    {
        if(sb.SelectFlag == true)
        {
            audioSource.PlayOneShot(soundE);
            if (totalGM.BackSideFlag == false && totalGM.GameOverCount == 0)
                totalGM.BackSideFlag = true;
            else if (totalGM.BackSideFlag == true || totalGM.GameOverCount > 0)
                totalGM.BackSideFlag = false;

            fadeOut.ClearFadeOut("SkillSelect");
        }
    }

    //�A�j���[�V�����I��茟�m�p
    //public void OnAnimationCompleted() =>animEndFlag = true;
}
