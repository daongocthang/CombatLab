using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "newEnemyStats", menuName = "Add/Unit Stats")]
    public class PlayerData : ScriptableObject
    {
        [Header("Specify")] public float maxHealth;
        public float damage;

        [Header("Move")] public float moveSpeed = 3.55f;
        [Header("Attack")] public float attackTime = 2.0f;
        public float attackRange = 2.0f;
        public float visionRange = 5.0f;

        [Header("Character Controller")] public float motionSmoothTime = 0.1f;
        public float rotationSpeed = 5.0f;
        public float gravity = 1.0f;
    }
}