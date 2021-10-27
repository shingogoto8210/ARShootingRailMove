using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミーの部位ごとのダメージを計算するクラス
/// </summary>
public class BodyRegionPartsController : MonoBehaviour
{
    [SerializeField, Header("部位の設定")]
    private BodyRegionType bodyPartsType;     //アタッチしたゲームオブジェクトに持たせたい情報をインスペクターより設定する
    
    /// <summary>
    /// 部位の情報の取得用。プロパティでも可
    /// </summary>
    /// <returns></returns>
    public BodyRegionType GetBodyType()
    {
        return bodyPartsType;
    }

    public (int,BodyRegionType) CalcDamageParts(int damage)
    {
        //部位の情報を利用してダメージを計算する
        int lastDamage = bodyPartsType switch
        {

            //頭部の場合はダメージ5倍
            BodyRegionType.Head => damage * 5,

            //TODO 他の部位を追加

            //上記以外
            _ => damage,
        };

        //処理結果をタプル型で戻す
        return (lastDamage, bodyPartsType);
    }
}
