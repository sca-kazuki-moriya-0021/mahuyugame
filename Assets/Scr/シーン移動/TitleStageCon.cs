using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TitleStageCon: MonoBehaviour
{
    [SerializeField]
    GameObject Panel = null;
    [SerializeField] Button button;
    [SerializeField]
    private GameObject[] titleBtton;
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
    private TotalGM totalGM;
    private GameObject selectedObj;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        totalGM = FindObjectOfType<TotalGM>();

        Panel.SetActive(false);
        button.Select();
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
        if (Panel.activeSelf)
        {
            Panel.SetActive(false);
            
            titleBtton[0].SetActive(true);
            titleBtton[2].SetActive(true);
            
            audioSource.PlayOneShot(soundE);
            //Time.timeScale = 1.0f;
        }
        else
        {
            anim.SetTrigger("change");
            
            
            titleBtton[0].SetActive(false);
            titleBtton[2].SetActive(false);

            audioSource.PlayOneShot(soundE);
            
            //Time.timeScale = 0.0f;
        }
    }
}
