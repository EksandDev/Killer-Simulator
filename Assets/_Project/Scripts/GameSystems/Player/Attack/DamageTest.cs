using UnityEngine;

public class DamageTest : MonoBehaviour, IDamageable
{
    private int _currentHealth = 20;
    private int _maxHealth = 20;

    public int CurrentHealth 
    { 
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            Debug.Log($"Health: {_currentHealth}");

            if (_currentHealth <= 0)
                gameObject.SetActive(false);
        }
    }

    public int MaxHealth => _maxHealth;
    public bool IsActive => gameObject.activeInHierarchy;
    public Transform CurrentTransform => transform;
    public Vector3 CurrentPosition => transform.position;
}