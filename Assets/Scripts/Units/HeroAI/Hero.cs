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


        stateMachine.Initialize(OccupyState);
    }

    public string OpponentTag => gameObject.tag switch {"Enemy" => "Player", "Player" => "Enemy", _ => ""};

    public override UnitData GetData()
    {
        return data;
    }

    public override void OnTriggered()
    {
        stateMachine.currentState.AnimTrigger();
    }
}