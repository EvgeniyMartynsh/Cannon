using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static WavesParser;

public class WavesSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;
    List<Wave> _waves = new List<Wave>();

    WavesSaveSettings _wavesSaveSettings;


    private int _currentEnemyIndex;
    private int _currentWaveIndex;
    private int _enemiesLeftToSpawn;


    void Start()
    {
        //var _levelsInfo = AssetDatabase.LoadAssetAtPath<LevelsData>("assets/resources/levelsinfo.asset");
        var _levelsInfo = Resources.Load<LevelsData>("levelsinfo");
        _waves = _levelsInfo._wavesList;
        
        StartCoroutine(SpawnEnemy());
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

    private IEnumerator SpawnEnemy()
    {
        int count = 0;

        for (int i = 0; i < _waves.Count; i++)
        {
            for (int j = 0; j < _waves[i].enemyInWaveArray.Length; j++)
            {
                yield return new WaitForSeconds(
                _waves[i].enemyInWaveArray[j]._spawnDelay);

                GameObject newEnemy = Instantiate(
                ParsPrefab(i, j),
                RandomSpawnPos(),
                Quaternion.identity);

                newEnemy.GetComponent<Enemy>().MaxHealth =
                    _waves[i].enemyInWaveArray[j]._healthEnemy;

                Debug.Log(i + " " + j);
                count++;
            }
        }
        //if (count < 4)
        //{
        //    //StartCoroutine(SpawnEnemy());
        //}


    }

    private GameObject ParsPrefab(int i, int j) //, int i
    {
        string prefabName = _waves[i].enemyInWaveArray[j]._enemyType.ToString();
        GameObject prefabToInstatiate = enemyPrefabs.Where(x => x.name == prefabName).SingleOrDefault();

        return prefabToInstatiate;
    }

    //private Vector2 RandomSpawnPos()
    //{

    //    float radius = 5f;
    //    Vector3 randomPos = Random.insideUnitSphere * radius;
    //    randomPos += transform.position;
    //    randomPos.y = 0f;

    //    Vector3 direction = randomPos - transform.position;
    //    direction.Normalize();

    //    float dotProduct = Vector3.Dot(transform.forward, direction);
    //    float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

    //    randomPos.x = Mathf.Cos(dotProductAngle) * radius + transform.position.x;
    //    randomPos.y = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * radius + transform.position.y;
    //    randomPos.z = transform.position.z;

    //    return randomPos;
    //}

    //private IEnumerator SpawnEnemy()
    //{
    //    if (_enemiesLeftToSpawn > 0)
    //    {
    //        yield return new WaitForSeconds(1); //_waves[0][0]._spawnDelay

    //        GameObject newEnemy = Instantiate(
    //        ParsPrefab(),
    //        RandomSpawnPos(),
    //        Quaternion.identity);

    //        newEnemy.GetComponent<Enemy>().MaxHealth =
    //            _waves[0][0]._healthEnemy;

    //        _enemiesLeftToSpawn--;
    //        _currentEnemyIndex++;

    //        StartCoroutine(SpawnEnemy());
    //    }
    //    else
    //    {
    //        if (_currentWaveIndex < _waves.Count - 1)
    //        {
    //            _currentWaveIndex++;
    //            _enemiesLeftToSpawn = _waves[0].Length;
    //            _currentEnemyIndex = 0;

    //            StartCoroutine(SpawnEnemy());
    //        }
    //    }
    //}

    //private GameObject ParsPrefab() //, int i
    //{
    //    string prefabName = _waves[0][0]._enemyType.ToString();
    //    GameObject prefabToInstatiate = enemyPrefabs.Where(x => x.name == prefabName).SingleOrDefault();

    //    return prefabToInstatiate;
    //}
}


