using System;
using UnityEngine;
using UnityEngine.UI;

public class Stats : CoreComponent
{
    [SerializeField] private float cooldown = 5.0f;
    [SerializeField] private bool alwaysDisplay;
    public bool EmptyHealth { get; private set; }
    private bool _hidden;
    public float MaxHealth { get; set; }
    private float _health;
    private Slider _healthBar;
    private SandTimer _sandTimer;

    private void Start()
    {
        _healthBar = GetComponentInChildren<Slider>();
        if (!alwaysDisplay)
        {
            _sandTimer = new SandTimer(delegate { DisplayUI(false); }, cooldown);
            DisplayUI(false);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _healthBar.value = GetHealthNormalized();
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
        _sandTimer?.RunAfterDelayed();
        if (alwaysDisplay && _hidden)
        {
            DisplayUI(true);
        }
    }

    public void DecreaseHealth(float amount)
    {
        _health -= amount;

        if (_health <= 0)
        {
            _health = 0;
            EmptyHealth = true;
        }
    }

    public void IncreaseHealth(float amount)
    {
        _health = Mathf.Clamp(_health + amount, 0, MaxHealth);
        if (_health > 0)
            EmptyHealth = false;
    }

    public void DisplayUI(bool enable)
    {
        if (enable) _sandTimer?.Start();

        if (_hidden != enable) return;
        _hidden = !enable;
        GetComponent<Canvas>().enabled = enable;
    }

    private float GetHealthNormalized()
    {
        return _health / MaxHealth;
    }
}