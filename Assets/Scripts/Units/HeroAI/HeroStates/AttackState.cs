using System.Collections;
using FiniteStateMachine;
using UnityEngine;

public class AttackState : State
{
    protected bool performAttack;
    protected bool outOfAtkRange;

    public AttackState(Entity entity, StateMachine stateMachine, UnitData data) : base(entity, stateMachine, data)
    {
        performAttack = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (entity.target != null)
        {
            var enemyPos = entity.target.transform.position;
            if (outOfAtkRange)
            {
                entity.MoveTo(enemyPos, data.attackRange - 0.25f);
            }
            else
            {
                entity.Steering(enemyPos);
                if (performAttack)
                {
                    Debug.Log("Melee attacking to enemy");
                    entity.StartCoroutine(AttackInterval());
                }
            }
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
        outOfAtkRange = entity.CheckTargetOutOfRange(data.attackRange);
    }

    public override void Exit()
    {
        base.Exit();
        entity.anim.SetBool("NormalAttack", false);
        performAttack = true;
    }

    private IEnumerator AttackInterval()
    {
        performAttack = false;
        entity.anim.SetBool("NormalAttack", true);

        var atkTime = data.attackTime;
        yield return new WaitForSeconds(atkTime / ((100 + atkTime) * 0.01f));

        if (entity.target == null)
        {
            entity.anim.SetBool("NormalAttack", false);
            performAttack = true;
        }
    }


    public virtual void TriggerAttack()
    {
        Debug.Log("damage");
    }
}