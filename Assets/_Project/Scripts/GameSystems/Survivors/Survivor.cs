using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Survivor : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _rotateSpeed;

    public Animator Animator => _animator;
    public float RunSpeed => _runSpeed;
    public float RotateSpeed => _rotateSpeed;
    public StateMachine StateMachine { get; private set; } 
    public Rigidbody Rigidbody { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Transform[] SafePlaces { get; private set; }
    public Vector3 PathPointPosition { get; private set; }
    public Vector3 ChaserDirection { get; set; }
    public Vector3 ChaserPosition { get; set; }

    public void Initialize(Transform[] safePlaces)
    {
        StateMachine = new();
        Rigidbody = GetComponent<Rigidbody>();
        Agent = GetComponent<NavMeshAgent>();
        SafePlaces = safePlaces;

        StateMachine.AddState(new IdleState(StateMachine));
        StateMachine.AddState(new ChaseState(StateMachine, this));
        StateMachine.AddState(new DeadState(StateMachine));
        StateMachine.SetState<IdleState>();

        Agent.speed = RunSpeed;
        Agent.angularSpeed = RotateSpeed;
        Agent.stoppingDistance = 0;
    }

    private void Update()
    {
        StateMachine.Update();
    }
}

public class IdleState : State
{
    public IdleState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Update()
    {

    }
}

public class ChaseState : State
{
    private Survivor _survivor;

    private NavMeshAgent _agent => _survivor.Agent;
    private Transform _transform => _survivor.transform;
    private Vector3 _chaserDirection => _survivor.ChaserDirection;

    public ChaseState(StateMachine stateMachine, Survivor survivor) 
        : base(stateMachine)
    {
        _survivor = survivor;
    }

    public override void Enter()
    {
        _agent.SetDestination(_transform.position + _chaserDirection * 5);
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        if (_agent.remainingDistance >= 0.5f)
            return;

        Transform nearestSafePlace = null;

        foreach (var safePlace in _survivor.SafePlaces)
        {
            var safePlaceDistance = Vector3.Distance
                (safePlace.position, _transform.position);

            if (nearestSafePlace == null)
            {
                nearestSafePlace = safePlace;
                continue;
            }

            var nearestSafePlaceDistance = Vector3.Distance
                (nearestSafePlace.position, _transform.position);

            if (nearestSafePlaceDistance > safePlaceDistance)
                nearestSafePlace = safePlace;
        }

        _agent.SetDestination(nearestSafePlace.position);
    }
}

public class DeadState : State
{
    public DeadState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {

    }

    public override void Update()
    {

    }
}