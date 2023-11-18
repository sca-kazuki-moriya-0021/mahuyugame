using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PouseCon : MonoBehaviour
{
    //効果音
    [SerializeField]
    private AudioClip soundE;

    [SerializeField]
    private Button button;

    [SerializeField]
    private EventSystem ev = EventSystem.current;

    [SerializeField]
    private Button[] poseButton;

    [SerializeField, Tooltip("自分のキャンパス")]
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
       
    }

    public void Pouse()
    {
        if (menuFlag == false)
        {
            Debug.Log("ポーズ中");

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
                    //アウトラインをここで入れる
                }
            }
            yield return null;
        }


    }

    //ゲーム終了
    public void GameEnd()
    {
        audioSource.PlayOneShot(soundE);
        poseButton[2].enabled = false;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        //エディタ上の動作
#else
            Application.Quit();
            //エディタ以外の操作
#endif

    }

    //ゲームに戻る
    public void BackStage()
    {
        selectedObj = null;
        myCanvas.enabled = false;
        audioSource.PlayOneShot(soundE);
        countDownCon.CountDownFlag = true;
        poseButton[0].enabled = false;
        //Debug.Log("おとなるよー");
    }


    //ステージリロード
    public void StageReload()
    {
        audioSource.PlayOneShot(soundE);
        var scene = totalGM.MyGetScene();
        switch (scene)
        {
            case TotalGM.StageCon.First:
                totalGM.NowTime[0] = 0;
                break;

            case TotalGM.StageCon.Secound:
                totalGM.NowTime[1] += 0;
                break;

            case TotalGM.StageCon.Thead:
                totalGM.NowTime[2] += 0;
                break;
        }
        //if (timeGM.TimeFlag == false)
        {
            Time.timeScale = 1f;
        }
        myCanvas.enabled = false;
        menuFlag = false;
        totalGM.BackScene = totalGM.MyGetScene();
        totalGM.ReloadCurrentScene();
        poseButton[1].enabled = false;

    }
}