using UnityEngine;

public class Hero : Entity
{
    [SerializeField] private UnitData data;


    public OccupyState OccupyState { get; private set; }
    public CombatState CombatState { get; private set; }

    public override void Awake()
    {
        base.Awake();
        OccupyState = new OccupyState(this, stateMachine, data, this);
        CombatState = new CombatState(this, stateMachine, data, this);
    }

    public override void Start()
    {
        base.Start();
        Stats.MaxHealth = data.maxHealth;
        Stats.IncreaseHealth(data.maxHealth);
        Stats.DisplayUI(false);

        stateMachine.Initialize(OccupyState);
    }

    public override void Damage(float amount)
    {
        Stats.DisplayUI(true);
        Stats.DecreaseHealth(amount);
        if (Stats.EmptyHealth)
            Destroy(gameObject);
    }

    public string OpponentTag => gameObject.tag switch {"Enemy" => "Player", "Player" => "Enemy", _ => ""};
}