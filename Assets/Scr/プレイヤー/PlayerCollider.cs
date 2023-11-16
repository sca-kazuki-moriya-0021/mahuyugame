using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollider : MonoBehaviour
{
    private TotalGM gm;

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

    // �摜�`��p�̃R���|�[�l���g
    [SerializeField]
    private SpriteRenderer characterSprite;
    private SpriteRenderer colliderSprite;

    #region//���G�֌W
    //private bool invincibleFlag = false;
    private static float invincibleCount = 5.0f;
    //private static float invincibleCounttime = 0;
    #endregion


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
        collider2D = GetComponent<CircleCollider2D>();
        colliderSprite = GetComponent<SpriteRenderer>();

        var scene = gm.MyGetScene();
        if(gm.BackScene == scene)
        {
            gm.PlayerLevel[0] = gm.PlayerLevel[1];
        }
        if(gm.BackScene != scene)
        {
            gm.PlayerHp[1] = gm.PlayerHp[0];
            gm.PlayerLevel[1] = gm.PlayerLevel[0];
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

        if (gm.PlayerHp[0] == 0)
        {
            gm.PlayerTransForm = this.transform.position;
            gm.BackScene = gm.MyGetScene();
            gm.PlayerLevel[1] = gm.PlayerLevel[0];
            SceneManager.LoadScene("GameOver");
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
            state = STATE.DAMAGED;
            StartCoroutine(PlayerDameged());
        }

        if (collision.gameObject.CompareTag("BaffItem"))
        {
            collision.gameObject.SetActive(false);
            gm.PlayerLevel[0]++;
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

        characterSprite.color = Color.black;
        colliderSprite .color = Color.black;

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
            characterSprite.enabled = false;
            colliderSprite.enabled = false;

            //flashInterval�҂��Ă���
            yield return new WaitForSeconds(flashInterval);
            //spriteRenderer���I��
            characterSprite.enabled = true;
            colliderSprite.enabled = true;

            //���[�v��5��܂������
            /* (i > 5)
            {
                //state��MUTEKI�ɂ���i�_�ł��Ȃ��瓮����悤�ɂȂ�j
                state = STATE.MUTEKI;
                //�F��΂ɂ���
                characterSprite.color = Color.green;
            }*/
        }

        //�f�t�H���g��Ԃɂ���
        state = STATE.NOMAL;
        //�����蔻����I���ɂ���
        collider2D.enabled = true;
        //�F�𔒂ɂ���
        characterSprite.color = Color.white;
        colliderSprite.color = Color.white;
        

        //�_�Ń��[�v���������瓖����t���O��false(�������ĂȂ����)
        isHit = false;
    }


}

