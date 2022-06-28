using System;
using FiniteStateMachine;
using UnityEngine;
using UnityEngine.AI;


public class Player : MonoBehaviour
{
    private const float AngularSpeed = 1200;
    private const float Acceleration = 100;

    [SerializeField] private float motionSmoothTime = 0.1f;
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float gravity = 1.0f;

    [SerializeField] private UnitData data;
    public Animator anim { get; private set; }
    public JoystickHandler joystickHandler { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public Core core { get; private set; }

    private CharacterController _playerCtrl;
    private NavMeshAgent _agent;
    private float rotateVelocity;

    public PlayerMoveState MoveState { get; private set; }

    private void Awake()
    {
        core = GetComponentInChildren<Core>();
        stateMachine = new PlayerStateMachine();
        // New states
        MoveState = new PlayerMoveState(this, stateMachine, data);
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        joystickHandler = GameObject.FindWithTag("Joystick").GetComponent<JoystickHandler>();
        _playerCtrl = GetComponent<CharacterController>();
        _agent = GetComponent<NavMeshAgent>();

        _agent.angularSpeed = AngularSpeed;
        _agent.acceleration = Acceleration;
        _agent.speed = data.moveSpeed;

        stateMachine.Initialize(MoveState);
    }

    private void Update()
    {
        core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicUpdate();
    }

    public void MoveTo(float inputX, float inputZ)
    {
        var lookDir = new Vector3(inputX, 0, inputZ);

        if (lookDir != Vector3.zero)
        {
            var rotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        var moveDir = lookDir * data.moveSpeed;
        if (!_playerCtrl.isGrounded)
            moveDir.y -= gravity;
        _playerCtrl.Move(moveDir * Time.deltaTime);

        var spd = _playerCtrl.velocity.magnitude / data.moveSpeed;
        anim.SetFloat("Speed", spd, motionSmoothTime, Time.deltaTime);
    }

    public void MoveAttack(Vector3 targetPos, float range)
    {
        _agent.SetDestination(targetPos);
        _agent.stoppingDistance = range;

        var rotationToLookAt = Quaternion.LookRotation(targetPos - transform.position);
        var rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
            rotationToLookAt.eulerAngles.y,
            ref rotateVelocity,
            rotationSpeed * (Time.deltaTime * 5));

        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }
}