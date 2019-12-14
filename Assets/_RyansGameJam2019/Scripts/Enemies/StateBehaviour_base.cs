//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RGJ {
public abstract class StateBehaviour_base : MonoBehaviour
{
    [SerializeField, ReadOnly] private bool isActive;
    public bool IsActive
    {
        get => isActive;
        set {
            if (value == isActive) return;
            isActive = value;
            OnStateChanged();
        }
    }

    private void OnStateChanged()
    {
        if(IsActive) OnEnterState();
        else OnLeaveState();
    }

    protected abstract void OnEnterState();

    protected abstract void OnLeaveState();
}
}