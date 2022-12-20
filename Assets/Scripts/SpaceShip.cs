using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : Enemy
{
    protected override void Awake()
    {
        Speed = 1f;
        Coins = 1;
        Damage = 10;

        base.Awake();
    }
}
