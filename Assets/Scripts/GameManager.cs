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

    [Header("���݂̃Q�[���̐i�s��")]
    public GameState currentGameState;

    private void Start()
    {
        //�Q�[���̏�Ԃ��������ɂ���
        currentGameState = GameState.Wait;

        //���[�g�p�̌o�H����ݒ�
        originRailPathData = DataBaseManager.instance.GetRailPathDatasFromBranchNo(0, BranchDirectionType.NoBranch);

        //RailMoveController�̏����ݒ�
        railMoveController.SetUpRailMoveController(this);

        StartCoroutine(SetUpNextRailPathData());
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

    /// <summary>
    /// ���[�g�̕���m�F�̏���
    /// </summary>
    /// <param name="nextbranchNo"></param>
    public void PreparateCheckNextBranch(int nextbranchNo)
    {
        StartCoroutine(CheckNextBranch(nextbranchNo));
    }

    /// <summary>
    /// ���[�g�̕��򔻒�
    /// </summary>
    /// <param name="nextStagePathDataNo"></param>
    /// <returns></returns>
    private IEnumerator CheckNextBranch(int nextStagePathDataNo)
    {
        //���̃��[�g���Ȃ��ꍇ
        if (nextStagePathDataNo >= DataBaseManager.instance.StagePathDataCount)
        {
            //�I��
            Debug.Log("�Q�[���I��");

            yield break;
        }

        //���[�g�ɕ��򂪂��邩�ǂ����̔���
        if(DataBaseManager.instance.GetBranchDatasListCount(nextStagePathDataNo) == 1)
        {
            Debug.Log("����Ȃ��Ŏ��̃��[�g��");

            //����Ȃ��̏ꍇ�C���̌o�H��o�^
            originRailPathData = DataBaseManager.instance.GetRailPathDatasFromBranchNo(nextStagePathDataNo, BranchDirectionType.NoBranch);
        }
        else
        {
            //TODO ���򂪂���ꍇ�CUI�ɕ����\�����C�I����҂�

            Debug.Log("���[�g�̕��򔭐�");

            //TODO �����I������܂őҋ@

            //TODO �I���������[�g�����̃��[�g�ɐݒ�
            
        }

        StartCoroutine(SetUpNextRailPathData());
    }

    /// <summary>
    /// ���̃��[�g�̏����ݒ�
    /// </summary>
    private IEnumerator SetUpNextRailPathData()
    {
        //���[�g���̃~�b�V��������ݒ�
        SetMissionTriggers();

        //�o�H���ړ���ɐݒ�
        railMoveController.SetNextRailPathData(originRailPathData);

        //���[���ړ��̌o�H�ƈړ��o�^����������܂őҋ@
        yield return new WaitUntil(() => railMoveController.GetMoveSetting());

        //�Q�[���̐i�s��Ԃ��ړ����ɕύX����
        currentGameState = GameState.Play_Move;
    }
}
