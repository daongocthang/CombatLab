using FiniteStateMachine;
using UnityEngine;

public class OccupyState : State
{
    private GameObject occupiable;

    private Hero _hero;

    public OccupyState(Entity entity, StateMachine stateMachine, UnitData data, Hero hero) : base(entity,
        stateMachine, data)
    {
        _hero = hero;
    }

    public override void Enter()
    {
        base.Enter();
        occupiable = entity.FindNearestInWorld("Occupancy");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (occupiable != null)
        {
            if (Vector3.Distance(entity.transform.position, occupiable.transform.position) > data.attackRange)
            {
                entity.MoveTo(occupiable.transform.position, data.attackRange);
                entity.Steering(occupiable.transform.position);
            }
        }

        var opp = entity.FindNearestInRange(_hero.OpponentTag, data.visionRange);
        if (opp != null)
        {
            entity.target = opp;
            stateMachine.ChangeState(_hero.CombatState);
        }
    }
}