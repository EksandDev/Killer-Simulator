using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private List<IDamageable> _targets = new List<IDamageable>();

    public IDamageable NearestTarget => GetNearestTargetFromList(_targets);
    public IReadOnlyList<IDamageable> Targets => _targets;

    public IDamageable GetNearestTargetFromList(List<IDamageable> targets)
    {
        if (targets.Count == 0)
            return null;

        IDamageable nearestTarget = null;
        IDamageable inactiveTarget = null;

        if (targets.Count >= 1)
        {
            foreach (var target in targets)
            {
                if (target.IsActive == false)
                {
                    inactiveTarget = target;
                    continue;
                }

                var targetDistance = Vector3.Distance
                    (target.CurrentPosition, transform.position);

                if (nearestTarget == null)
                {
                    nearestTarget = target;
                    continue;
                }

                var nearestTargetDistance = Vector3.Distance
                    (nearestTarget.CurrentPosition, transform.position);

                if (nearestTargetDistance > targetDistance)
                    nearestTarget = target;
            }
        }

        else
            return targets[0];

        if (nearestTarget != null) 
            _targets.Remove(inactiveTarget);

        return nearestTarget;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            _targets.Add(damageable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            _targets.Remove(damageable);
        }
    }
}