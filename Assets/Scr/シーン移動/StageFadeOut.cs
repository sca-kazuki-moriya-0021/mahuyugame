using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageFadeOut : MonoBehaviour
{
    [SerializeField]
    private Image backGround;

    private TotalGM totalGM;
    // Start is called before the first frame update
    void Start()
    {
       totalGM = FindObjectOfType<TotalGM>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearFadeOut(string s)
    {
        Tween t = backGround.DOFade(endValue: 1f, duration: 1f);
        t.Play();
        t.OnComplete(() =>{
            if(s == "SkillSelect")
            SceneManager.LoadScene("SkillSelect", LoadSceneMode.Single);
            if(s == "GameEnd")
            {
                #if UNITY_EDITOR
                  UnityEditor.EditorApplication.isPlaying = false;
                //エディタ上の動作
                #else
                 Application.Quit();
                //エディタ以外の操作
                #endif
            }
        });
    }


    public void GameOverFadeOut(string s)
    {
        Tween t = backGround.DOFade(endValue: 1f, duration: 1f);
        t.Play();
        t.OnComplete(() => {
            if (s == "SkillSelect")
                SceneManager.LoadScene("SkillSelect", LoadSceneMode.Single);
            if (s == "GameEnd")
            {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                //エディタ上の動作
                #else
                 Application.Quit();
                //エディタ以外の操作
                #endif
            }
            if(s == "ReloadStage")
            {
                totalGM.ReloadClearScene();
            }
        });
    }
}
