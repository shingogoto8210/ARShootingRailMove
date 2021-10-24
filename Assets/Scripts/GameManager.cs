using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private RailMoveController railMoveController;
    [SerializeField,Header("経路用のパス群の元データ")]
    private RailPathData originRailPathData;
    [SerializeField,Header("パスにおけるミッションの発生の有無")]//Debug用
    private bool[] isMissionTriggers;

    [Header("現在のゲームの進行状況")]
    public GameState currentGameState;

    private void Start()
    {
        //ゲームの状態を準備中にする
        currentGameState = GameState.Wait;

        //ルート用の経路情報を設定
        originRailPathData = DataBaseManager.instance.GetRailPathDatasFromBranchNo(0, BranchDirectionType.NoBranch);

        //RailMoveControllerの初期設定
        railMoveController.SetUpRailMoveController(this);

        StartCoroutine(SetUpNextRailPathData());
    }

    /// <summary>
    /// パスデータよりミッションの発生有無情報取得
    /// </summary>
    private void SetMissionTriggers()
    {
        //配列の初期化
        isMissionTriggers = new bool[originRailPathData.GetIsMissionTriggers().Length];
        isMissionTriggers = originRailPathData.GetIsMissionTriggers();
    }

    /// <summary>
    /// ミッション発生有無の判定
    /// </summary>
    /// <param name="index"></param>
    public void CheckMissionTrigger(int index)
    {
        if (isMissionTriggers[index])
        {
            //TODO ミッション発生
            Debug.Log(index + "番目のミッション発生");

            //Debug用　今はそのまま進行
            railMoveController.ResumeMove();
        }
        else
        {
            //ミッションなし。次のパスへ移動を再開
            railMoveController.ResumeMove();
        }
    }

    /// <summary>
    /// ルートの分岐確認の準備
    /// </summary>
    /// <param name="nextbranchNo"></param>
    public void PreparateCheckNextBranch(int nextbranchNo)
    {
        StartCoroutine(CheckNextBranch(nextbranchNo));
    }

    /// <summary>
    /// ルートの分岐判定
    /// </summary>
    /// <param name="nextStagePathDataNo"></param>
    /// <returns></returns>
    private IEnumerator CheckNextBranch(int nextStagePathDataNo)
    {
        //次のルートがない場合
        if (nextStagePathDataNo >= DataBaseManager.instance.StagePathDataCount)
        {
            //終了
            Debug.Log("ゲーム終了");

            yield break;
        }

        //ルートに分岐があるかどうかの判定
        if(DataBaseManager.instance.GetBranchDatasListCount(nextStagePathDataNo) == 1)
        {
            Debug.Log("分岐なしで次のルートへ");

            //分岐なしの場合，次の経路を登録
            originRailPathData = DataBaseManager.instance.GetRailPathDatasFromBranchNo(nextStagePathDataNo, BranchDirectionType.NoBranch);
        }
        else
        {
            //TODO 分岐がある場合，UIに分岐を表示し，選択を待つ

            Debug.Log("ルートの分岐発生");

            //TODO 分岐を選択するまで待機

            //TODO 選択したルートを次のルートに設定
            
        }

        StartCoroutine(SetUpNextRailPathData());
    }

    /// <summary>
    /// 次のルートの初期設定
    /// </summary>
    private IEnumerator SetUpNextRailPathData()
    {
        //ルート内のミッション情報を設定
        SetMissionTriggers();

        //経路を移動先に設定
        railMoveController.SetNextRailPathData(originRailPathData);

        //レール移動の経路と移動登録が完了するまで待機
        yield return new WaitUntil(() => railMoveController.GetMoveSetting());

        //ゲームの進行状態を移動中に変更する
        currentGameState = GameState.Play_Move;
    }
}
