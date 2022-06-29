using System;
using UnityEngine;
using UnityEngine.AI;

public class Movement : CoreComponent
{
    private const float AngularSpeed = 1200;
    private const float Acceleration = 100;

    [SerializeField] private float motionSmoothTime = 0.1f;
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float gravity = 1.0f;

    private Animator _anim;

    private CharacterController _controller;
    private NavMeshAgent _agent;
    private float _rotateVelocity;

    private void Start()
    {
        _controller = GetComponentInParent<CharacterController>();
        _agent = GetComponentInParent<NavMeshAgent>();
        _anim = core.transform.parent.GetComponentInChildren<Animator>();

        _agent.acceleration = Acceleration;
        _agent.angularSpeed = AngularSpeed;
    }

    public void Move(Vector3 point, float speed)
    {
        if (_controller == null)
        {
            Debug.LogWarning($"Not found {typeof(CharacterController)} in parent");
            return;
        }

        var moveDir = point * speed * Time.deltaTime;

        if (!_controller.isGrounded)
            moveDir.y -= gravity;
        _controller.Move(moveDir);

        var spd = _controller.velocity.magnitude / speed;
        _anim.SetFloat("Speed", spd, motionSmoothTime, Time.deltaTime);
    }

    public void MeshMove(Vector3 point, float range = 0)
    {
        _agent.SetDestination(point);
        _agent.stoppingDistance = range;

        var spd = _agent.velocity.magnitude / _agent.speed;
        _anim.SetFloat("Speed", spd, motionSmoothTime, Time.deltaTime);
    }

    public void LookRotation(Transform intent, Vector3 point)
    {
        if (point == Vector3.zero) return;

        var rotationToLookAt = Quaternion.LookRotation(point);
        intent.rotation = Quaternion.Lerp(intent.rotation, rotationToLookAt, rotationSpeed * Time.deltaTime);
    }
}