using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �e��f�[�^�x�[�X�p�̃X�N���v�^�u���I�u�W�F�N�g�̊Ǘ��N���X
/// </summary>
public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    [SerializeField]
    private StagePathDataSO stagePathDataSO;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// �X�e�[�W�p�X�ԍ����番����RailPathData�����擾
    /// </summary>
    /// <param name="nextStagePathDataNo"></param>
    /// <param name="searchBranchDirectionType"></param>
    /// <returns></returns>
    public RailPathData GetRailPathDatasFromBranchNo(int nextStagePathDataNo,BranchDirectionType searchBranchDirectionType)
    {
        return stagePathDataSO.stagePathDatasList[nextStagePathDataNo].branchDatasList.Find(x => x.branchDirectionType == searchBranchDirectionType).railPathData;
    }

    /// <summary>
    /// �Q�b�^�[���\�b�h���g���ăX�e�[�W���̃��[�g�̐��̎擾
    /// </summary>
    /// <returns></returns>
    //public int GetStagePathDatasListCount()
    //{
      //  return stagePathDataSO.stagePathDatasList.Count;
    //}

    /// <summary>
    /// �v���p�e�B��Get���g���ăX�e�[�W���̃��[�g�̐��̎擾
    /// </summary>
    public int StagePathDataCount
    {
        get => stagePathDataSO.stagePathDatasList.Count;
    }

    /// <summary>
    /// �u�����`�̊Ǘ����Ă��镪�򐔂̎擾
    /// </summary>
    /// <param name="branchNo"></param>
    /// <returns></returns>
    public int GetBranchDatasListCount(int branchNo)
    {
        return stagePathDataSO.stagePathDatasList[branchNo].branchDatasList.Count;
    }
}
