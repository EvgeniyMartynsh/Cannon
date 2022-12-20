using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRange : MonoBehaviour
{
    float circleDiametr = Cannon.FireRange * 10;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = new Vector2(circleDiametr, circleDiametr);
    }
}
