using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSkillIcon : MonoBehaviour
{
    private bool skillCanFlag;

    private TotalGM gm;

    public bool SkillCanFlag
    {
        get { return this.skillCanFlag; }
        set { this.skillCanFlag = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<TotalGM>();

        var scene = gm.MyGetScene();

        //前のステージと現在のステージを比較
        //同じシーンなら最初に読み込まれた状態に戻す
        if (scene == gm.BackScene)
        {
            gm.SkillTime[0] = gm.SkillTime[1];
            gm.SkillTime[2] = gm.SkillTime[3];
            gm.SkillTime[4] = gm.SkillTime[5];
            gm.SkillTime[6] = gm.SkillTime[7];
        }
        //違うシーンなら今の状態を保存する
        else if(scene != gm.BackScene)
        {
            gm.SkillTime[1] = gm.SkillTime[0];
            gm.SkillTime[3] = gm.SkillTime[2];
            gm.SkillTime[5] = gm.SkillTime[4];
            gm.SkillTime[7] = gm.SkillTime[6];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
