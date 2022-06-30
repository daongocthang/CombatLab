using FiniteStateMachine;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private Movement Movement => _movement ?? core.GetCoreComponent(ref _movement);
    private Movement _movement;

    private GameInput GameInput => _gameInput ?? core.GetCoreComponent(ref _gameInput);
    private GameInput _gameInput;
    private bool _attack;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, UnitData data) : base(player,
        stateMachine, data)
    {
    }

    public override void Enter()
    {
        base.Enter();
        GameInput.OnAttack(delegate { _attack = true; });
        player.Controller.enabled = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        var point = GameInput.GetAxis().normalized;
        Movement.ManualMove(point, data.moveSpeed);
        Movement.Steering(player.transform, point);

        if (_attack)
            stateMachine.ChangeState(player.AttackState);
    }

    public override void Exit()
    {
        base.Exit();
        _attack = false;
        player.Controller.enabled = false;
    }
}