using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRange : MonoBehaviour
{
    float circleDiametr;


    void Update()
    {
        circleDiametr = 10 * GameManager.FireRange;
        gameObject.transform.localScale = new Vector2(circleDiametr, circleDiametr);
        
    }
}
