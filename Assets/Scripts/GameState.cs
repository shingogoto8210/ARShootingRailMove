/// <summary>
/// ゲームの進行状態の種類
/// </summary>
public enum GameState
{
    Debug,         //AR時にエディターでデバッグを行う際に適用
    Tracking,　　　//ARのトラッキング中
    Wait,          //トラッキング完了後，ゲームの準備
    Play_Move,     //移動中
    Play_Mission,  //ミッション中
    GameUp,        //ゲーム終了(ゲームオーバー，ゲームクリア）
}
