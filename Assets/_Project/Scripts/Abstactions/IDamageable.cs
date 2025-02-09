﻿using UnityEngine;

public interface IDamageable
{
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; }
    public bool IsActive { get; }
    public Transform CurrentTransform { get; }
    public Vector3 CurrentPosition { get; }
}