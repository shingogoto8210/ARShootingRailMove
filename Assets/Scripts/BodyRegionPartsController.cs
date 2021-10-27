using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�l�~�[�̕��ʂ��Ƃ̃_���[�W���v�Z����N���X
/// </summary>
public class BodyRegionPartsController : MonoBehaviour
{
    [SerializeField, Header("���ʂ̐ݒ�")]
    private BodyRegionType bodyPartsType;     //�A�^�b�`�����Q�[���I�u�W�F�N�g�Ɏ��������������C���X�y�N�^�[���ݒ肷��
    
    /// <summary>
    /// ���ʂ̏��̎擾�p�B�v���p�e�B�ł���
    /// </summary>
    /// <returns></returns>
    public BodyRegionType GetBodyType()
    {
        return bodyPartsType;
    }

    public (int,BodyRegionType) CalcDamageParts(int damage)
    {
        //���ʂ̏��𗘗p���ă_���[�W���v�Z����
        int lastDamage = bodyPartsType switch
        {

            //�����̏ꍇ�̓_���[�W5�{
            BodyRegionType.Head => damage * 5,

            //TODO ���̕��ʂ�ǉ�

            //��L�ȊO
            _ => damage,
        };

        //�������ʂ��^�v���^�Ŗ߂�
        return (lastDamage, bodyPartsType);
    }
}
