using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Enemy
{
    protected override void Awake()
    {
        Speed = 0.5f;
        Coins = 3;
        Damage = 25;
        
        base.Awake();
    }
}

