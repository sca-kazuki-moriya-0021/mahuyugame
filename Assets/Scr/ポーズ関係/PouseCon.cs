using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class PouseCon : MonoBehaviour
{
    //効果音
    [SerializeField]
    private AudioClip soundE;

    //最初に選択されているボタン
    [SerializeField]
    private Button button;

    [SerializeField]
    private EventSystem ev = EventSystem.current;

    //ボタンの数を取得する
    [SerializeField]
    private Button[] poseButton;

    [SerializeField, Tooltip("自分のキャンパス")]
    private Canvas myCanvas;
    [SerializeField]
    private CountDownCon countDownCon;

    //ゲーム終了時に使うキャンパス
    [SerializeField]
    private Image quitImage;

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
            menuFlag = true;
            Time.timeScale = 0f;
            myCanvas.enabled = true;
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

    //ゲーム終了
    public void GameEnd()
    { 
        audioSource.PlayOneShot(soundE);
        //quitImage.DOFade(2.55f, 0.5f).OnComplete(() => {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            //エディタ上の動作
        #else
            Application.Quit();
            //エディタ以外の操作
        #endif
        //});
    }

    //ゲームに戻る
    public void BackStage()
    {
        selectedObj = null;
        myCanvas.enabled = false;
        audioSource.PlayOneShot(soundE);
        countDownCon.CountDownFlag = true;
        poseButton[0].enabled = false;
    }


    //ステージリロード
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
        Time.timeScale = 1f;
        myCanvas.enabled = false;
        menuFlag = false;
        totalGM.BackScene = totalGM.MyGetScene();
        totalGM.ReloadCurrentScene();
        poseButton[1].enabled = false;
    }
}