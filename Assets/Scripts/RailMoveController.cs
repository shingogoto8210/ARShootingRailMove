using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RailMoveController : MonoBehaviour
{
    [SerializeField]
    private Transform railMoveTarget; //���[���ړ�������Ώ�

    [SerializeField]
    private RailPathData currentRailPathData; //�A�^�b�`����Ă���RailPathData�Q�[���I�u�W�F�N�g���A�T�C���B���ƂŎ����A�T�C���ɕύX

    private Tween tween;

    private GameManager gameManager;

    private int moveCount;

    /// <summary>
    /// RailMoveController�̏����ݒ�
    /// </summary>
    /// <param name="gameManager"></param>
    public void SetUpRailMoveController(GameManager gameManager)
    {
        this.gameManager = gameManager;
        //TODO ���ɂ�����ꍇ�ɂ͒ǋL�B�K�v�ɉ����Ĉ�����ʂ��ĊO������������炤�悤�ɂ���
    }

    public void SetNextRailPathData(RailPathData nextRailPathData)
    {
        //�ړI�n�擾
        currentRailPathData = nextRailPathData;
        //�ړ��J�n
        StartCoroutine(StartRailMove());
    }



    //private void Start()
    //{
      //  StartCoroutine(StartRailMove());
    //}

    /// <summary>
    /// ���[���ړ��̊J�n
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartRailMove()
    {
        yield return null;

        //�ړ�����n�_���擾���邽�߂̔z��̏�����
        //Vector3[] paths = new Vector3[currentRailPathData.GetPathTrans().Length];
        //float totalTime = 0;
        //�ړ�����ʒu���Ǝ��Ԃ����Ԃɔz��Ɏ擾
        //for(int i = 0; i < currentRailPathData.GetPathTrans().Length; i++)
        //{
        //  paths[i] = currentRailPathData.GetPathTrans()[i].position;
        //  totalTime += currentRailPathData.GetRailMoveDurations()[i];
        //}
        
        //�ړ���̃p�X��񂩂�position�̏�񂾂����o���Ĕz����쐬
        Vector3[] paths = currentRailPathData.GetPathTrans().Select(x => x.position).ToArray();
        float totalTime = currentRailPathData.GetRailMoveDurations().Sum();

        Debug.Log(totalTime);

        tween = railMoveTarget.DOPath(paths, totalTime).SetEase(Ease.Linear).OnWaypointChange((waypointIndex) => CheckArrivalDestination(waypointIndex));

        //�ړ����ꎞ��~
        PauseMove();

        //�Q�[���̐i�s��Ԃ��ړ����ɂȂ�܂őҋ@
        yield return new WaitUntil(() => gameManager.currentGameState == GameState.Play_Move);

        //�ړ��J�n
        ResumeMove();

        Debug.Log("�ړ��J�n");
    }

    /// <summary>
    /// ���[���ړ��̈ꎞ��~
    /// </summary>
    public void PauseMove()
    {
        tween.Pause();
    }

    /// <summary>
    /// ���[���ړ��̍ĊJ
    /// </summary>
    public void ResumeMove()
    {
        tween.Play();
    }

    /// <summary>
    /// �p�X�̖ڕW�n�_�ɓ������邽�тɎ��s�����
    /// </summary>
    /// <param name="waypointIndex"></param>
    private void CheckArrivalDestination(int waypointIndex)
    {
        Debug.Log("�ڕW�n�_�@�����F" + waypointIndex + "�Ԗ�");

        //�ړ��̈ꎞ��~
        PauseMove();

        //�ړ���̃p�X���܂��c���Ă��邩�m�F
        if(waypointIndex < currentRailPathData.GetPathTrans().Length)
        {
            //�~�b�V�������������邩�Q�[���}�l�[�W���[���Ŋm�F
            gameManager.CheckMissionTrigger(waypointIndex);
            //ResumeMove();
        }
        else
        {
            tween.Kill();

            //�o�H�����k�ɂ���
            tween = null;

            //�ړ��悪�c���Ă��Ȃ��ꍇ�ɂ́C�Q�[���}�l�[�W���[���ŕ���̊m�F(���̃��[�g�I��C�ړ���̕���C�{�X�C�N���A�̂����ꂩ�j
            moveCount++;

            gameManager.PreparateCheckNextBranch(moveCount);

            Debug.Log("����̊m�F");
        }
    }

    /// <summary>
    /// �ړ��p�̏������o�^���ꂽ���m�F
    /// </summary>
    /// <returns></returns>
    public bool GetMoveSetting()
    {
        return tween != null ? true : false;
    }
}
