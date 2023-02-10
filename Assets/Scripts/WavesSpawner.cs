using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static Parser;

public class WavesSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;
    List<Wave> _waves = new List<Wave>();
    GameManager gameManager;



    void Start()
    {
        Debug.Log("WaveSpawner start");

        var _levelsInfo = Resources.Load<LevelsData>("levelsinfo");
        _waves = _levelsInfo._wavesList;
        gameManager = GameManager.instance;

        StartCoroutine(SpawnEnemy());
    }

    private Vector2 RandomSpawnPos()
    {
        float radius = 5.5f;
        Vector3 randomPos = Random.insideUnitSphere * radius;
        randomPos.y = 0f;

        Vector3 direction = randomPos - transform.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * radius + transform.position.x;
        randomPos.y = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * radius;
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
                yield return new WaitForSeconds(_waves[i].enemyInWaveArray[j]._spawnDelay);

                if (!gameManager.IsGameOver)
                {
                    GameObject newEnemy = Instantiate(
                            ParsPrefab(i, j),
                                RandomSpawnPos(),
                                    Quaternion.identity);

                    newEnemy.GetComponent<Enemy>().MaxHealth = _waves[i].enemyInWaveArray[j]._healthEnemy;
                    newEnemy.GetComponent<Enemy>().Speed = _waves[i].enemyInWaveArray[j]._speed;
                    newEnemy.GetComponent<Enemy>().Coins = _waves[i].enemyInWaveArray[j]._coins;
                    newEnemy.GetComponent<Enemy>().ExtraCoins = _waves[i].enemyInWaveArray[j]._extraCoins;
                    newEnemy.GetComponent<Enemy>().Damage = _waves[i].enemyInWaveArray[j]._damage;

                    count++;
                }

            }
        }


    }

    private GameObject ParsPrefab(int i, int j) //, int i
    {
        string prefabName = _waves[i].enemyInWaveArray[j]._enemyType.ToString();
        GameObject prefabToInstatiate = enemyPrefabs.Where(x => x.name == prefabName).SingleOrDefault();

        return prefabToInstatiate;
    }

}


