using System;
using UnityEngine;

public class CombatDummy : MonoBehaviour, IDamageable
{
    private const float MaxHealth = 100;
    private Core _core;
    private Stats Stats => _stats ? _stats : _core.GetCoreComponent(ref _stats);
    private Stats _stats;

    private void Awake()
    {
        _core = GetComponentInChildren<Core>();
    }

    private void Start()
    {
        Stats.MaxHealth = MaxHealth;
        Stats.IncreaseHealth(MaxHealth);
        Stats.DisplayUI(false);
    }

    private void Update()
    {
        _core.LogicUpdate();

        if (Stats.EmptyHealth)
        {
            Stats.MaxHealth = MaxHealth;
            Stats.IncreaseHealth(MaxHealth);
        }
    }

    public void Damage(float amount)
    {
        Debug.Log(_core.transform.parent.name + " Damaged!");
        Stats.DisplayUI(true);
        Stats.DecreaseHealth(amount);
    }
}