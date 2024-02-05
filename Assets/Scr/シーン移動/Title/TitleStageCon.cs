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
    //現在のアニメステーション名
    private string currentStateName;
    //操作説明のアニメーション
    [SerializeField]
    private Animator operationAnim;
    [SerializeField]
    private Animator settingAnim;
    private bool animEndFlag;

    private bool activeFlag = true;

    //効果音用
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip soundE;

    //ゲーム終了に使う時のImage
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


    //ゲーム終了
    public void GameEnd()
    {
        quitImage.DOFade(2.55f,0.5f).OnComplete(() => {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            //エディタ上の動作
        #else
            Application.Quit();
            //エディタ以外の操作
        #endif
        });
    }

    //スキルセレクト画面に行くとき
    public void SkillSelect()
    {
        audioSource.PlayOneShot(soundE);
        quitImage.DOFade(2.55f, 1f).OnComplete(() => {
            SceneManager.LoadScene("SkillSelect", LoadSceneMode.Single);
        });
    }

    //パネル操作
    public void Opetrue()
    {
        switch (currentStateName)
        {
            //最初に操作説明メニューを開く時
            case "Idle":
                //titleButton.SetActive(false);
                AnimationCon(true,false);
                currentStateName = "OpenOperation";
                break;
            //操作説明メニューを閉じる時
            case "OpenOperation" when animEndFlag == true:
                AnimationCon(false,true);
                currentStateName = "CloseOperation";
                //titleButton.SetActive(true);
                break;
            //操作説明メニューを開く時
            case "CloseOperation" when animEndFlag == true:
                //titleButton.SetActive(false);
                AnimationCon(true, false);
                currentStateName = "OpenOperation";
            break;
        }
    }

    //パネルが開いている時の処理
    private void AnimationCon(bool set,bool active)
    {
        audioSource.PlayOneShot(soundE);
        animEndFlag = false;
        operationAnim.SetBool("Open", set);
        titleBtton[0].SetActive(active);
        titleBtton[2].SetActive(active);
        titleBtton[3].SetActive(active);
    }


    //設定を開くとき
    public void OpenSetting()
    {
        audioSource.PlayOneShot(soundE);
        settingAnim.SetBool("Open", true);
        activeFlag = false;
        for (int i = 0; i < titleBtton.Length; i++)
            titleBtton[i].SetActive(false);

        //セレクトされているオブジェクトを初期化する
        selectedObj = null;
        outLine.enabled = false;
    }

}
