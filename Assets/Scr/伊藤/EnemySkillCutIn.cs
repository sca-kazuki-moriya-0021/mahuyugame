using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySkillCutIn : MonoBehaviour
{
    [SerializeField,Header("エネミーのスキルカットインイラスト")]
    private Sprite[] EnemyskillSprites;

    [SerializeField,Header("取得する画像")]
    private Image getImage;

    [SerializeField,Header("カットイン用の文章")]
    private string[] Enemytexts;

    [SerializeField,Header("取得する文章")]
    private Text getEnemyText;

    [SerializeField,Header("エネミーのスキルカットインのアニメーション")]
    private Animator[] EnemyskillAnimator;

    private Canvas EnemyCanvas;

    private bool cutInFlagEnemy;

    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        EnemyCanvas=this.GetComponent<Canvas>();

        EnemyCanvas.enabled=false;

        EnemyCutInDisplay(6);
    }

    // Update is called once per frame
    void Update()
    {
        if (cutInFlagEnemy == true)
        {
            time+=Time.unscaledDeltaTime;
            if (time >= 3f)
            {
                time=0;
                Time.timeScale=1f;
                EnemyCanvas.enabled=false;
            }
        }
    }

    public void EnemyCutInDisplay(int i)
    {
        for(int x = 0; x < EnemyskillAnimator.Length; x++)
        {
            EnemyskillAnimator[x].Rebind();
            EnemyskillAnimator[x].updateMode=AnimatorUpdateMode.UnscaledTime;
        }
        EnemyCanvas.enabled=true;
        Time.timeScale=0f;
        getImage.sprite=EnemyskillSprites[i];
        getEnemyText.text=Enemytexts[i];
        cutInFlagEnemy= true;
    }
}
