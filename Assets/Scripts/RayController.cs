using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rayによる弾の発射処理の制御クラス
/// </summary>
public class RayController : MonoBehaviour
{
    [Header("発射口用のエフェクトのサイズ調整")]
    public Vector3 muzzleFlashScale;

    private bool isShooting;

    private GameObject muzzleFlashObj;  //生成したエフェクトの代入用

    private GameObject target;　　　　　//Rayで補足した対象の代入用

    [SerializeField,Header("Ray用のレイヤー設定")]
    private int[] layerMasks;

    [SerializeField]  //Debug用。確認出来たら，SerializeField属性を削除してprivateにしておく
    private string[] layerMasksStr;

    [SerializeField]
    private PlayerController playerController;

    private void Start()
    {
        //Layerの情報を文字列に変換し，Raycastメソッドで利用しやすい情報を変数として作成しておく
        layerMasksStr = new string[layerMasks.Length];
        for(int i = 0; i < layerMasks.Length; i++)
        {
            layerMasksStr[i] = LayerMask.LayerToName(layerMasks[i]);
        }
    }

    private void Update()
    {
        //TODO ゲーム状態がプレイ中でない場合には処理を行わない制御をする

        //リロード判定（弾数0でリロード機能ありの場合）
        if(playerController.BulletCount == 0 && playerController.isReloadModeOn && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(playerController.ReloadBullet());
        }

        //発射判定（弾数が残っており，リロード実行中でない場合）押しっぱなしで発射できる
        if(playerController.BulletCount > 0 && !playerController.isReloading && Input.GetMouseButton(0))
        {
            StartCoroutine(ShootTimer());
        }
    }

    /// <summary>
    /// 継続的な弾の発射処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootTimer()
    {
        if (!isShooting)
        {
            isShooting = true;

            //発射エフェクトの表示。初回のみ生成し，２回目はオンオフで切り替える
            if(muzzleFlashObj == null)
            {
                //発射口の位置にRayControllerゲームオブジェクトを配置する
                muzzleFlashObj = Instantiate(EffectManager.instance.muzzleFlashPrefab, transform.position, transform.rotation);
                muzzleFlashObj.transform.SetParent(gameObject.transform);
                muzzleFlashObj.transform.localScale = muzzleFlashScale;
            }
            else
            {
                muzzleFlashObj.SetActive(true);
            }

            //発射
            Shoot();

            yield return new WaitForSeconds(playerController.shootInterval);

            muzzleFlashObj.SetActive(false);

            //if(hitEffect != null)
            //{
            //hitEffectObj.SetActive(false);
            //}

            isShooting = false;
        }
        else
        {
            yield return null;
        }
    }

    /// <summary>
    /// 弾の発射
    /// </summary>
    private void Shoot()
    {
        //カメラの位置からRayを投射
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 3.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray,out hit, playerController.shootRange, LayerMask.GetMask(layerMasksStr)))
        {
            Debug.Log(hit.collider.gameObject.name);

            //同じ対象を攻撃しているか確認。対象がいなかったか，同じ対象でない場合
            if(target == null || target != hit.collider.gameObject)
            {
                target = hit.collider.gameObject;

                Debug.Log(target.name);

                //TODO TryGetComponentの処理で敵や障害物などの情報を取得しつつ，判定をする

                //TODO 演出

                //同じ対象の場合
            }
            else
            {
                //TODO すでに情報があるので再取得はせずに判定飲みする
                //TODO 演出
            }
        }

        //弾数を減らす
        playerController.CalcBulletCount(-1);
    }
}
