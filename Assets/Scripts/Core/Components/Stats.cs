using System;
using UnityEngine;
using UnityEngine.UI;

public class Stats : CoreComponent
{
    [SerializeField] private float cooldown = 5.0f;
    public bool EmptyHealth { get; private set; }
    public float MaxHealth { get; set; }
    private float _health;
    private Slider _healthBar;
    private float _sandTime;
    private bool _hidden;
    private void Start()
    {
        _healthBar = GetComponentInChildren<Slider>();
        DisplayUI(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _healthBar.value = GetHealthNormalized();
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
        if (_sandTime > 0)
        {
            _sandTime -= 1 / cooldown * Time.deltaTime;
            if (_sandTime <= 0)
            {
                _sandTime = 0;
                DisplayUI(false);
            }
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
        if (enable) _sandTime = 1;
        
        if(_hidden!=enable) return;
        _hidden = !enable;
        GetComponent<Canvas>().enabled = enable;
        
    }

    private float GetHealthNormalized()
    {
        return _health / MaxHealth;
    }
}