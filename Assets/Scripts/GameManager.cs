using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private RailMoveController railMoveController;
    [SerializeField,Header("�o�H�p�̃p�X�Q�̌��f�[�^")]
    private RailPathData originRailPathData;
    [SerializeField,Header("�p�X�ɂ�����~�b�V�����̔����̗L��")]//Debug�p
    private bool[] isMissionTriggers;

    void Start()
    {
        //TODO �Q�[���̏�Ԃ��������ɂ���
        //TODO ���[�g�p�̌o�H����ݒ�

        //RailMoveController�̏����ݒ�
        railMoveController.SetUpRailMoveController(this);

        //�p�X�f�[�^���~�b�V�����̔����L�����擾
        SetMissionTriggers();

        //���ɍĐ����郌�[���ړ��̖ړI�n�ƌo�H�̃p�X��ݒ�
        railMoveController.SetNextRailPathData(originRailPathData);

        //TODO �o�H�̏�������������̂�҂iStart���\�b�h�̖߂�l��IEnumerator�ɕύX���ăR���[�`�����\�b�h�ɕς���j
        //TODO �Q�[���̏�Ԃ��v���C���ɕς���
    }

    /// <summary>
    /// �p�X�f�[�^���~�b�V�����̔����L�����擾
    /// </summary>
    private void SetMissionTriggers()
    {
        //�z��̏�����
        isMissionTriggers = new bool[originRailPathData.GetIsMissionTriggers().Length];
        isMissionTriggers = originRailPathData.GetIsMissionTriggers();
    }

    /// <summary>
    /// �~�b�V���������L���̔���
    /// </summary>
    /// <param name="index"></param>
    public void CheckMissionTrigger(int index)
    {
        if (isMissionTriggers[index])
        {
            //TODO �~�b�V��������
            Debug.Log(index + "�Ԗڂ̃~�b�V��������");

            //Debug�p�@���͂��̂܂ܐi�s
            railMoveController.ResumeMove();
        }
        else
        {
            //�~�b�V�����Ȃ��B���̃p�X�ֈړ����ĊJ
            railMoveController.ResumeMove();
        }
    }
}
