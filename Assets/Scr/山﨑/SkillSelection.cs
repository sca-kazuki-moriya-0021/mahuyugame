using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSelection : MonoBehaviour
{
    [SerializeField]Button button;
    [SerializeField] GameObject GoStageButton;
    [SerializeField] GameObject[] SkillSelect;
    [SerializeField] Button[] Skill;
    [SerializeField]
    private Navigation[] navigation;

    private EventSystem ev = EventSystem.current;

    public GameObject a;
    //[SerializeField] Button[] HighlightSkill;
    GameObject selectedObj;

    int test;

    void Start()
    {

        //É{É^ÉìÇ™ëIëÇ≥ÇÍÇΩèÛë‘Ç…Ç»ÇÈ
       button.Select();
        if (ev.alreadySelecting)
        {

        }
        
   
        //selectedObj = EventSystem.current.currentSelectedGameObject;
        GoStageButton.SetActive(false);
       for(int i=0; i<=3;i++)
        {
            //navigation = Skill[i].GetComponent<Button>().navigation;
            SkillSelect[i].SetActive(false);
        }
    }

    

    public void Skill_0_Click()
    {
        if (SkillSelect[0].activeSelf) { 
            SkillSelect[0].SetActive(false);
            test--;
        }
        else { 
            SkillSelect[0].SetActive(true);
            test++;
            
        }
        GoStage();
        Test();
    }
    public void Skill_1_Click()
    {
        if (SkillSelect[1].activeSelf)
        {
            SkillSelect[1].SetActive(false);
            test--;
        }
        else
        {
            SkillSelect[1].SetActive(true);
            test++;

        }
        GoStage(); Test();
    }
    public void Skill_2_Click()
    {
        if (SkillSelect[2].activeSelf)
        {
            SkillSelect[2].SetActive(false);
            test--;
        }
        else
        {
            SkillSelect[2].SetActive(true);
            test++;

        }
        GoStage(); Test();
    }
    public void Skill_3_Click()
    {
        if (SkillSelect[3].activeSelf)
        {
            SkillSelect[3].SetActive(false);
            test--;
        }
        else
        {
            SkillSelect[3].SetActive(true);
            test++;

        }
        GoStage(); Test();
    }
    /*
    public void HandleMouseEnter()
    {
        if(Skill[1].IsInteractable())
        {
            a.SetActive(false);
        }
        else
        {
            a.SetActive(true);
        }
    }*/
    private void GoStage()
    {
        if(test==2)
        {
            GoStageButton.SetActive(true);
        }
        else
        {
            GoStageButton.SetActive(false);
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
            if(!SkillSelect[i].activeSelf&&test==2)
            {
                Skill[i].interactable = false;
            }
            else
            {
                Skill[i].interactable = true;
            }
        }
    }
}
