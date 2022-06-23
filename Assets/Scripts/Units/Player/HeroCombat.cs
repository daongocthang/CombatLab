using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HeroCombat : MonoBehaviour
{
    public enum AttackType
    {
        Melee,
        Ranged
    }

    [SerializeField]private const float AttackTime = 2f;
    [SerializeField] private AttackType attackType = AttackType.Melee;
    [SerializeField] private float attackRange = 2.0f;
    
    public float visionRange = 5.0f;

    public GameObject enemy { get; set; }
    public bool performAttack { get; set; }
    public Movement movement { get; private set; }
    private Vector3 _enemyPos;

    private void Start()
    {
        movement = GetComponent<Movement>();

        performAttack = true;
    }

    public void Update()
    {
        if (enemy != null)
        {
            _enemyPos = enemy.transform.position;
            if (Vector3.Distance(transform.position, _enemyPos) > attackRange)
            {
                movement.SetDestination(_enemyPos, attackRange - 0.25f);
            }
            else
            {
                movement.Steering(_enemyPos);
                if (performAttack)
                {
                    if (attackType == AttackType.Melee)
                    {
                        Debug.Log("Melee attacking to enemy");
                        StartCoroutine(MeleeAttackInterval());
                    }

                    if (attackType == AttackType.Ranged)
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
        return performAttack && (enemy == null) && (movement.forceMoving == false);
    }

    private IEnumerator MeleeAttackInterval()
    {
        performAttack = false;
       movement.animator.SetBool("NormalAttack", true);

        yield return new WaitForSeconds(AttackTime / ((100 + AttackTime) * 0.01f));

        if (enemy == null)
        {
            movement.animator.SetBool("NormalAttack", false);
            performAttack = true;
        }
    }

    private IEnumerator RangedAttackInterval()
    {
        performAttack = false;
        movement.animator.SetBool("NormalAttack", true);

        yield return new WaitForSeconds(AttackTime / ((100 + AttackTime) * 0.01f));

        if (enemy == null)
        {
            movement.animator.SetBool("NormalAttack", false);
            performAttack = true;
        }
    }

    public void MeleeAttack()
    {
        if (enemy != null)
        {
            // Damage the enemy
            Debug.Log("DAMAGE!");
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
}