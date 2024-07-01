using UnityEngine;
using Zenject;

public class AttackMarkMover : MonoBehaviour
{
    [SerializeField] private GameObject _mark;

    private AttackZone _attackZone;

    #region Zenject init
    [Inject]
    private void Init(AttackZone attackZone)
    {
        _attackZone = attackZone;
    }
    #endregion

    private void Update()
    {
        if (_attackZone.NearestTarget == null)
        {
            _mark.SetActive(false);
            return;
        }

        if (_mark.activeInHierarchy == false)
            _mark.SetActive(true);

        _mark.transform.position = _attackZone.NearestTarget.CurrentPosition;
    }
}
