using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RailMoveController : MonoBehaviour
{
    [SerializeField]
    private Transform railMoveTarget; //レール移動させる対象

    [SerializeField]
    private RailPathData currentRailPathData; //アタッチされているRailPathDataゲームオブジェクトをアサイン。あとで自動アサインに変更

    private Tween tween;

    private GameManager gameManager;

    private int moveCount;

    /// <summary>
    /// RailMoveControllerの初期設定
    /// </summary>
    /// <param name="gameManager"></param>
    public void SetUpRailMoveController(GameManager gameManager)
    {
        this.gameManager = gameManager;
        //TODO 他にもある場合には追記。必要に応じて引数を通じて外部から情報をもらうようにする
    }

    public void SetNextRailPathData(RailPathData nextRailPathData)
    {
        //目的地取得
        currentRailPathData = nextRailPathData;
        //移動開始
        StartCoroutine(StartRailMove());
    }



    //private void Start()
    //{
      //  StartCoroutine(StartRailMove());
    //}

    /// <summary>
    /// レール移動の開始
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartRailMove()
    {
        yield return null;

        //移動する地点を取得するための配列の初期化
        //Vector3[] paths = new Vector3[currentRailPathData.GetPathTrans().Length];
        //float totalTime = 0;
        //移動する位置情報と時間を順番に配列に取得
        //for(int i = 0; i < currentRailPathData.GetPathTrans().Length; i++)
        //{
        //  paths[i] = currentRailPathData.GetPathTrans()[i].position;
        //  totalTime += currentRailPathData.GetRailMoveDurations()[i];
        //}
        
        //移動先のパス情報からpositionの情報だけ抽出して配列を作成
        Vector3[] paths = currentRailPathData.GetPathTrans().Select(x => x.position).ToArray();
        float totalTime = currentRailPathData.GetRailMoveDurations().Sum();

        Debug.Log(totalTime);

        tween = railMoveTarget.DOPath(paths, totalTime).SetEase(Ease.Linear).OnWaypointChange((waypointIndex) => CheckArrivalDestination(waypointIndex));

        //移動を一時停止
        PauseMove();

        //ゲームの進行状態が移動中になるまで待機
        yield return new WaitUntil(() => gameManager.currentGameState == GameState.Play_Move);

        //移動開始
        ResumeMove();

        Debug.Log("移動開始");
    }

    /// <summary>
    /// レール移動の一時停止
    /// </summary>
    public void PauseMove()
    {
        tween.Pause();
    }

    /// <summary>
    /// レール移動の再開
    /// </summary>
    public void ResumeMove()
    {
        tween.Play();
    }

    /// <summary>
    /// パスの目標地点に到着するたびに実行される
    /// </summary>
    /// <param name="waypointIndex"></param>
    private void CheckArrivalDestination(int waypointIndex)
    {
        Debug.Log("目標地点　到着：" + waypointIndex + "番目");

        //移動の一時停止
        PauseMove();

        //移動先のパスがまだ残っているか確認
        if(waypointIndex < currentRailPathData.GetPathTrans().Length)
        {
            //ミッションが発生するかゲームマネージャー側で確認
            gameManager.CheckMissionTrigger(waypointIndex);
            //ResumeMove();
        }
        else
        {
            tween.Kill();

            //経路情報を殻にする
            tween = null;

            //移動先が残っていない場合には，ゲームマネージャー側で分岐の確認(次のルート選定，移動先の分岐，ボス，クリアのいずれか）
            moveCount++;

            gameManager.PreparateCheckNextBranch(moveCount);

            Debug.Log("分岐の確認");
        }
    }

    /// <summary>
    /// 移動用の処理が登録されたか確認
    /// </summary>
    /// <returns></returns>
    public bool GetMoveSetting()
    {
        return tween != null ? true : false;
    }
}
