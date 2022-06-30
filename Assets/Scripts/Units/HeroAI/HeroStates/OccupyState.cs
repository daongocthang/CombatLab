using FiniteStateMachine;
using UnityEngine;

public class OccupyState : State
{
    private Detector Detector => _detector ?? core.GetCoreComponent(ref _detector);
    private Detector _detector;

    private Movement Movement => _movement ?? core.GetCoreComponent(ref _movement);
    private Movement _movement;

    private GameObject occupiable;
    private bool detectedOpancy;

    private Hero _hero;

    public OccupyState(Entity entity, StateMachine stateMachine, UnitData data, Hero hero) : base(entity,
        stateMachine, data)
    {
        _hero = hero;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (detectedOpancy)
        {
            Movement.AutoMove(occupiable.transform.position, data.attackRange);
        }

        var opp = Detector.FindNearestInRange(_hero.OpponentTag, data.visionRange);
        if (opp != null)
        {
            entity.target = opp;
            stateMachine.ChangeState(_hero.CombatState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (occupiable)
        {
            detectedOpancy = Detector.CheckTargetOutOfRange(occupiable, data.attackRange);
        }
        else
        {
            occupiable = Detector.FindNearestInWorld("Occupancy");
        }
    }
}