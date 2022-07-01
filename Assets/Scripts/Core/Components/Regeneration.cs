using System;
using UnityEngine;

public class Regeneration : CoreComponent
{
    [SerializeField] private float regenTime = 1.0f;

    private Stats Stats => _stats ? _stats : core.GetCoreComponent(ref _stats);
    private Stats _stats;

    private Combat Combat => _combat ? _combat : core.GetCoreComponent(ref _combat);
    private Combat _combat;


    private Transform parent;
    private UnitData _data;
    private SandTimer _hpRegenHandler;

    private void Start()
    {
        parent = core.transform.parent;
        _data = parent.GetComponent<IParcelable>().GetData();
        _hpRegenHandler = new SandTimer(delegate
        {
            Debug.Log($"{parent.name} has regenerated {_data.hp5}");
            Stats.IncreaseHealth(_data.hp5);
        }, regenTime);
        _hpRegenHandler.Start();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!Combat.UnderAttack && _hpRegenHandler.RunAfterDelayed())
            _hpRegenHandler.Start();
    }
}