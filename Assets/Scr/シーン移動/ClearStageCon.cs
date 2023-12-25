using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClearStageCon : MonoBehaviour
{
    [SerializeField] Button button;
    //効果音用
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip soundE;

    [SerializeField]
    private EventSystem ev = EventSystem.current;
    private TotalGM totalGM;
    private GameObject selectedObj;

    [SerializeField]
    private StageFadeOut fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        totalGM = FindObjectOfType<TotalGM>();
        button.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
            //アウトラインをここで入れる
        }
    }

    //ゲーム終了
    public void GameEnd()
    {
        audioSource.PlayOneShot(soundE);
        fadeOut.ClearFadeOut("GameEnd");
    }

    public void SkillSelect()
    {
        audioSource.PlayOneShot(soundE);

        if (totalGM.BackSideFlag == false && totalGM.GameOverCount == 0)
            totalGM.BackSideFlag = true;
        else if (totalGM.BackSideFlag == true || totalGM.GameOverCount > 0)
            totalGM.BackSideFlag = false;

        fadeOut.ClearFadeOut("SkillSelect");
    }
}
