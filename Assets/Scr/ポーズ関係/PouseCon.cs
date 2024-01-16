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
    private Animator anim;

    //メニュー画面が開かれているか
    private bool menuFlag = false;

    private TotalGM totalGM;

    //選択されているオブジェクト
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

    //ゲーム終了
    public void GameEnd()
    { 
        audioSource.PlayOneShot(soundE);
        quitImage.DOFade(1f, 1f).SetUpdate(true).OnComplete(() => {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            //エディタ上の動作
        #else
            Application.Quit();
            //エディタ以外の操作
        #endif
        });
    }

    //ゲームに戻る
    public void BackStage()
    {
        audioSource.PlayOneShot(soundE);
        selectedObj = null;
        anim.SetBool("OpenPouse", false);
        poseButton[0].enabled = false;
        myCanvas.enabled = false;
        countDownCon.CountDownFlag = true;
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