using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public void SetHealth()
    {
        GameManager.GameHealth += 10;
    }
}
