using Newtonsoft.Json.Linq;
using System;
using Unity.VisualScripting;
using UnityEngine;



public class Cannon : MonoBehaviour
{

    string enemyTag = "Enemy";

    Transform nearestTarget = null;
    //Vector3 directionTurretToTarget;
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

    Rigidbody2D rb;


    private void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    private void Update()
    {
        FindNearestTarget();

    }

    private void FixedUpdate()
    {
        TurretRotation();
    }

    private void FindNearestTarget()
    {
        if (!GameManager.IsGameOver)
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

                    if (distanceToNearestTarget <= GameManager.FireRange)
                    {
                        isDistanceToNearestTargenInFireRange = true;
                    }
                }
            }
        }
    }

    private void TurretRotation()
    {

        if (isTargenOnField && nearestTarget != null && !GameManager.IsGameOver)
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


    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, GameManager.FireRange);
    }


    void Shoot()
    {
        IsReadyToShot();

        if (isReadyToShot)
        {
            Instantiate(projectilePrefab, projectileSpawnPosition.transform.position, projectileSpawnPosition.transform.rotation);
            startTime = Time.time;

            return;
        }

        else { return; }
    }
    void IsReadyToShot()
    {
        int targetName = TargetNameParse();

        if (targetName != countEnemy && isDistanceToNearestTargenInFireRange)
        {
            isReadyToShot = true;
            countEnemy = targetName;
        }
        else if (targetName == countEnemy && isDistanceToNearestTargenInFireRange)
        {
            isReadyToShot = (Time.time - startTime) > ShotDelay;
        }
    }

    int TargetNameParse()
    {
        int number;
        bool success = int.TryParse(nearestTarget.name, out number);

        if (success)
        {
            return number;
        }
        else
        {
            Debug.Log("convert string to int failed: " + " " + nearestTarget.name);
            return 0;
        }
    }


}



