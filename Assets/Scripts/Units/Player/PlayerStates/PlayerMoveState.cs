using FiniteStateMachine;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private float _inputX, _inputZ;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, UnitData data) : base(player,
        stateMachine, data)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.MoveTo(_inputX, _inputZ);
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _inputX = player.joystickHandler.InputHorizontal;
        _inputZ = player.joystickHandler.InputVertical;
        Debug.Log($"{_inputX}/{_inputZ}");
    }
}