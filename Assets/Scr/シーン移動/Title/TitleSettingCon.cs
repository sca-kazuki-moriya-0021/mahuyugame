using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TitleSettingCon : MonoBehaviour
{
    [SerializeField]
    private TitleStageCon stageCon;
    [SerializeField]
    private Toggle[] toggle;
    [SerializeField]
    private Button button;
    [SerializeField]
    private Animator anima;

    [SerializeField]
    private EventSystem ev = EventSystem.current;

    [SerializeField]
    private TotalGM totalGM;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void FixedUpdate()
    {
        if(stageCon.ActiveFlag == false)
        {
            if (stageCon.SelectedObj == null)
            {
                toggle[0].Select();
                stageCon.SelectedObj = ev.currentSelectedGameObject;
            }
            else
                stageCon.SelectedObj = ev.currentSelectedGameObject;
        }
    }

    public void SkillCutInToggle()
    {
        if(toggle[0].isOn == true)
        {
            Debug.Log("入ったよ");
            totalGM.CutinWhetherFlag = true;
        }
        if(toggle[0].isOn == false)
        {
            Debug.Log("外したよ");
            totalGM.CutinWhetherFlag = false;
            Debug.Log(totalGM.CutinWhetherFlag);
        }
            
    }

    public void LightToggle()
    {
        if(toggle[1].isOn == true)
            totalGM.LightBlinkingFlag = true;
        if (toggle[1].isOn == false)
            totalGM.LightBlinkingFlag = false;
    }

    public void Close()
    {
       anima.SetBool("Open", false);
    }

    public void OnAnimationCompleted()
    {
        for(int i = 0; i< toggle.Length; i++)
        {
            toggle[i].gameObject.SetActive(true);
            toggle[i].interactable = true;
        }
        button.gameObject.SetActive(true);
        button.interactable = true;
    }

    //設定画面を閉じるときに使うアニメーション
    public void OnAnimationClose()
    {

        for (int i = 0; i < toggle.Length; i++)
        {
            toggle[i].interactable = false;
            toggle[i].gameObject.SetActive(false);
        }

        button.interactable = false;
        button.gameObject.SetActive(false);

        stageCon.SelectedObj = null;
        stageCon.ActiveFlag = true;
    }
} 
