using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class EnemyController_Agent : EnemyBase
{
    protected NavMeshAgent agent;  //NavMeshAgent�R���|�[�l���g�̑���p�B�q�N���X�ɂ͕ϐ��̐錾�����R�ɂł���B���̏���protected�Ȃ̂Őe�N���X�ł����p�\

    private float originMoveSpeed; //�����̈ړ����x�̕ێ��p�B���̏���private�C���q�Ȃ̂ŁC���̃N���X�ł����g���Ȃ��B

    /// <summary>
    /// �G�l�~�[�̐ݒ�
    /// </summary>
    public override void SetUpEnemy(GameObject playerObj,GameManager gameManager = null)
    {
        base.SetUpEnemy(playerObj, gameManager);

        //NavMesh�𗘗p���Ă��邩����
        if(TryGetComponent(out agent))
        {
            originMoveSpeed = moveSpeed;

            //���p���Ă���ꍇ�ɂ͖ڕW�n�_���Z�b�g
            agent.destination = lookTarget.transform.position;

            //�ړ����x��NavMesh�ɐݒ�
            agent.speed = moveSpeed;

            //�A�j��������ꍇ�ɂ͍Đ�
            if (anim)
            {
                anim.SetBool("Walk", true);
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        //�ڕW�n���X�V
        if(lookTarget != null && agent != null)
        {
            agent.destination = lookTarget.transform.position;
        }
    }

    /// <summary>
    /// �ړ����ꎞ��~
    /// </summary>
    public override void PauseMove()
    {
        agent.speed = 0;
    }

    /// <summary>
    /// �ړ��ĊJ
    /// </summary>
    public override void ResumeMove()
    {
        agent.speed = originMoveSpeed;
    }

    /// <summary>
    /// �ڕW�n������
    /// </summary>
    public void ClearPath()
    {
        //�q�N���X�����̐V�������\�b�h�B���̃��\�b�h��public �C���q�Ȃ̂ŁC�e�N���X�ł��O���̃N���X�ł����p�ł���
        agent.ResetPath();
    }
}
