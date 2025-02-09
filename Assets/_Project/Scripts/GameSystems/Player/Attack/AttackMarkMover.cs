using UnityEngine;
using Zenject;

public class AttackMarkMover : MonoBehaviour
{
    [SerializeField] private AttackZone _attackZone;
    [SerializeField] private Camera _userInterfaceCamera;
    [SerializeField] private Canvas _markCanvas;

    private void Update()
    {
        if (_attackZone.NearestTarget == null)
        {
            _markCanvas.gameObject.SetActive(false);
            return;
        }

        if (_markCanvas.gameObject.activeInHierarchy == false)
            _markCanvas.gameObject.SetActive(true);

        _markCanvas.transform.LookAt(_userInterfaceCamera.transform, Vector3.up);
        _markCanvas.transform.parent.position = _attackZone.NearestTarget.CurrentPosition;
    }
}
