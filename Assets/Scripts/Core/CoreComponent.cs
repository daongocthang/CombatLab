using System;
using Interfaces;
using UnityEngine;

public class CoreComponent : MonoBehaviour, ILogicUpdate
{
    protected Core core;

    private void Awake()
    {
        core = transform.parent.GetComponent<Core>();
        if (core == null)
        {
            Debug.LogError("There is not Core on the parent");
        }
        core.AddComponent(this);
    }

    public virtual void LogicUpdate()
    {
    }
}