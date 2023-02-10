using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRange : MonoBehaviour
{
    float circleDiametr;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }
    void Update()
    {

        circleDiametr = 10 * gameManager.FireRange;
        gameObject.transform.localScale = new Vector2(circleDiametr, circleDiametr);
        
    }
}
