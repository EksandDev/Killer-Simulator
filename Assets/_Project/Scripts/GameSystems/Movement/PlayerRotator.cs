using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private float _speed;

    private bool _isActive = true;

    public bool IsActive { get => _isActive; set => _isActive = value; }

    public IEnumerator Follow(Vector3 position)
    {
        while (!_isActive)
        {
            Vector3 direction = (position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp
                (transform.rotation, lookRotation, _speed * Time.deltaTime);

            yield return null;
        }
    }

    private void Update()
    {
        if (_isActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 direction = (hit.point - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp
                    (transform.rotation, lookRotation, _speed * Time.deltaTime);
            }
        }
    }

    private void RotateTo(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp
            (transform.rotation, lookRotation, _speed * Time.deltaTime);
    }
}