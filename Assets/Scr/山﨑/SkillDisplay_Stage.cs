using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDisplay_Stage : MonoBehaviour
{
    [SerializeField] private Sprite[] skillicon;
    [SerializeField] private Image[] skillPosition;
    [SerializeField] private Image[] ui;
    private bool[] skillCoolFlag = new bool[] {false ,false};
    [SerializeField] private float[] coolTime = new float[] {0,0,0,0};
    private Player player;
    private TotalGM totalGM;
    int count;
    //private bool[] playerSkill = new bool[] { false, false, false, false };

    public bool[] SkillCoolTime {
        get { return this.skillCoolFlag; }
        set { this.skillCoolFlag = value; }
    }

    // Start is called before the first frame update

    //ƒXƒLƒ‹‚Ì•\Ž¦
    void Start()
    {
        totalGM = FindObjectOfType<TotalGM>();
        player = FindObjectOfType<Player>();
        for ( int count = 0; count <= 3; count++)
        {

            if (totalGM.PlayerSkill[count] == true)
            {
                if (skillPosition[0].sprite == null)
                    skillPosition[0].sprite = skillicon[count];
                else
                    skillPosition[1].sprite = skillicon[count];
            }
        }

    }

    //coolTime
    private void Update()
    {
        if(/*player.SkillAtkFlag[0] &&*/ !skillCoolFlag[0])
        {
            skillCoolFlag[0] = false;
            ui[0].fillAmount -= 1.0f / coolTime[0] * Time.deltaTime;
            if (ui[0].fillAmount <= 0)
            {

                skillCoolFlag[0] = true;
                ui[0].fillAmount = 1;
                coolTime[0] = 0;
            }
        }

        if (/*player.SkillAtkFlag[1] && */ !skillCoolFlag[1])
        {
            skillCoolFlag[1] = false;
            ui[1].fillAmount -= 1.0f / coolTime[1] * Time.deltaTime;
            if(ui[1].fillAmount <= 0)
            {

                skillCoolFlag[1] = true;
                ui[1].fillAmount = 1;
                coolTime[1] = 0;
            }
        }
    }
}
