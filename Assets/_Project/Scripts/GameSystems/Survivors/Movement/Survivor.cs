using UnityEngine;
using UnityEngine.AI;

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
    public Vector3 PathPointPosition { get; }
    

    private void Start()
    {
        StateMachine = new();
        Rigidbody = GetComponent<Rigidbody>();
        Agent = GetComponent<NavMeshAgent>();

        StateMachine.AddState(new IdleState(StateMachine));
        StateMachine.AddState(new RunState(StateMachine, this));
        StateMachine.AddState(new DeadState(StateMachine));
        StateMachine.SetState<IdleState>();
    }

    private void Update()
    {
        StateMachine.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.PhysicsUpdate();
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

public class RunState : State
{
    private Rigidbody _rigidbody;
    private NavMeshAgent _agent;
    private Transform _transform;
    private Vector3 _pathPointPosition;
    private float _speed;

    public RunState(StateMachine stateMachine, Survivor survivor) 
        : base(stateMachine)
    {
        _rigidbody = survivor.Rigidbody;
        _agent = survivor.Agent;
        _transform = survivor.transform;
        _pathPointPosition = survivor.PathPointPosition;
        _speed = survivor.RunSpeed;
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

    public override void PhysicsUpdate()
    {
        Vector3 direction = (_pathPointPosition - _transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        _transform.rotation = Quaternion.Slerp
            (_transform.rotation, lookRotation, _speed * Time.deltaTime);
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