using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    //Player�p
    [Header("���ˌ��p�̃G�t�F�N�g")]
    public GameObject muzzleFlashPrefab;

    [Header("�G�ɒe�����������Ƃ��̃G�t�F�N�g")]
    public GameObject hitEffectPrefab;

    //TODO �K�X�ǉ�����

    //�G�p

    //����

    private void Awake()
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

    //enum�ɃG�t�F�N�g�̎�ނ�o�^���Ă����āC����������Ƃ��C�ΏۂƂȂ�G�t�F�N�g�̏����擾����Q�b�^�[���\�b�h���������Ă��悢
}
