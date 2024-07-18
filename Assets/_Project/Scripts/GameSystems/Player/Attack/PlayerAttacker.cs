using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerRotator _rotator;
    [SerializeField] private AttackZone _attackZone;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _lineCastStartPoint;
    [SerializeField] private float _attackSphereRadius;

    private PlayerInput _input;
    private int _damage = 5;
    private bool _isAttacking;

    private const string ATTACK_ACTION = "Attack";
    private const string IS_ATTACKING = "IsAttacking";

    public bool IsAttacking
    {
        get => _isAttacking;
        private set
        {
            _isAttacking = value;
            _animator.SetBool(IS_ATTACKING, _isAttacking);
        }
    }

    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        _input.actions.FindAction(ATTACK_ACTION).performed += OnAttack;
    }

    private void OnAttack(InputAction.CallbackContext context) => StartCoroutine(PerformAttack());

    private IEnumerator PerformAttack()
    {
        if (_isAttacking)
            yield break;

        IsAttacking = true;
        var moverSpeed = _mover.Speed;
        _mover.Speed /= 2;
        _rotator.IsActive = false;
        var nearestTarget = _attackZone.NearestTarget;

        if (nearestTarget != null)
            StartCoroutine(_rotator.Follow(nearestTarget.CurrentTransform));

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0).Length / 1.6f);

        nearestTarget = GetTargetInAttackRange();
        RaycastHit hit;

        if (nearestTarget != null && Physics.Linecast
            (_lineCastStartPoint.position, nearestTarget.CurrentPosition, out hit) &&
            hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
                damageable.CurrentHealth -= _damage;
        }

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0).Length / 1.6f);

        IsAttacking = false;
        _mover.Speed = moverSpeed;
        _rotator.IsActive = true;
    }

    private IDamageable GetTargetInAttackRange()
    {
        Collider[] hitedColliders;
        hitedColliders = Physics.OverlapSphere(_attackPoint.position, _attackSphereRadius);
        List<IDamageable> targets = new List<IDamageable>();

        foreach (var collider in hitedColliders)
        {
            if (collider.TryGetComponent<IDamageable>(out IDamageable damageable))
                targets.Add(damageable);
        }

        return _attackZone.GetNearestTargetFromList(targets);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackSphereRadius);
    }
}