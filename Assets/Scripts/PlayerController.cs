using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレーヤー情報の管理クラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    private int hp; 　　　　　//現在のHp値
    private int bulletCount;　//現在の弾数値

    [SerializeField,Header("最大Hp値")]
    private int maxHp;

    [SerializeField, Header("最大弾数値")]
    private int maxBullet;

    [SerializeField, Header("リロード時間")]
    private float reloadTime;

    [Header("弾の攻撃力")]
    public int bulletPower;

    [Header("弾の連射速度")]
    public float shootInterval;

    [Header("弾の射程距離")]
    public float shootRange;

    [Header("リロード機能のオン/オフ")]
    public bool isReloadModeOn;

    [Header("リロード状態の制御")]
    public bool isReloading;

    /// <summary>
    /// 弾数用のプロパティ
    /// </summary>
    public int BulletCount
    {
        set => bulletCount = Mathf.Clamp(value, 0, maxBullet);
        get => bulletCount;
    }
    void Start()
    {
        //Debug用
        SetUpPlayer();
    }

    /// <summary>
    /// プレイヤー情報の初期設定
    /// </summary>
    public void SetUpPlayer()
    {
        //maxHpの設定があるか確認。なければ初期値10でセットしてhpを設定
        hp = maxHp = maxHp == 0 ? 10 : maxBullet;

        //maxBulletの設定があるか確認。なければ初期値10でセットして弾数を設定
        BulletCount = maxBullet = maxBullet == 0 ? 10 : maxBullet;

        //インスペクター上の他の設定も初期値判定をつくった方が入力忘れがなく安心
        //TODO　そのほかの初期設定があればここで行う
    }

    //Hpの計算と更新
    public void CalcHp(int amount)
    {
        hp = Mathf.Clamp(hp += amount, 0, maxHp);
        Debug.Log("現在のHp :" + hp); //UIでhpが確認できるようになったらコメントアウト

        //TODO Hp表示の更新

        if(hp < 0)
        {
            Debug.Log("Game Over");
        }
    }

    /// <summary>
    /// 弾数の計算と更新
    /// </summary>
    /// <param name="amount"></param>
    public void CalcBulletCount(int amount)
    {
        BulletCount += amount;
        Debug.Log("現在の弾数 :" + BulletCount);　//UIで弾数が確認できるようになったらコメントアウト

        //TODO　弾数のUI表示更新
    }

    /// <summary>
    /// 弾数のリロード
    /// </summary>
    /// <returns></returns>
    public IEnumerator ReloadBullet()
    {
        //リロード状態にして，弾の発射を制御する
        isReloading = true;

        //リロード
        BulletCount = maxBullet;

        Debug.Log("リロード");  //UIで確認できるようになったらコメントアウト

        //TODO 弾数のUI表示更新

        //TODO SE

        //リロードの待機時間
        yield return new WaitForSeconds(reloadTime);

        //再度，弾が発射できる状態にする
        isReloading = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //TODO 敵からの攻撃によって被弾した場合の処理

        //TODO ボスや敵の攻撃範囲を感知しないようにするためにタグで判定するか，レイヤーを設定して回避する
    }

    void Update()
    {
        
    }
}
