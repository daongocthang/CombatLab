using System.Collections;
using System.Linq;
using FiniteStateMachine;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour, IDamageable
{
    private const float AngularSpeed = 1200;
    private const float Acceleration = 100;

    [SerializeField] private float motionSmoothTime = 0.1f;

    public Animator anim { get; private set; }
    public NavMeshAgent agent { get; private set; }
    public Core core { get; private set; }
    public StateMachine stateMachine { get; private set; }

    public GameObject target { get; set; }

    protected Stats Stats => _stats ?? core.GetCoreComponent(ref _stats);
    private Stats _stats; 
        

    public virtual void Awake()
    {
        core = GetComponentInChildren<Core>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        stateMachine = new StateMachine();
    }

    public virtual void Start()
    {
        agent.angularSpeed = AngularSpeed;
        agent.acceleration = Acceleration;
    }

    public virtual void Update()
    {
        core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();

        var speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicUpdate();
    }

    public void MoveTo(Vector3 targetPos, float stopRange = 0)
    {
        agent.SetDestination(targetPos);
        agent.stoppingDistance = stopRange;
    }

    public virtual void Damage(float amount)
    {
    }
}