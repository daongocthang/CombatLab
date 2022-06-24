using System;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    private const float ANGULAR_SPD = 1200;
    private const float ACCELERATION = 100;
    
    [SerializeField] private float rotateSpeedMovement = 0.1f;
    [SerializeField] private float motionSmoothTime = 0.1f;
    public Animator animator;
    public UnitStats stats;
    public NavMeshAgent agent { get; private set; }
    public bool forceMoving { get; set; }
    private float _rotateVelocity;
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.speed = stats.moveSpeed;
        agent.angularSpeed = ANGULAR_SPD;
        agent.acceleration = ACCELERATION;
    }

    public void Update()
    {
        var speed = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
    }

    public void Steering(Vector3 targetPos)
    {
        // STEERING
        var rotationToLookAt = Quaternion.LookRotation(targetPos - transform.position);
        var rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
            rotationToLookAt.eulerAngles.y,
            ref _rotateVelocity,
            rotateSpeedMovement * (Time.deltaTime * 5));

        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }

    public void SetDestination(Vector3 targetPos, float stopRange = 0)
    {
        agent.SetDestination(targetPos);
        agent.stoppingDistance = stopRange;
    }
}