using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillCutInCon : MonoBehaviour
{
    [SerializeField, Header("スキルカットインのイラスト")]
    private Sprite[] skillSprites;

    [SerializeField, Header("取得したい画像")]
    private Image getImage;

    [SerializeField,Header("スキルカットインの文章")]
    private string[] texts;

    [SerializeField,Header("取得したい文章")]
    private Text getText;

    [SerializeField,Header("スキルカットインのアニメーション")]
    private Animator[] skillAnimator;

    [SerializeField,Header("スキルカットイン背景画像")]
    private Sprite skillBarkGround;

    [SerializeField,Header("取得したい背景画像")]
    private Image getbackImage;

    [SerializeField]
    private Canvas myCanvas;

    //カットインしたかどうか
    private bool cutInFlag;

    private float time = 0;

    public bool CutInFlag
    {
        get { return this.cutInFlag; }
        set { this.cutInFlag = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        myCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //カットイン時間制限用
        if (cutInFlag == true)
        {
            time += Time.unscaledDeltaTime;
            if (time >= 1.5f)
            {
                time = 0;
                Time.timeScale = 1f;
                cutInFlag = false;
                myCanvas.enabled = false;
            }
        }
    }
    
    //プレイヤーのスキルカットイン
    public void PlayerCutInDisplay(int i)
    {
        //アニメーション初期化用
       for(int x = 0; x < skillAnimator.Length; x++)
       {
            skillAnimator[x].Rebind();
            skillAnimator[x].updateMode = AnimatorUpdateMode.UnscaledTime;
       }
       myCanvas.enabled = true;
       Time.timeScale = 0f;
       getImage.sprite = skillSprites[i];
       getText.text = texts[i];
       getbackImage.sprite = skillBarkGround;
       cutInFlag = true;
    }
}
