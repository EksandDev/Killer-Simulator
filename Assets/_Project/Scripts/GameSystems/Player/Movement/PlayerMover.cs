using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;

    private Rigidbody _rigidbody;
    private PlayerInput _input;

    private const string MOVE_ACTION = "Movement";
    private const string IS_RUNNING = "IsRunning";

    public Vector3 Direction { get; private set; }

    public float Speed
    {
        get => _speed;
        set
        {
            if (value <= 0 || _speed == 0)
                return;

            _speed = value;
        }
    }

    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Direction = _input.actions.FindAction(MOVE_ACTION).ReadValue<Vector3>().normalized;

        if (Direction != Vector3.zero)
            _animator.SetBool(IS_RUNNING, true);

        else
            _animator.SetBool(IS_RUNNING, false);
    }

    private void FixedUpdate() => _rigidbody.velocity = Direction * _speed * Time.fixedDeltaTime;
}