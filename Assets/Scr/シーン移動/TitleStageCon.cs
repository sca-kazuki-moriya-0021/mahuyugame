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
    //現在のアニメステーション名
    private string currentStateName;

    //操作説明のアニメーション
    [SerializeField]
    private Animator anim;
    private bool animEndFlag;

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
                titleToggle.SetActive(true);
                AnimationCon(true,false);
                currentStateName = "OpenOperation";
                break;
            //操作説明メニューを閉じる時
            case "OpenOperation" when animEndFlag == true:
                AnimationCon(false,true);
                currentStateName = "CloseOperation";
                //titleButton.SetActive(true);
                titleToggle.SetActive(false);
                break;
            //操作説明メニューを開く時
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
