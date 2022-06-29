using System.Collections;
using FiniteStateMachine;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private GameInput GameInput => _gameInput ?? core.GetCoreComponent(ref _gameInput);
    private GameInput _gameInput;

    private Movement Movement => _movement ?? core.GetCoreComponent(ref _movement);
    private Movement _movement;

    private Detector Detector => _detector ?? core.GetCoreComponent(ref _detector);
    private Detector _detector;

    private bool _performAttack;
    private bool _outOfAtkRange;
    private GameObject _opponent;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, UnitData data) : base(player,
        stateMachine, data)
    {
        _performAttack = true;
    }

    public override void Enter()
    {
        base.Enter();
        _opponent = Detector.FindNearestInRange("Enemy", data.visionRange);
        player.Agent.enabled = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_opponent != null)
        {
            if (_outOfAtkRange)
            {
                Movement.MeshMove(_opponent.transform.position, data.attackRange - 0.25f);
            }
            else
            {
                player.Anim.SetFloat("Speed", 0);
                if (_performAttack)
                {
                    Debug.Log("Melee attacking to enemy");
                    player.StartCoroutine(AttackInterval());
                }
            }
        }
        else
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _performAttack = true;
        player.Anim.SetBool("NormalAttack", false);
        player.Agent.enabled = false;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _outOfAtkRange = Detector.CheckTargetOutOfRange(_opponent, data.attackRange);
        if (GameInput.GetAxis() != Vector3.zero)
            _opponent = null;
    }


    private IEnumerator AttackInterval()
    {
        _performAttack = false;
        player.Anim.SetBool("NormalAttack", true);

        var atkTime = data.attackTime;
        yield return new WaitForSeconds(atkTime / ((100 + atkTime) * 0.01f));

        if (_opponent == null)
        {
            player.Anim.SetBool("NormalAttack", false);
            _performAttack = true;
        }
    }
}