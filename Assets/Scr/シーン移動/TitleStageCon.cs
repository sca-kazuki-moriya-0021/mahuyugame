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

    //現在のアニメステーション名
    private string currentStateName;

    [SerializeField]
    private Animator anim;

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
        SceneManager.LoadScene("SkillSelect", LoadSceneMode.Single);
    }

    //パネル操作
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
