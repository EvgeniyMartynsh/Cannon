using Newtonsoft.Json.Linq;
using System;
using Unity.VisualScripting;
using UnityEngine;



public class Cannon : MonoBehaviour
{

    string enemyTag = "Enemy";

    Transform nearestTarget = null;
    Vector2 directionTurretToTarget_2d;

    public static float RotationSpeed { get; set; } = 0.1f;
    public static int Damage { get; set; } = 10;
    public float ShotDelay { get; set; } = 0.5f;
    public static bool isDistanceToNearestTargenInFireRange { get; set; } = false;

    bool isTargenOnField = false;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject projectileSpawnPosition;
    GameManager gameManager;

    float startTime;
    bool isReadyToShot;
    int countEnemy = 0;
    float shotTimer = 0;


    Rigidbody2D rb;


    private void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameManager.instance;

    }

    private void Update()
    {
        FindNearestTarget();
        shotTimer += Time.deltaTime;

    }

    private void FixedUpdate()
    {
        TurretRotation();
    }

    private void FindNearestTarget()
    {
        if (!gameManager.IsGameOver)
        {
            float distanceToNearestTarget = float.MaxValue;
            GameObject[] targetArray = GameObject.FindGameObjectsWithTag(enemyTag);
            foreach (GameObject item in targetArray)
            {
                float distance = Vector2.Distance(item.transform.position, transform.position);

                if (distance < distanceToNearestTarget)
                {
                    nearestTarget = item.transform;
                    distanceToNearestTarget = distance;
                    isTargenOnField = true;

                    if (distanceToNearestTarget <= gameManager.FireRange)
                    {
                        isDistanceToNearestTargenInFireRange = true;
                    }
                }
            }
        }
    }

    private void TurretRotation()
    {

        if (isTargenOnField && nearestTarget != null && !gameManager.IsGameOver)
        {
            float rotationModifier = 1f;
         
            directionTurretToTarget_2d = nearestTarget.GetComponent<Rigidbody2D>().transform.position - rb.transform.position;
            
            float angle = Mathf.Atan2(directionTurretToTarget_2d.y, directionTurretToTarget_2d.x) * Mathf.Rad2Deg - rotationModifier;
            
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, RotationSpeed);
            
            float angle2 = Quaternion.Angle(rb.transform.localRotation, q);

            if (angle2 < 1)
            {
                Shoot();
            }
        }
    }


    void Shoot()
    {
        if (shotTimer >= ShotDelay && isDistanceToNearestTargenInFireRange)
        {
            Instantiate(projectilePrefab, projectileSpawnPosition.transform.position, projectileSpawnPosition.transform.rotation);
            shotTimer = 0;
        }

        else { return; }
    }

}



