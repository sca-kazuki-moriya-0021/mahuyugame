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
    private Animator skillAnimator;

    [SerializeField,Header("�X�L���J�b�g�C���w�i�摜")]
    private Sprite skillBarkGround;

    [SerializeField,Header("�擾�������w�i�摜")]
    private Image getbackImage;

    [SerializeField]
    private Canvas myCanvas;

    //�J�b�g�C���������ǂ���
    private bool cutInFlag;

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
       
    }
    
    //�v���C���[�̃X�L���J�b�g�C��
    public void PlayerCutInDisplay(int i)
    {
     
       skillAnimator.Rebind();
       skillAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
       myCanvas.enabled = true;
       Time.timeScale = 0f;
       getImage.sprite = skillSprites[i];
       getText.text = texts[i];
       getbackImage.sprite = skillBarkGround;
       cutInFlag = true;
    }

    public void OnAnimationCompleted()
    {
        Time.timeScale = 1f;
        cutInFlag = false;
        myCanvas.enabled = false;
    }
}
