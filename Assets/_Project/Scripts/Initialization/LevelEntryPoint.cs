using UnityEngine;
using Zenject;

public class LevelEntryPoint : MonoBehaviour
{
    [Header("Zenject")]
    [SerializeField] private SceneContext _sceneContext;

    [Header("Survivors")]
    [SerializeField] private Survivor _survivorPrefab;
    [SerializeField] private Transform[] _survivorsSpawnPoints;
    [SerializeField] private Transform[] _safePlaces;

    private MapInformation _mapInformation;

    private void Start()
    {
        _mapInformation = new();
        _sceneContext.Run();

        Initialize();
    }

    private void Initialize()
    {
        foreach (var spawnPoint in _survivorsSpawnPoints)
        {
            var newSurvivor = Instantiate(_survivorPrefab, spawnPoint.position, Quaternion.identity);
            newSurvivor.Initialize(_safePlaces);
        }
    }
}
