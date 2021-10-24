using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���򂲂Ƃ̃p�X�f�[�^�̃f�[�^�x�[�X
/// </summary>
[CreateAssetMenu(fileName = "StagePathDataSO",menuName = "Create StagePathDataSO")]
public class StagePathDataSO : ScriptableObject
{
    [System.Serializable]
    public class StagePathData
    {
        [System.Serializable]
        public class BranchData
        {
            public BranchDirectionType branchDirectionType;
            public RailPathData railPathData;

            //���ɂ�����Βǉ�����
        }

        [Header("���򂲂Ƃ̃p�X�f�[�^�Q")]
        public List<BranchData> branchDatasList;
    }

    [Header("�X�e�[�W���Ƃ̃p�X�f�[�^�Q")]
    public List<StagePathData> stagePathDatasList = new List<StagePathData>();
}
