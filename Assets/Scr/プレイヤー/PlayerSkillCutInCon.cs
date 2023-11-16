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

    private Canvas myCanvas;

    private bool cutInFlag;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        myCanvas = this.GetComponent<Canvas>();

        myCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(cutInFlag == true)
        {
            time += Time.unscaledDeltaTime;
            if (time >= 3f)
            {
                time = 0;
                Time.timeScale = 1f;
                myCanvas.enabled = false;
            }
        }
    }
    
    public void PlayerCutInDisplay(int i)
    {
       for(int x = 0;x< skillAnimator.Length; x++)
       {
            Debug.Log("入ったお");
            skillAnimator[x].updateMode = AnimatorUpdateMode.UnscaledTime;
       }
       myCanvas.enabled = true;
       time = 0;
       Time.timeScale = 0f;
       getImage.sprite = skillSprites[i];
       getText.text = texts[i];
       cutInFlag = true;
    }
}
