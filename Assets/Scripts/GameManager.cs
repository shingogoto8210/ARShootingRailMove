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

    void Start()
    {
        //TODO ゲームの状態を準備中にする
        //TODO ルート用の経路情報を設定

        //RailMoveControllerの初期設定
        railMoveController.SetUpRailMoveController(this);

        //パスデータよりミッションの発生有無情報取得
        SetMissionTriggers();

        //次に再生するレール移動の目的地と経路のパスを設定
        railMoveController.SetNextRailPathData(originRailPathData);

        //TODO 経路の準備が完了するのを待つ（Startメソッドの戻り値をIEnumeratorに変更してコルーチンメソッドに変える）
        //TODO ゲームの状態をプレイ中に変える
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
}
