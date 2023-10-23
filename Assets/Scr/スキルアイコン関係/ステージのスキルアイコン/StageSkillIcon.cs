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
            gm.SkillCoolTimeCount[0] = gm.SkillCoolTimeCount[1];
            gm.SkillCoolTimeCount[2] = gm.SkillCoolTimeCount[3];
            gm.SkillCoolTimeCount[4] = gm.SkillCoolTimeCount[5];
            gm.SkillCoolTimeCount[6] = gm.SkillCoolTimeCount[7];
        }
        //違うシーンなら今の状態を保存する
        else if(scene != gm.BackScene)
        {
            gm.SkillCoolTimeCount[1] = gm.SkillCoolTimeCount[0];
            gm.SkillCoolTimeCount[3] = gm.SkillCoolTimeCount[2];
            gm.SkillCoolTimeCount[5] = gm.SkillCoolTimeCount[4];
            gm.SkillCoolTimeCount[7] = gm.SkillCoolTimeCount[6];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
