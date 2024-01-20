using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverStageCon : MonoBehaviour
{
    [SerializeField] Button button;
    //効果音用
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip soundE;

    [SerializeField]
    private Sprite[] charaSprites;
    [SerializeField]
    private Image getImage;

    [SerializeField] private Image outLine;
    [SerializeField]
    private EventSystem ev = EventSystem.current;
    private TotalGM totalGM;
    private GameObject selectedObj;

    [SerializeField]
    private StageFadeOut fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        totalGM = FindObjectOfType<TotalGM>();
        button.Select();
        getImage.sprite = charaSprites[0];
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
        {
            selectedObj = ev.currentSelectedGameObject;
            outLine.transform.position = selectedObj.transform.position;
        }
    }

    //ゲーム終了
    public void GameEnd()
    {
        audioSource.PlayOneShot(soundE);
        fadeOut.GameOverFadeOut("GameEnd");
    }


    //ゲームオーバーからステージに戻る時に使う関数
    //事前にプレイヤーとかでBackSceneに値を投げておいてこれを発動する感じ
    public void ReloadStage()
    {
        audioSource.PlayOneShot(soundE);
        getImage.sprite = charaSprites[1];
        var scene = totalGM.BackScene;
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
        totalGM.GameOverCount++;
        fadeOut.GameOverFadeOut("ReloadStage");
    }

    public void SkillSelect()
    {
        audioSource.PlayOneShot(soundE);
        fadeOut.GameOverFadeOut("SkillSelect");
    }
}
