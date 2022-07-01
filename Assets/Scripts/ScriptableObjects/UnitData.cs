using UnityEngine;
[CreateAssetMenu(fileName ="newEnemyStats", menuName ="Add/Unit Stats")]
public class UnitData : ScriptableObject
{

    [Header("Basic Stats")] 
    public float hp;
    public float hp5;
    public float damage;

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
    
    public enum SideType
    {
        Enemy,
        Player
    }
}