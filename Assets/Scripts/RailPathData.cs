using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ���[���ړ��p�̃p�X�f�[�^�Ǘ��N���X
/// </summary>
public class RailPathData : MonoBehaviour
{
    [System.Serializable]
    public class PathDataDetail
    {
        [Tooltip("�ړ�����")]
        public float railMoveDuration;

        [Tooltip("�ړ��n�_�ƃJ�����̊p�x")]
        public Transform pathTran;

        [Tooltip("�~�b�V�����̔����L���B�I���Ŕ���")]
        public bool isMissionTrigger;
    }

    [Header("�o�H�p�̃p�X�f�[�^�Q")]
    public PathDataDetail[] pathDataDetails;

    /// <summary>
    /// �p�X�̈ړ����Ԃ̎擾
    /// </summary>
    /// <returns></returns>
    public float[] GetRailMoveDurations()
    {
        return pathDataDetails.Select(x => x.railMoveDuration).ToArray();
    }

    /// <summary>
    /// �p�X�̈ʒu�Ɖ�]���̎擾
    /// </summary>
    /// <returns></returns>
    public Transform[] GetPathTrans()
    {
        return pathDataDetails.Select(x => x.pathTran).ToArray();
    }

    public bool[] GetIsMissionTriggers()
    {
        return pathDataDetails.Select(x => x.isMissionTrigger).ToArray();
    }
}
