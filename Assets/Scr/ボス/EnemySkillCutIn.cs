using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySkillCutIn : MonoBehaviour
{
    [SerializeField,Header("�G�l�~�[�̃X�L���J�b�g�C���C���X�g")]
    private Sprite[] enemyskillSprites;

    [SerializeField,Header("�擾����摜")]
    private Image getImage;

    [SerializeField,Header("�J�b�g�C���p�̕���")]
    private string[] enemytexts;

    [SerializeField,Header("�擾���镶��")]
    private Text getenemyText;

    [SerializeField,Header("�G�l�~�[�̃X�L���J�b�g�C���̃A�j���[�V����")]
    private Animator[] enemyskillAnimator;

    private Canvas enemyCanvas;

    private bool cutInFlagEnemy;

    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemyCanvas=this.GetComponent<Canvas>();

        enemyCanvas.enabled=false;

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
                enemyCanvas.enabled=false;
            }
        }
    }

    public void EnemyCutInDisplay(int i)
    {
        for(int x = 0; x < enemyskillAnimator.Length; x++)
        {
            enemyskillAnimator[x].Rebind();
            enemyskillAnimator[x].updateMode=AnimatorUpdateMode.UnscaledTime;
        }
        enemyCanvas.enabled=true;
        Time.timeScale=0f;
        getImage.sprite=enemyskillSprites[i];
        getenemyText.text=enemytexts[i];
        cutInFlagEnemy= true;
    }
}
