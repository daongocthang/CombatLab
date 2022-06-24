using UnityEngine;
[CreateAssetMenu(fileName ="newEnemyStats", menuName ="Add/Unit Stats")]
public class UnitStats : ScriptableObject
{
    [Header("Move Stats")]
    public float moveSpeed = 3.55f;
    [Header("Attack Stats")] 
    public AttackType attackType = AttackType.Melee;
    public float attackTime = 2.0f;
    public float attackRange = 2.0f;
    public float visionRange = 5.0f;
    
    public enum AttackType
    {
        Melee,
        Ranged
    }
}