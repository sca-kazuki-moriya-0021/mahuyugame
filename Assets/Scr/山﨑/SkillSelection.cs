using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSelection : MonoBehaviour
{
    [SerializeField]Button button;
    [SerializeField] GameObject[] skillSelect;
    [SerializeField] Button[] skill;
    private EventSystem ev;// = EventSystem.current;
    private GameObject outLine;
    private GameObject goStageButton;
    private GameObject selectedObj;

    int test;

    void Start()
    {

        //É{É^ÉìÇ™ëIëÇ≥ÇÍÇΩèÛë‘Ç…Ç»ÇÈ
        button.Select();
        goStageButton = GameObject.Find("Stage");
        outLine = GameObject.Find("outerFrame");
        ev = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        Debug.Log(ev);
        for (int i=0; i<=3;i++)
        {
            
            skillSelect[i].SetActive(false);
        }
        goStageButton.SetActive(false);
    }

    private void FixedUpdate()
    {
        selectedObj = ev.currentSelectedGameObject.gameObject;
        outLine.transform.position = selectedObj.transform.position;
        if(selectedObj.gameObject.tag == "Button")
        {
            outLine.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else
        {
            outLine.transform.localScale = new Vector3(4.5f, 4.5f, 4.5f);
        }
    }

    public void Skill_0_Click()
    {
        if (skillSelect[0].activeSelf) { 
            skillSelect[0].SetActive(false);
            test--;
        }
        else { 
            skillSelect[0].SetActive(true);
            test++;
            
        }
        GoStage();
        Test();
    }
    public void Skill_1_Click()
    {
        if (skillSelect[1].activeSelf)
        {
            skillSelect[1].SetActive(false);
            test--;
        }
        else
        {
            skillSelect[1].SetActive(true);
            test++;

        }
        GoStage(); Test();
    }
    public void Skill_2_Click()
    {
        if (skillSelect[2].activeSelf)
        {
            skillSelect[2].SetActive(false);
            test--;
        }
        else
        {
            skillSelect[2].SetActive(true);
            test++;

        }
        GoStage(); Test();
    }
    public void Skill_3_Click()
    {
        if (skillSelect[3].activeSelf)
        {
            skillSelect[3].SetActive(false);
            test--;
        }
        else
        {
            skillSelect[3].SetActive(true);
            test++;

        }
        GoStage(); Test();
    }
    
    private void GoStage()
    {
        if(test==2)
        {
            goStageButton.SetActive(true);
        }
        else
        {
            goStageButton.SetActive(false);
        }
    }
    public void GoTitleScene()
    {
        //SceneManager.LoadScene("ëJà⁄ÇµÇΩÇ¢Sceneñº");
    }

    public void GoStageScene()
    {
        //SceneManager.LoadScene("ëJà⁄ÇµÇΩÇ¢Sceneñº");
    }



    private void Test()
    {
        for (int i = 0; i <= 3; i++)
        {
            if(!skillSelect[i].activeSelf&&test==2)
            {
                skill[i].interactable = false;
            }
            else
            {
                skill[i].interactable = true;
            }
        }
    }
}
