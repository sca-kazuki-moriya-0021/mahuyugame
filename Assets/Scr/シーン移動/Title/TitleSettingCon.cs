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
    private GameObject selectedObj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ev.currentSelectedGameObject);
    }
    void FixedUpdate()
    {
        /*if(selectedObj == toggle[0].gameObject)
        {
            Debug.Log("入っている");
            toggle[0].Select();
            selectedObj = ev.currentSelectedGameObject;
        }
        else if(selectedObj == toggle[1].gameObject)
        {
            toggle[1].Select();
            selectedObj = ev.currentSelectedGameObject;
        }
        else if(selectedObj == button.gameObject)
        {
            button.Select();
            selectedObj = ev.currentSelectedGameObject;
        }*/
    }

    public void Close()
    {
       anima.SetBool("Open", false);
    }

    public void OnAnimationCompleted()
    {
        stageCon.AnimEndFlag = true;
        for(int i = 0; i< toggle.Length; i++)
        {
            toggle[i].gameObject.SetActive(true);
            toggle[i].interactable = true;
        }
        button.interactable = true;
        button.gameObject.SetActive(true);
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

        stageCon.ActiveFlag = true;
        stageCon.AnimEndFlag = true;
    }
} 
