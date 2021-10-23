using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���[���[���̊Ǘ��N���X
/// </summary>
public class PlayerController : MonoBehaviour
{
    private int hp; �@�@�@�@�@//���݂�Hp�l
    private int bulletCount;�@//���݂̒e���l

    [SerializeField,Header("�ő�Hp�l")]
    private int maxHp;

    [SerializeField, Header("�ő�e���l")]
    private int maxBullet;

    [SerializeField, Header("�����[�h����")]
    private float reloadTime;

    [Header("�e�̍U����")]
    public int bulletPower;

    [Header("�e�̘A�ˑ��x")]
    public float shootInterval;

    [Header("�e�̎˒�����")]
    public float shootRange;

    [Header("�����[�h�@�\�̃I��/�I�t")]
    public bool isReloadModeOn;

    [Header("�����[�h��Ԃ̐���")]
    public bool isReloading;

    /// <summary>
    /// �e���p�̃v���p�e�B
    /// </summary>
    public int BulletCount
    {
        set => bulletCount = Mathf.Clamp(value, 0, maxBullet);
        get => bulletCount;
    }
    void Start()
    {
        //Debug�p
        SetUpPlayer();
    }

    /// <summary>
    /// �v���C���[���̏����ݒ�
    /// </summary>
    public void SetUpPlayer()
    {
        //maxHp�̐ݒ肪���邩�m�F�B�Ȃ���Ώ����l10�ŃZ�b�g����hp��ݒ�
        hp = maxHp = maxHp == 0 ? 10 : maxBullet;

        //maxBullet�̐ݒ肪���邩�m�F�B�Ȃ���Ώ����l10�ŃZ�b�g���Ēe����ݒ�
        BulletCount = maxBullet = maxBullet == 0 ? 10 : maxBullet;

        //�C���X�y�N�^�[��̑��̐ݒ�������l������������������͖Y�ꂪ�Ȃ����S
        //TODO�@���̂ق��̏����ݒ肪����΂����ōs��
    }

    //Hp�̌v�Z�ƍX�V
    public void CalcHp(int amount)
    {
        hp = Mathf.Clamp(hp += amount, 0, maxHp);
        Debug.Log("���݂�Hp :" + hp); //UI��hp���m�F�ł���悤�ɂȂ�����R�����g�A�E�g

        //TODO Hp�\���̍X�V

        if(hp < 0)
        {
            Debug.Log("Game Over");
        }
    }

    /// <summary>
    /// �e���̌v�Z�ƍX�V
    /// </summary>
    /// <param name="amount"></param>
    public void CalcBulletCount(int amount)
    {
        BulletCount += amount;
        Debug.Log("���݂̒e�� :" + BulletCount);�@//UI�Œe�����m�F�ł���悤�ɂȂ�����R�����g�A�E�g

        //TODO�@�e����UI�\���X�V
    }

    /// <summary>
    /// �e���̃����[�h
    /// </summary>
    /// <returns></returns>
    public IEnumerator ReloadBullet()
    {
        //�����[�h��Ԃɂ��āC�e�̔��˂𐧌䂷��
        isReloading = true;

        //�����[�h
        BulletCount = maxBullet;

        Debug.Log("�����[�h");  //UI�Ŋm�F�ł���悤�ɂȂ�����R�����g�A�E�g

        //TODO �e����UI�\���X�V

        //TODO SE

        //�����[�h�̑ҋ@����
        yield return new WaitForSeconds(reloadTime);

        //�ēx�C�e�����˂ł����Ԃɂ���
        isReloading = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //TODO �G����̍U���ɂ���Ĕ�e�����ꍇ�̏���

        //TODO �{�X��G�̍U���͈͂����m���Ȃ��悤�ɂ��邽�߂Ƀ^�O�Ŕ��肷�邩�C���C���[��ݒ肵�ĉ������
    }

    void Update()
    {
        
    }
}
