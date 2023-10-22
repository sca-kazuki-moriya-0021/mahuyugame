using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDisplay_Stage : MonoBehaviour
{
    [SerializeField] private Sprite[] skillicon;
    [SerializeField] private Image[] skillPosition;
    [SerializeField] private Image[] ui;
    private bool[] skillCoolFlag = new bool[] {true ,true};
    private bool[] test = new bool[] { false, false };
    [SerializeField] private float[] initialCoolTime = new float[] {0,0,0,0};//スキルの設定
    [SerializeField] private float[] skillCoolTime = new float [] {0,0};//ここに選択スキル入れる
    private Player player;
    private TotalGM totalGM;
    //int count;
    //int skillCount=0;
    //private bool[] playerSkill = new bool[] { false, false, false, false };

    public bool[] SkillCoolTime {
        get { return this.skillCoolFlag; }
        set { this.skillCoolFlag = value; }
    }

    // Start is called before the first frame update

    //スキルの表示
    void Start()
    {
        totalGM = FindObjectOfType<TotalGM>();
        player = FindObjectOfType<Player>();
        for ( int count = 0; count <= 3; count++)
        {

            if (totalGM.PlayerSkill[count] == true)
            {
                if (skillPosition[0].sprite == null)
                { 
                    skillPosition[0].sprite = skillicon[count];
                    skillCoolTime[0] = initialCoolTime[count];
                }
                else
                { 
                    skillPosition[1].sprite = skillicon[count];
                    skillCoolTime[1] = initialCoolTime[count];
                }
            }
        }
        ui[0].enabled = false;
        ui[1].enabled = false;
    }

    //ここでスキル発動の検知

    private void Update()
    {
        if(player.SkillAtkFlag[0] && !skillCoolFlag[0])
        {
            
            test[0] = true;
        }
        if (player.SkillAtkFlag[1] &&  !skillCoolFlag[1])
        {
            test[1] = true;
        }
        if (test[0])
        {
            ui[0].enabled = true;
            ui[0].fillAmount -= 1.0f / skillCoolTime[0] * Time.deltaTime;
            if (ui[0].fillAmount <= 0)
            {                
                skillCoolFlag[0] = true;
                ui[0].fillAmount = 1;
                initialCoolTime[0] = 0;
                ui[0].enabled = false;
                test[0] = false;                
            }
        }
        if (test[1])
        {
            ui[1].enabled = true;
            ui[1].fillAmount -= 1.0f / skillCoolTime[1] * Time.deltaTime;
            if (ui[1].fillAmount <= 0)
            {
                skillCoolFlag[1] = true;
                ui[1].fillAmount = 1;
                initialCoolTime[1] = 0;
                ui[1].enabled = false;
                test[1] = false;
            }
        }
    }
}
