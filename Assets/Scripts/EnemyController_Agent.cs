using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class EnemyController_Agent : EnemyBase
{
    [SerializeField]
    protected NavMeshAgent agent;  //NavMeshAgentコンポーネントの代入用。子クラスには変数の宣言も自由にできる。この情報はprotectedなので親クラスでも利用可能

    private float originMoveSpeed; //初期の移動速度の保持用。この情報はprivate修飾子なので，このクラスでしか使えない。

    /// <summary>
    /// エネミーの設定
    /// </summary>
    public override void SetUpEnemy(GameObject playerObj,GameManager gameManager = null)
    {
        base.SetUpEnemy(playerObj, gameManager);

        //NavMeshを利用しているか判定
        if(TryGetComponent(out agent))
        {
            originMoveSpeed = moveSpeed;

            //利用している場合には目標地点をセット
            agent.destination = lookTarget.transform.position;
            Debug.Log("目的地点を：" + lookTarget + "にセット");

            //移動速度をNavMeshに設定
            agent.speed = moveSpeed;

            //アニメがある場合には再生
            if (anim)
            {
                anim.SetBool("Walk", true);
                Debug.Log("歩き始める");

            }
        }
    }

    protected override void Update()
    {
        base.Update();

        //目標地を更新
        if(lookTarget != null && agent != null)
        {
            agent.destination = lookTarget.transform.position;
            //Debug.Log("目的地点を更新");

        }
    }

    /// <summary>
    /// 移動を一時停止
    /// </summary>
    public override void PauseMove()
    {
        agent.speed = 0;
    }

    /// <summary>
    /// 移動再開
    /// </summary>
    public override void ResumeMove()
    {
        agent.speed = originMoveSpeed;
    }

    /// <summary>
    /// 目標地を消去
    /// </summary>
    public void ClearPath()
    {
        //子クラスだけの新しいメソッド。このメソッドはpublic 修飾子なので，親クラスでも外部のクラスでも利用できる
        agent.ResetPath();
    }
}
