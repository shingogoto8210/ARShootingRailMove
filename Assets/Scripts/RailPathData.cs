using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���[���ړ��p�̃p�X�f�[�^�Ǘ��N���X
/// </summary>
public class RailPathData : MonoBehaviour
{
    [SerializeField,Tooltip("�ړ�����")]
    private float[] railMoveDurations;

    [SerializeField, Tooltip("�ړ��n�_�ƃJ�����̊p�x")]
    private Transform[] pathTrans;

    /// <summary>
    /// �p�X�̈ړ����Ԃ̎擾
    /// </summary>
    /// <returns></returns>
    public float[] GetRailMoveDurations()
    {
        return railMoveDurations;
    }

    /// <summary>
    /// �p�X�̈ʒu�Ɖ�]���̎擾
    /// </summary>
    /// <returns></returns>
    public Transform[] GetPathTrans()
    {
        return pathTrans;
    }
}
