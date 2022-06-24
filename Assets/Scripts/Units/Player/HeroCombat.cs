using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class HeroCombat : MonoBehaviour,ITriggerable
{
    public GameObject enemy;
    public bool performAttack { get; set; }
    public Character character { get; private set; }
    private Vector3 _enemyPos;

    private void Start()
    {
        character = GetComponent<Character>();

        performAttack = true;
    }

    public void Update()
    {
        if (enemy != null)
        {
            _enemyPos = enemy.transform.position;
            var atkRange = character.stats.attackRange;
            if (Vector3.Distance(transform.position, _enemyPos) > atkRange)
            {
                character.SetDestination(_enemyPos, atkRange - 0.25f);
            }
            else
            {
                character.Steering(_enemyPos);
                if (performAttack)
                {
                    var atkType = character.stats.attackType;
                    if (atkType == UnitStats.AttackType.Melee)
                    {
                        Debug.Log("Melee attacking to enemy");
                        StartCoroutine(MeleeAttackInterval());
                    }

                    if (atkType == UnitStats.AttackType.Ranged)
                    {
                        Debug.Log("Ranged attacking to enemy");
                        StartCoroutine(RangedAttackInterval());
                    }
                }
            }
        }
    }

    public bool EnableEnemyDetection()
    {
        return performAttack && (enemy == null) && (character.forceMoving == false);
    }

    private IEnumerator MeleeAttackInterval()
    {
        performAttack = false;
        character.animator.SetBool("NormalAttack", true);
        
        var atkTime = character.stats.attackTime;
        yield return new WaitForSeconds(atkTime / ((100 + atkTime) * 0.01f));

        if (enemy is null)
        {
            character.animator.SetBool("NormalAttack", false);
            performAttack = true;
        }
    }

    private IEnumerator RangedAttackInterval()
    {
        performAttack = false;
        character.animator.SetBool("NormalAttack", true);

        var atkTime = character.stats.attackTime;
        yield return new WaitForSeconds(atkTime / ((100 + atkTime) * 0.01f));

        if (enemy == null)
        {
            character.animator.SetBool("NormalAttack", false);
            performAttack = true;
        }
    }

    public void MeleeAttack()
    {
        if (enemy != null)
        {
            // Damage the enemy
            Debug.Log("Enemy is damaged");
            performAttack = true;
        }
    }

    public void RangedAttack()
    {
        if (enemy != null)
        {
            // Spawn projectile

            performAttack = true;
        }
    }

    public void forceStop()
    {
        enemy = null;
        performAttack = true;
        character.animator.SetBool("NormalAttack", false);
    }

    public void OnTriggered()
    {
        switch (character.stats.attackType)
        {
            case UnitStats.AttackType.Melee:
                MeleeAttack();
                break;
            case UnitStats.AttackType.Ranged:
                RangedAttack();
                break;
        }
    }
}