using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerAttacker _attacker;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerRotator _rotator;
    [SerializeField] private AttackZone _attackZone;

    public override void InstallBindings()
    {
        Container.Bind<PlayerAttacker>().FromInstance(_attacker).AsSingle();
        Container.Bind<PlayerMover>().FromInstance(_mover).AsSingle();
        Container.Bind<PlayerRotator>().FromInstance(_rotator).AsSingle();
        Container.Bind<AttackZone>().FromInstance(_attackZone).AsSingle();
    }
}
