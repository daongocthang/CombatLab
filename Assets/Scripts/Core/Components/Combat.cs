using System;
using Unity.Android.Gradle;
using UnityEngine;

public class Combat : CoreComponent, IDamageable
{
    [SerializeField] private float cooldown = 5.0f;
    public bool UnderAttack { get; private set; }
    private Stats Stats => _stats ? _stats : core.GetCoreComponent(ref _stats);
    private Stats _stats;

    private SandTimer _sandTimer;
    private Transform parent;
    private UnitData _data;

    private void Start()
    {
        parent = core.transform.parent;
        _data = parent.GetComponent<IParcelable>().GetData();
        _sandTimer = new SandTimer(delegate { UnderAttack = false; }, cooldown);

        Stats.MaxHealth = _data.hp;
        Stats.IncreaseHealth(Stats.MaxHealth);
        Stats.DisplayUI(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _sandTimer.RunAfterDelayed();
    }

    public void Damage(float amount)
    {
        _sandTimer.Start();
        UnderAttack = true;
        
        Stats.DisplayUI(true);
        Stats.DecreaseHealth(amount);
        if (Stats.EmptyHealth)
        {
            Destroy(parent.gameObject);
        }
    }
}