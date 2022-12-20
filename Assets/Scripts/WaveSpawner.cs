using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Waves[] _waves;
    private int _currentEnemyIndex;
    private int _currentWaveIndex;
    private int _enemiesLeftToSpawn;

    private void Start()
    {
        _enemiesLeftToSpawn = _waves[0].WaveSettings.Length;
        StartCoroutine(SpawnEnemyInWave());
    }

    private IEnumerator SpawnEnemyInWave()
    {
        if (_enemiesLeftToSpawn > 0)
        {
            yield return new WaitForSeconds(_waves[_currentWaveIndex]
                .WaveSettings[_currentEnemyIndex]
                .SpawnDelay);

            GameObject newEnemy = Instantiate(
            _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].Enemy,
            //_waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].NeededSpawner.transform.position,
            RandomSpawnPos(),
            Quaternion.identity) ;

            newEnemy.GetComponent<Enemy>().MaxHealth =
                _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].HealthEnemy;

            _enemiesLeftToSpawn--;
            _currentEnemyIndex++;

            StartCoroutine(SpawnEnemyInWave());
        }
        else
        {
            if (_currentWaveIndex < _waves.Length - 1)
            {
                _currentWaveIndex++;
                _enemiesLeftToSpawn = _waves[_currentWaveIndex].WaveSettings.Length;
                _currentEnemyIndex = 0;
                
                StartCoroutine(SpawnEnemyInWave());
            }
        }
    }

    private Vector2 RandomSpawnPos()
    {

        float radius = 5f;
        Vector3 randomPos = Random.insideUnitSphere * radius;
        randomPos += transform.position;
        randomPos.y = 0f;

        Vector3 direction = randomPos - transform.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * radius + transform.position.x;
        randomPos.y = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * radius + transform.position.y;
        randomPos.z = transform.position.z;

        return randomPos;
    }
}

[System.Serializable]
public class Waves
{
    [SerializeField] private WaveSettings[] _waveSettings;
    public WaveSettings[] WaveSettings { get => _waveSettings; }
}

[System.Serializable]
public class WaveSettings
{
    [SerializeField] private GameObject _enemy;
    public GameObject Enemy { get => _enemy; }
    
    [SerializeField] private GameObject _neededSpawner;
    public GameObject NeededSpawner { get => _neededSpawner; }
    
    [SerializeField] private float _spawnDelay;
    public float SpawnDelay { get => _spawnDelay; }

    [SerializeField] private int _healthEnemy;
    public int HealthEnemy { get => _healthEnemy;}
}
