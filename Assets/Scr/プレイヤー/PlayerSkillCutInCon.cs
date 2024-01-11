using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillCutInCon : MonoBehaviour
{
    [SerializeField, Header("�X�L���J�b�g�C���̃C���X�g")]
    private Sprite[] skillSprites;

    [SerializeField, Header("�擾�������摜")]
    private Image getImage;

    [SerializeField,Header("�X�L���J�b�g�C���̕���")]
    private string[] texts;

    [SerializeField,Header("�擾����������")]
    private Text getText;

    [SerializeField,Header("�X�L���J�b�g�C���̃A�j���[�V����")]
    private Animator[] skillAnimator;

    [SerializeField,Header("�X�L���J�b�g�C���w�i�摜")]
    private Sprite skillBarkGround;

    [SerializeField,Header("�擾�������w�i�摜")]
    private Image getbackImage;

    [SerializeField]
    private Canvas myCanvas;

    //�J�b�g�C���������ǂ���
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
        //�J�b�g�C�����Ԑ����p
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
    
    //�v���C���[�̃X�L���J�b�g�C��
    public void PlayerCutInDisplay(int i)
    {
        //�A�j���[�V�����������p
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
