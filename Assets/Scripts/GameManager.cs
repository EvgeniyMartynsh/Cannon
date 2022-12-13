using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] List<GameObject> enemyPrefab;
    public static int PlayerHealth { get; set; } = 100;
    public static int PlayerScore { get; set; }
    public static bool IsGameActive { get; set; } = true;
    public static int EnemyCount { get; set; } = 1;



    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI fireRangeText;
    [SerializeField] TextMeshProUGUI rotationSpeedText;
    [SerializeField] TextMeshProUGUI bulletSpeedText;
    [SerializeField] TextMeshProUGUI healthPlayerText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUILayer();
        GameOver();
    }

    void GameOver()
    {
        if (PlayerHealth <= 0)
        {
            IsGameActive = false;
        }
    }

    void UpdateUILayer()
    {
        scoreText.text = "$ " + PlayerScore;
        fireRangeText.text = "Range " + Cannon.FireRange;
        rotationSpeedText.text = "Rotation speed:  " + Cannon.RotationSpeed;
        bulletSpeedText.text = "Bullet speed: " + Projectile.Speed;
        healthPlayerText.text = "Health: " + PlayerHealth;
    }
    IEnumerator spawnEnemy()
    {
        while (IsGameActive)
        {
            int minTimeToSpawn = 0;
            int maxTimeToSpawn = 1;

            int randomTimeSpawn = Random.Range(minTimeToSpawn, maxTimeToSpawn);
            int enemyIndex = Random.Range(0, 3);

            Instantiate(enemyPrefab[enemyIndex], SpawnRandomEnemyPos(), enemyPrefab[enemyIndex].transform.rotation);
            EnemyCount++;

            yield return new WaitForSeconds(randomTimeSpawn);
        }
    }

    //private Vector3 SpawnRandomEnemyPos()
    //{
    //    float radius = 10f;
    //    Vector3 randomPos = Random.insideUnitSphere * radius;

    //    randomPos += transform.position;
    //    randomPos.y = 0f;

    //    Vector3 direction = randomPos - transform.position;
    //    direction.Normalize();

    //    float dotProduct = Vector3.Dot(transform.forward, direction);

    //    float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

    //    randomPos.x = Mathf.Cos(dotProductAngle) * radius + transform.position.x;
    //    randomPos.z = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * radius + transform.position.z;

    //    return randomPos;
    //}

    private Vector2 SpawnRandomEnemyPos()
    {
        float radius = 5f;
        Vector2 randomPos = Random.insideUnitCircle * radius;

        return randomPos;
    }
}
