using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;

public class Hero : Entity
{
    [SerializeField] private UnitData data;
    [SerializeField] private UnitData.SideType opponent;

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

    public string OpponentTag => opponent.ToString();
}