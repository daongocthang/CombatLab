using System;
using FiniteStateMachine;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;


public class Player : MonoBehaviour,ITriggerable,IParcelable
{
    [SerializeField] private float motionSmoothTime = 0.1f;
    [SerializeField] private UnitData data;

    public Animator Anim { get; private set; }
    public Core Core { get; private set; }
    public CharacterController Controller { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    private PlayerStateMachine _stateMachine;


    // Player States
    public PlayerMoveState MoveState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        _stateMachine = new PlayerStateMachine();
        // New states
        MoveState = new PlayerMoveState(this, _stateMachine, data);
        AttackState = new PlayerAttackState(this, _stateMachine, data);
    }

    private void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        Agent = GetComponent<NavMeshAgent>();

        Agent.speed = data.moveSpeed;
        _stateMachine.Initialize(MoveState);

        Agent.enabled = false;
    }

    private void Update()
    {
        Core.LogicUpdate();
        _stateMachine.currentState.LogicUpdate();

        var spd = Agent.enabled
            ? Agent.velocity.magnitude / Agent.speed
            : Controller.velocity.magnitude / data.moveSpeed;
        Anim.SetFloat("Speed", spd, motionSmoothTime, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _stateMachine.currentState.PhysicUpdate();
    }

    public void OnTriggered()
    {
        _stateMachine.currentState.AnimTrigger();
    }

    public UnitData GetData()
    {
        return data;
    }
}