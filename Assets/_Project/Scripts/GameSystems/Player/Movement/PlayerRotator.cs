using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private float _speed;

    private bool _isActive = true;

    public bool IsActive { get => _isActive; set => _isActive = value; }

    public IEnumerator Follow(Transform target)
    {
        while (!_isActive)
        {
            RotateTo(target.position);

            yield return null;
        }
    }

    private void Update()
    {
        if (!_isActive)
            return;

        if (_mover.Direction != Vector3.zero)
            RotateTo(transform.position + _mover.Direction);
    }

    private void RotateTo(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.DORotateQuaternion(lookRotation, 0.5f).SetLink(gameObject);
    }
}