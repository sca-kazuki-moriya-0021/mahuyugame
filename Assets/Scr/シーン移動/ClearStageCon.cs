using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;

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
    private EventSystem ev = EventSystem.current;
    private TotalGM totalGM;
    private GameObject selectedObj;

    [SerializeField]
    private StageFadeOut fadeOut;
    [SerializeField]
    private StageFadeIn fadeIn;
    [SerializeField]
    private SelectbuttonMove sb;

    [SerializeField]private Light2D light2D;
    private Color startColor;
    [SerializeField]private Color endColor;

    [SerializeField] private Image outLine;

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
            nowScoreTexts[i].Set(score);

            highScore = totalGM.HighScore[i].ToString("00000000");
            highScoreTexts[i].Set(highScore);
        }

        outLine.enabled = false;
        startColor =  light2D.color;
    }

    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {
        //�X�R�A�\���I�������
        if(sb.SelectFlag == true)
        {
            outLine.enabled = true;
            if (selectedObj == null)
            {
                button.Select();
                selectedObj = ev.currentSelectedGameObject;
            }
            else
            {
                selectedObj = ev.currentSelectedGameObject;
                outLine.transform.position = selectedObj.transform.position;
            }
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

            StartCoroutine(SetColor());

            fadeOut.ClearFadeOut("SkillSelect");
        }
    }

    private IEnumerator SetColor()
    {
        var time = 0f;
        Debug.Log("�����Ă����");
        while (true)
        {
            time += Time.unscaledDeltaTime;
            light2D.color = Color.Lerp(startColor,endColor,time);
            yield return null;
        }
    }
}
