﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidModel
{
    int StartHP { get; set; } = 3;
    public int CurrentHP { get; set; }

    public Vector2 Position { get; set; }
    public float RotationAngle { get; set; }
    public int Speed { get; private set; } = 2;

    public AsteroidModel()
    {
        CurrentHP = StartHP;
    }
}