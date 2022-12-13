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

    public static float RotationSpeed { get; set; } = 0.01f;
    public static float FireRange { get; set; } = 2.5f;
    public static bool isDistanceToNearestTargenInFireRange { get; set; } = false;

    bool isTargenOnField = false;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject projectileSpawnPosition;

    float startTime;
    float shotDelay = 10f;
    bool isReadyToShot;
    int countEnemy = 0;

    Rigidbody2D rb;


    private void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FindNearestTarget();
        TurretRotation();
    }

    private void FindNearestTarget()
    {
        if (GameManager.IsGameActive)
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

                    if (distanceToNearestTarget <= FireRange)
                    {
                        isDistanceToNearestTargenInFireRange = true;
                    }
                }
            }
        }
    }

    private void TurretRotation()
    {

        if (isTargenOnField && nearestTarget != null && GameManager.IsGameActive)
        {
            Debug.Log("isTargenOnField" + isTargenOnField);
            Debug.Log("GameManager.IsGameActive" + GameManager.IsGameActive);
            Debug.Log("nearestTarget" + nearestTarget);

            ////directionTurretToTarget = nearestTarget.position - transform.position; //вектор направления к ближайшей цели
            ////directionTurretToTarget.z = 0;

            //directionTurretToTarget_2d = nearestTarget.GetComponent<Rigidbody2D>().transform.position - rb.transform.position;

            //Quaternion rotateQuaternion = Quaternion.LookRotation(directionTurretToTarget_2d); // кватернион  на цель

            //float angle = Quaternion.Angle(gameObject.transform.localRotation, rotateQuaternion); // угол на цель, нужен для скорости поворота

            //gameObject.transform.localRotation = 
            //    Quaternion.Slerp(
            //        gameObject.transform.localRotation,
            //        rotateQuaternion,
            //        Mathf.Min(1f, Time.deltaTime * RotationSpeed / angle)
            //        );
            ////сферически интерполированный квартерион между текущим положением и кватернионом направления на цель

            float rotationModifier = 1f;
         
            directionTurretToTarget_2d = nearestTarget.GetComponent<Rigidbody2D>().transform.position - rb.transform.position;
            float angle = Mathf.Atan2(directionTurretToTarget_2d.y, directionTurretToTarget_2d.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, RotationSpeed);
            
            float angle2 = Quaternion.Angle(rb.transform.localRotation, q);

            Debug.Log("angle " + angle);
            Debug.Log("angle " + angle2);

            if (angle2 < 1)
            {
                Debug.Log(angle);
                Shoot();
            }
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, FireRange);
    }


    void Shoot()
    {
        Debug.Log("ready to shoot");

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
        Debug.Log("targetname " + targetName);

        if (targetName != countEnemy && isDistanceToNearestTargenInFireRange)
        {
            isReadyToShot = true;
            countEnemy = targetName;
        }
        else if (targetName == countEnemy && isDistanceToNearestTargenInFireRange)
        {
            isReadyToShot = (Time.time - startTime) > shotDelay;
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



