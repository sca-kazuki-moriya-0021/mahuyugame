using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine.Unity;
using Spine;

public class PlayerCollider : MonoBehaviour
{
    private TotalGM gm;
    private NowLoading nowLoading;

    //STATE�^�̕ϐ�
    STATE state;
    //�_�ŊԊu
    [SerializeField] private float flashInterval;
    //�_�ł�����Ƃ��̃��[�v�J�E���g
    [SerializeField] private int loopCount;
    //�R���C�_�[���I���I�t���邽�߂�CircleCollider2D
    private CircleCollider2D collider2D;
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
    private MeshRenderer SpineRenderer;
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
        nowLoading = FindObjectOfType<NowLoading>();
        collider2D = GetComponent<CircleCollider2D>();
        colliderSprite = GetComponent<SpriteRenderer>();

        // SkeletonAnimation����AnimationState���擾
        spineAnimationState = skeletonAnimation.AnimationState;

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
    }

    // Update is called once per frame
    void Update()
    {
        // �X�e�[�g���_���[�W�Ȃ烊�^�[��
        if (state == STATE.DAMAGED)
        {
            return;
        }

        if (gm.PlayerHp[0] <= 0 && deathFlag == false && nowLoading.FadeInFlag == false)
        {
            deathFlag = true;
            StartCoroutine(PlayerDeath());
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            //collision.gameObject.CompareTag("EnemySkillBullet") ||
            //collision.gameObject.CompareTag("DestoryBullet"))
        {
            Destroy(collision.gameObject);
            
            gm.PlayerHp[0]--;
            if(gm.PlayerHp[0] > 0)
            {
                state = STATE.DAMAGED;
                StartCoroutine(PlayerDameged());
            }
        }

        if (collision.gameObject.CompareTag("ScoreAdditionItem"))
        {
            collision.gameObject.SetActive(false);
            var scene = gm.MyGetScene();
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

        if (collision.gameObject.CompareTag("WeaponSwitchItem0"))
        {
            collision.gameObject.SetActive(false);
            for (int i = 0; i < gm.PlayerWeapon.Length; i++)
            {
                gm.PlayerWeapon[i] = false;
            }

            gm.PlayerWeapon[0] = true;
        }

        if (collision.gameObject.CompareTag("WeaponSwitchItem1"))
        {
            collision.gameObject.SetActive(false);
            for (int i = 0; i < gm.PlayerWeapon.Length; i++)
            {
                gm.PlayerWeapon[i] = false;
            }

            gm.PlayerWeapon[1] = true;
        }

        if (collision.gameObject.CompareTag("WeaponSwitchItem2"))
        {
            collision.gameObject.SetActive(false);
            for (int i = 0; i < gm.PlayerWeapon.Length; i++)
            {
                gm.PlayerWeapon[i] = false;
            }

            gm.PlayerWeapon[2] = true;
        }

        if (collision.gameObject.CompareTag("WeaponSwitchItem3"))
        {
            collision.gameObject.SetActive(false);
            for (int i = 0; i < gm.PlayerWeapon.Length; i++)
            {
                gm.PlayerWeapon[i] = false;
            }

            gm.PlayerWeapon[3] = true;
        }
    }

    private IEnumerator PlayerDameged()
    {
        isHit = true;
        collider2D.enabled = false;

        SpineRenderer.material.color = Color.black;
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
            SpineRenderer.enabled = false;
            colliderSprite.enabled = false;

            //flashInterval�҂��Ă���
            yield return new WaitForSeconds(flashInterval);
            //spriteRenderer���I��
            SpineRenderer.enabled = true;
            colliderSprite.enabled = true;
        }

        //�f�t�H���g��Ԃɂ���
        state = STATE.NOMAL;
        //�����蔻����I���ɂ���
        collider2D.enabled = true;
        //�F�𔒂ɂ���
        SpineRenderer.material.color = Color.white;
        colliderSprite.color = Color.white;
        
        //�_�Ń��[�v���������瓖����t���O��false(�������ĂȂ����)
        isHit = false;
    }

    private IEnumerator PlayerDeath()
    {
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

