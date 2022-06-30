using System.Collections;
using System.Linq;
using FiniteStateMachine;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    private const float AngularSpeed = 1200;
    private const float Acceleration = 100;

    [SerializeField] private float motionSmoothTime = 0.1f;
    [SerializeField] private float rotateSpeedMovement = 0.1f;

    public Animator anim { get; private set; }
    public NavMeshAgent agent { get; private set; }
    public Core core { get; private set; }
    public StateMachine stateMachine { get; private set; }

    public GameObject target;

    private float rotateVelocity;

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

    public void Steering(Vector3 targetPos)
    {
        // STEERING
        var rotationToLookAt = Quaternion.LookRotation(targetPos - transform.position);
        var rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
            rotationToLookAt.eulerAngles.y,
            ref rotateVelocity,
            rotateSpeedMovement * (Time.deltaTime * 5));

        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }

    public void MoveTo(Vector3 targetPos, float stopRange = 0)
    {
        agent.SetDestination(targetPos);
        agent.stoppingDistance = stopRange;
    }

    public bool CheckTargetOutOfRange(float range)
    {
        if (target == null) return true;

        return Vector3.Distance(transform.position, target.transform.position) > range;
    }

    public GameObject FindNearestInWorld(string tagName)
    {
        var hitObjects = GameObject.FindGameObjectsWithTag(tagName);
        if (hitObjects.Length > 0)
        {
            hitObjects = hitObjects.OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
                .ToArray();
            return hitObjects.First();
        }

        return null;
    }

    public GameObject FindNearestInRange(string tagName, float range)
    {
        var objects = GameObject.FindGameObjectsWithTag(tagName);


        objects = objects.Where(e => Vector3.Distance(transform.position, e.transform.position) <= range)
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();


        return objects.FirstOrDefault()?.gameObject;
    }
}