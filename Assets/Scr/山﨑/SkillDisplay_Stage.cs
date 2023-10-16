using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDisplay_Stage : MonoBehaviour
{
    [SerializeField] private Sprite[] skillicon;
    [SerializeField] private Image[] skillPosition;
    //[SerializeField] private Image secondSkill;
    private TotalGM totalGM;
    int count;
    //private bool[] playerSkill = new bool[] { false, false, false, false };

    // Start is called before the first frame update
    void Start()
    {
        totalGM = FindObjectOfType<TotalGM>();
        for ( int count = 0; count <= 3; count++)
        {
            /*
            if (count >= 3) { 
            count=3; }
            */
            if (totalGM.PlayerSkill[count] == true)
            {
                if (skillPosition[0].sprite == null)
                    skillPosition[0].sprite = skillicon[count];
                else
                    skillPosition[1].sprite = skillicon[count];
            }
        }
       
    }
}
