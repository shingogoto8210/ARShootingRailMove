using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�l�~�[�̊�N���X
/// </summary>
public class EnemyBase : EventBase
{
    [SerializeField]   //Debug���C�G�l�~�[���C���X�^���X���Ȃ��ꍇ�ɂ́C�C���X�y�N�^�[����Ώۂ�o�^���Ă���
    protected GameObject lookTarget;

    [SerializeField]
    protected int enemyNo;

    [SerializeField]
    protected int hp;

    [SerializeField]
    protected int attackPower;

    [SerializeField]
    protected float moveSpeed;

    protected Animator anim;

    protected bool isAttack;

    protected float attackInterval = 3.0f;

    protected PlayerController player;

    protected GameManager gameManager;

    protected IEnumerator attackCoroutine;

    protected int point = 100;

    protected bool isDead;

    public EnemyMoveType enemyMoveType;

    //���ʂ�List����������
    [SerializeField, Header("���ʂ̏���o�^���郊�X�g")]
    protected List<BodyRegionPartsController> partsControllersList = new List<BodyRegionPartsController>();

    //TODO �G�̃f�[�^�̃N���X����������

    protected virtual void Start()
    {
        //�f�o�b�O�p
        SetUpEnemy(lookTarget);
    }
    
    /// <summary>
    /// �G�l�~�[�̐ݒ�B�O���N���X����Ăяo���݌v
    /// </summary>
    /// <param name="playerObj"></param>
    /// <param name="gameManager"></param>
    public virtual void SetUpEnemy(GameObject playerObj,GameManager gameManager = null)
    {
        lookTarget = playerObj;
        this.gameManager = gameManager;

        //�G�̃f�[�^��G�̔ԍ����猟�����ăZ�b�g
        GetEnemyData();

        TryGetComponent(out anim);
    }

    /// <summary>
    /// �G�̏����f�[�^�x�[�X���擾���Đݒ�
    /// </summary>
    protected virtual void GetEnemyData()
    {
        //TODO �f�[�^�x�[�X����f�[�^���擾���ăZ�b�g
    }

    
    protected virtual void Update()
    {
        //�G�l�~�[��Ώہi�J�����j�̕����Ɍ�����
        if (lookTarget)
        {
            Vector3 direction = lookTarget.transform.position - transform.position;
            direction.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (isAttack)
        {
            return;
        }

        //���[�J���֐����`
        void SetAttackCoroutine()
        {
            //IEnumerator�^�̕ϐ��ɍU���p�̃��\�b�h��o�^
            attackCoroutine = Attack(player);

            //�o�^�������\�b�h�����s
            StartCoroutine(attackCoroutine);

            //Debug.Log("�U���J�n");
        }

        //�v���C���[�̏���ێ����Ă���C�U�����łȂ��Ȃ�
        if(player != null)
        {
            SetAttackCoroutine();

            //Debug.Log("�v���C���[�@���m��");

            //�v���C���[�̏�񂪂Ȃ��Ȃ�
        }
        else
        {
            //�v���C���[GameObject���쐬���C������PlayerController���A�^�b�`�������ߕύX�B���X�̓J�����ɂ��Ă����B
            //if(other.transform.parent.TryGetComponent(out player))
            if(other.transform.TryGetComponent(out player))
            {

                //�U���p�̃��\�b�h��o�^
                SetAttackCoroutine();

                //Debug.Log("�U���͈͓��Ƀv���C���[�@�����m");
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {

        //�v���C���[�����m�ς݂̎��ɁC�U���͈͓��Ƀv���C���[�����Ȃ��Ȃ�����
        if(player != null)
        {

            //������
            player = null;

            //�U���������~�߂�
            isAttack = false;
            StopCoroutine(attackCoroutine);

            //Debug.Log("�U���͈͊O");
        }
    }

    /// <summary>
    /// �U��
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    protected IEnumerator Attack(PlayerController player = null)
    {
        isAttack = true;


        if (anim)
        {
            anim.SetTrigger("Attack");
        }
        //Trigger��true�ɂȂ��Ă�����ۓ����o���܂ł̎��ԍ�
        yield return new WaitForSeconds(2.0f);
        player.CalcHp(-attackPower);

        yield return new WaitForSeconds(attackInterval);
        isAttack = false;
    }

    /// <summary>
    /// �U���͎擾�p
    /// </summary>
    /// <returns></returns>
    public int GetAttackPower()
    {
        return attackPower;
    }

    /// <summary>
    /// ���ۃN���X�̃��\�b�h������
    /// </summary>
    /// <param name="value"></param>
    /// <param name="hitBodyRegionType"></param>
    public override void TriggerEvent(int value, BodyRegionType hitBodyRegionType)
    {
        //�_���[�W�v�Z
        CalcDamage(value, hitBodyRegionType);
    }

    /// <summary>
    /// �_���[�W�v�Z
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="hitParts"></param>
    public virtual void CalcDamage(int damage, BodyRegionType hitParts)
    {
        if (isDead)
        {
            return;
        }

        hp -= damage;

        if (anim) anim.ResetTrigger("Attack");

        if(hp <= 0)
        {
            isDead = true;

            if (anim)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("Down", true);
            }

            //TODO �G�l�~�[�̏����O���N���X��List�ŊǗ����Ă���ꍇ�ɂ́CList����폜

            //���ʂɂ�锻�肪����C���C���������ē|�����ꍇ
            if(hitParts == BodyRegionType.Head)
            {
                BodyRegionPartsController parts = partsControllersList.Find(x => x.GetBodyType() == hitParts);
                parts.gameObject.SetActive(false);

                //�X�R�A�Ƀ{�[�i�X
                point *= 3;
            }

            //TODO �X�R�A���Z

            Destroy(gameObject, 1.5f);
        }
        else
        {
            if (anim) anim.SetTrigger("Damage");
        }
    }

    /// <summary>
    /// �ړ����ꎞ��~
    /// </summary>
    public virtual void PauseMove()
    {

    }

    /// <summary>
    /// �ړ����ĊJ
    /// </summary>
    public virtual void ResumeMove()
    {

    }


}
