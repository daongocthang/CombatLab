using System;
using UnityEngine;

public class Combat : CoreComponent, IDamageable
{
    private Stats Stats => _stats ? _stats : core.GetCoreComponent(ref _stats);
    private Stats _stats;

    private Transform parent;
    private UnitData _data;

    private void Start()
    {
        parent = core.transform.parent;
        _data = parent.GetComponent<IParcelable>().GetData();

        Stats.MaxHealth = _data.maxHealth;
        Stats.IncreaseHealth(Stats.MaxHealth);
        Stats.DisplayUI(false);
    }

    public void Damage(float amount)
    {
        Stats.DisplayUI(true);
        Stats.DecreaseHealth(amount);
        if (Stats.EmptyHealth)
        {
            Destroy(parent.gameObject);
        }
    }
}