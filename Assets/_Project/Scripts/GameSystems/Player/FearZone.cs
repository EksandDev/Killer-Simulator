using System.Collections;
using UnityEngine;

public class FearZone : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private float _timeToDisableChase;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Survivor>(out Survivor survivor))
        {
            StopAllCoroutines();
            survivor.ChaserDirection = _playerMover.Direction;
            survivor.StateMachine.SetState<ChaseState>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Survivor>(out Survivor survivor))
            StartCoroutine(ChaseDisable(survivor));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Survivor>(out Survivor survivor))
        {
            survivor.ChaserDirection = _playerMover.Direction;
        }
    }

    private IEnumerator ChaseDisable(Survivor survivor)
    {
        yield return new WaitForSeconds(_timeToDisableChase);

        survivor.StateMachine.SetState<IdleState>();
    }
}
