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
            totalGM.CutinWhetherFlag = true;
        }
        if(toggle[0].isOn == false)
        {
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

        if (totalGM.CutinWhetherFlag == true)
            toggle[0].isOn = true;
        else 
            toggle[0].isOn = false;
        
        if (totalGM.LightBlinkingFlag == true)
            toggle[1].isOn = true;
        else
            toggle[1].isOn = false;

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
