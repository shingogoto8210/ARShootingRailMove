using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �C�x���g���ʗp�̒��ۃN���X
/// </summary>
public abstract class EventBase : MonoBehaviour
{
    public EventType eventType;

    /// <summary>
    /// �C�x���g���Ηp
    /// </summary>
    /// <param name="value"></param>
    /// <param name="hitBodyRegionType"></param>
    public abstract void TriggerEvent(int value, BodyRegionType hitBodyRegionType);
}
