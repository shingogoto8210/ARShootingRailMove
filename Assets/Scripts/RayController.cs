using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ray�ɂ��e�̔��ˏ����̐���N���X
/// </summary>
public class RayController : MonoBehaviour
{
    [Header("���ˌ��p�̃G�t�F�N�g�̃T�C�Y����")]
    public Vector3 muzzleFlashScale;

    private bool isShooting;

    private GameObject muzzleFlashObj;  //���������G�t�F�N�g�̑���p

    private GameObject target;�@�@�@�@�@//Ray�ŕ⑫�����Ώۂ̑���p

    [SerializeField,Header("Ray�p�̃��C���[�ݒ�")]
    private int[] layerMasks;

    [SerializeField]  //Debug�p�B�m�F�o������CSerializeField�������폜����private�ɂ��Ă���
    private string[] layerMasksStr;

    [SerializeField]
    private PlayerController playerController;

    private EventBase eventBase;

    private GameObject hitEffectObj;

    private void Start()
    {
        //Layer�̏��𕶎���ɕϊ����CRaycast���\�b�h�ŗ��p���₷������ϐ��Ƃ��č쐬���Ă���
        layerMasksStr = new string[layerMasks.Length];
        for(int i = 0; i < layerMasks.Length; i++)
        {
            layerMasksStr[i] = LayerMask.LayerToName(layerMasks[i]);
        }
    }

    private void Update()
    {
        //TODO �Q�[����Ԃ��v���C���łȂ��ꍇ�ɂ͏������s��Ȃ����������

        //�����[�h����i�e��0�Ń����[�h�@�\����̏ꍇ�j
        if(playerController.BulletCount == 0 && playerController.isReloadModeOn && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(playerController.ReloadBullet());
        }

        //���˔���i�e�����c���Ă���C�����[�h���s���łȂ��ꍇ�j�������ςȂ��Ŕ��˂ł���
        if(playerController.BulletCount > 0 && !playerController.isReloading && Input.GetMouseButton(0))
        {
            StartCoroutine(ShootTimer());
        }
    }

    /// <summary>
    /// �p���I�Ȓe�̔��ˏ���
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootTimer()
    {
        if (!isShooting)
        {
            isShooting = true;

            //���˃G�t�F�N�g�̕\���B����̂ݐ������C�Q��ڂ̓I���I�t�Ő؂�ւ���
            if(muzzleFlashObj == null)
            {
                //���ˌ��̈ʒu��RayController�Q�[���I�u�W�F�N�g��z�u����
                muzzleFlashObj = Instantiate(EffectManager.instance.muzzleFlashPrefab, transform.position, transform.rotation);
                muzzleFlashObj.transform.SetParent(gameObject.transform);
                muzzleFlashObj.transform.localScale = muzzleFlashScale;
            }
            else
            {
                muzzleFlashObj.SetActive(true);
            }

            //����
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
    /// �e�̔���
    /// </summary>
    private void Shoot()
    {
        //�J�����̈ʒu����Ray�𓊎�
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 3.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray,out hit, playerController.shootRange, LayerMask.GetMask(layerMasksStr)))
        {
            Debug.Log(hit.collider.gameObject.name);

            //�����Ώۂ��U�����Ă��邩�m�F�B�Ώۂ����Ȃ��������C�����ΏۂłȂ��ꍇ
            if(target == null || target != hit.collider.gameObject)
            {
                target = hit.collider.gameObject;

                Debug.Log(target.name);

                //TryGetComponent�̏����œG���Q���Ȃǂ̏����擾���C���������
                //�Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă���e�N���X���擾�ł��邩����
                if(target.TryGetComponent(out eventBase))
                {

                    //�擾�����e�N���X�ɂ��钊�ۃ��\�b�h�����s����@�����@�q�N���X�Ŏ������Ă��郁�\�b�h�̐U�镑���ɂȂ�
                    eventBase.TriggerEvent(playerController.bulletPower,BodyRegionType.Not_Available);

                    //���o
                    PlayHitEffect(hit.point, hit.normal);
                }

            //�����Ώۂ̏ꍇ
            }
            else if(target == hit.collider.gameObject)
            {
                //���łɏ�񂪂���̂ōĎ擾�͂����ɔ�����݂���
                eventBase.TriggerEvent(playerController.bulletPower, BodyRegionType.Not_Available);

                //���o
                PlayHitEffect(hit.point, hit.normal);
            }
        }

        //�e�������炷
        playerController.CalcBulletCount(-1);
    }

    private void PlayHitEffect(Vector3 effectPos,Vector3 surfacePos)
    {
        if(hitEffectObj == null)
        {
            hitEffectObj = Instantiate(EffectManager.instance.hitEffectPrefab, effectPos, Quaternion.identity);
        }
        else
        {
            hitEffectObj.transform.position = effectPos;
            hitEffectObj.transform.rotation = Quaternion.FromToRotation(Vector3.forward, surfacePos);

            hitEffectObj.SetActive(true);
        }
    }
}
