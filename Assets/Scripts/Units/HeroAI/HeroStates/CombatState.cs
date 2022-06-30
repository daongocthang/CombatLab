using FiniteStateMachine;
using UnityEngine;


public class CombatState : AttackState
{
    private Hero _hero;
    private bool outOfVision;

    public CombatState(Entity entity, StateMachine stateMachine, UnitData data, Hero hero) : base(entity,
        stateMachine, data)
    {
        _hero = hero;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (outOfVision)
        {
            entity.target = null;
        }

        if (entity.target == null)
        {
            stateMachine.ChangeState(_hero.OccupyState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
        outOfVision = Detector.CheckTargetOutOfRange(entity.target, data.visionRange);
    }
}