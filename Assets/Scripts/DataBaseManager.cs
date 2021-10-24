using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各種データベース用のスクリプタブルオブジェクトの管理クラス
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
    /// ステージパス番号から分岐先のRailPathData情報を取得
    /// </summary>
    /// <param name="nextStagePathDataNo"></param>
    /// <param name="searchBranchDirectionType"></param>
    /// <returns></returns>
    public RailPathData GetRailPathDatasFromBranchNo(int nextStagePathDataNo,BranchDirectionType searchBranchDirectionType)
    {
        return stagePathDataSO.stagePathDatasList[nextStagePathDataNo].branchDatasList.Find(x => x.branchDirectionType == searchBranchDirectionType).railPathData;
    }

    /// <summary>
    /// ゲッターメソッドヲ使ってステージ内のルートの数の取得
    /// </summary>
    /// <returns></returns>
    //public int GetStagePathDatasListCount()
    //{
      //  return stagePathDataSO.stagePathDatasList.Count;
    //}

    /// <summary>
    /// プロパティのGetヲ使ってステージ内のルートの数の取得
    /// </summary>
    public int StagePathDataCount
    {
        get => stagePathDataSO.stagePathDatasList.Count;
    }

    /// <summary>
    /// ブランチの管理している分岐数の取得
    /// </summary>
    /// <param name="branchNo"></param>
    /// <returns></returns>
    public int GetBranchDatasListCount(int branchNo)
    {
        return stagePathDataSO.stagePathDatasList[branchNo].branchDatasList.Count;
    }
}
