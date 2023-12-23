using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine.Unity;
using Spine;

public class PlayerCollider : MonoBehaviour
{
    private TotalGM gm;
    private BossCollder bossCollder;

    //�A�C�e���擾���ʉ�
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] audioClips;

    //STATE�^�̕ϐ�
    STATE state;
    //�_�ŊԊu
    [SerializeField] private float flashInterval;
    //�_�ł�����Ƃ��̃��[�v�J�E���g
    [SerializeField] private int loopCount;
    //�R���C�_�[���I���I�t���邽�߂�CircleCollider2D
    [SerializeField]
    private CircleCollider2D collider;
    //�����������ǂ����̃t���O
    private bool isHit;
    //���S�t���O
    private bool deathFlag = false;

    //���S����A�j���[�V������
    [SerializeField]
    private string deathAnimation;
    //�Q�[���I�u�W�F�N�g�ɐݒ肳��Ă���SkeletonAnimation
    [SerializeField]
    private SkeletonAnimation skeletonAnimation = default;
    //Spine�A�j���[�V������K�p���邽�߂ɕK�v��AnimationState
    private Spine.AnimationState spineAnimationState = default;

    // �摜�`��p�̃R���|�[�l���g
    [SerializeField]
    private MeshRenderer spineRenderer;
    [SerializeField]
    private SpriteRenderer colliderSprite;

    public bool DeathFlag
    {
        get { return this.deathFlag; }
        set { this.deathFlag = value; }
    }

    //�v���C���[�̏�ԗp�񋓌^�i�m�[�}���A�_���[�W�A���G��3��ށj
    enum STATE
    {
        NOMAL,
        DAMAGED,
        MUTEKI
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<TotalGM>();
        bossCollder = FindObjectOfType<BossCollder>();

        // SkeletonAnimation����AnimationState���擾
        spineAnimationState = skeletonAnimation.AnimationState;

        //�v���C���[��Hp������
        var scene = gm.MyGetScene();
        if(gm.BackScene == scene)
        {
            gm.PlayerHp[0] = gm.PlayerHp[1];
        }
        if(gm.BackScene != scene || gm.BackScene == TotalGM.StageCon.No)
        {
            //�̗�0�̂܂܁A���X�e�[�W�Ɉڍs������̗�1�񕜂���
            if(gm.PlayerHp[0] == 0)
            {
                gm.PlayerHp[0]++;
            }
            gm.PlayerHp[1] = gm.PlayerHp[0];
        }
        skeletonAnimation.timeScale = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        // �X�e�[�g���_���[�W�Ȃ烊�^�[��
        if (state == STATE.DAMAGED)
        {
            return;
        }
        //�v���C���[Hp��0�ȉ����t�F�[�h�C�����ĂȂ�������
        if (gm.PlayerHp[0] <= 0 && deathFlag == false && bossCollder.BossDeathFlag == false)
        {
            deathFlag = true;
            StartCoroutine(PlayerDeath());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�G�̒e�ɓ���������
        if (collision.gameObject.CompareTag("Bullet"))
            //collision.gameObject.CompareTag("EnemySkillBullet") ||
            //collision.gameObject.CompareTag("DestoryBullet"))
        {
            if(bossCollder.BossDeathFlag == false)
            {
                gm.PlayerHp[0]--;
                if (gm.PlayerHp[0] > 0)
                {
                    state = STATE.DAMAGED;
                    audioSource.PlayOneShot(audioClips[1]);
                    StartCoroutine(PlayerDameged());
                }
            }
        }
        //�X�R�A���Z�A�C�e���������
        if (collision.gameObject.CompareTag("ScoreAdditionItem"))
        {
            collision.gameObject.SetActive(false);
            var scene = gm.MyGetScene();
            audioSource.PlayOneShot(audioClips[0]);
            switch (scene)
            {
                case TotalGM.StageCon.First:
                    gm.NowScore[0] += 5000;
                    break;
                case TotalGM.StageCon.Secound:
                    gm.NowScore[1] += 5000;
                    break;
                case TotalGM.StageCon.Thead:
                    gm.NowScore[2] += 5000;
                    break;
            }
        }
    }
    //�v���C���[���_���[�W�󂯂���
    private IEnumerator PlayerDameged()
    {
        isHit = true;
        collider.enabled = false;

        spineRenderer.material.color = Color.black;
        colliderSprite.color = Color.black;
        
        //�_�Ń��[�v�J�n
        for (int i = 0; i < loopCount; i++)
        {
            if (isHit == false)
            {
                continue;
            }
            //flashInterval�҂��Ă���
            yield return new WaitForSeconds(flashInterval);
            //spriteRenderer���I�t
            spineRenderer.enabled = false;
            colliderSprite.enabled = false;

            //flashInterval�҂��Ă���
            yield return new WaitForSeconds(flashInterval);
            //spriteRenderer���I��
            spineRenderer.enabled = true;
            colliderSprite.enabled = true;
        }

        //�f�t�H���g��Ԃɂ���
        state = STATE.NOMAL;
        //�����蔻����I���ɂ���
        collider.enabled = true;
        //�F�𔒂ɂ���
        spineRenderer.material.color = Color.white;
        colliderSprite.color = Color.white;
        
        //�_�Ń��[�v���������瓖����t���O��false(�������ĂȂ����)
        isHit = false;
    }

    //�v���C���[�����񂾂Ƃ�
    private IEnumerator PlayerDeath()
    {
        if(bossCollder.BossDeathFlag == true)
        {
            StopCoroutine(PlayerDeath());
        }
        colliderSprite.enabled = false;
        gm.PlayerTransForm = transform.position;
        gm.BackScene = gm.MyGetScene();

        spineAnimationState.SetAnimation(0, deathAnimation, false);
        
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("GameOver");
        deathFlag = false;
        StopCoroutine(PlayerDeath());
    }
}

