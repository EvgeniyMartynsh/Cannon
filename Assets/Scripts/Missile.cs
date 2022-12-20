using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Enemy
{
    protected override void Awake()
    {
        Speed = 1.5f;
        Coins = 2;
        Damage = 15;

        base.Awake();
    }
}
