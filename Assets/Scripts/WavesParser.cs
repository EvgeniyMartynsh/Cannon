using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using UnityEngine;




public class WavesParser
{
    IList<IList<object>> google;

    int _wavesCount;

    [SerializeField]


    public List<Wave> wavesList = new List<Wave>();

    [System.Serializable]
    public class Wave
    {
        public EnemyInWave[] enemyInWaveArray;
    }

    [System.Serializable]
    public class EnemyInWave
    {
        public string _enemyType;

        public float _spawnDelay;
        public float _speed;

        public int _healthEnemy;
        public int _coins;
        public int _extraCoins;
        public int _damage;
    }


    public WavesParser()
    {

        SheetReader sheetReader = new SheetReader();
        CountAndFillWaves(sheetReader);
    }
    private void CountAndFillWaves(SheetReader sheetReader)
    {
        _wavesCount = WavesCount(sheetReader);

        for (int i = 1; i <= _wavesCount; i++)
            FillEnemyArrayInWaves(sheetReader, i);
    }

    private void FillEnemyArrayInWaves(SheetReader sheetReader, int i)
    {
        google = sheetReader.getSheetRange($"Wave_{i}");
        int linesCount = google.Count - 1; // -1 ������� ���������

        Wave wave = new Wave();
        wave.enemyInWaveArray = new EnemyInWave[linesCount];

        int count = 0;
        for (int k = 1; k < google.Count; k++)
        {
            wave.enemyInWaveArray[count] = new EnemyInWave();

            wave.enemyInWaveArray[count]._enemyType = (string)google[k][0];
            wave.enemyInWaveArray[count]._spawnDelay = Convert.ToSingle(google[k][1]);
            wave.enemyInWaveArray[count]._healthEnemy = Convert.ToInt32(google[k][2]);
            wave.enemyInWaveArray[count]._speed = Convert.ToSingle(google[k][3]);
            wave.enemyInWaveArray[count]._coins = Convert.ToInt32(google[k][4]);
            wave.enemyInWaveArray[count]._extraCoins = Convert.ToInt32(google[k][5]);
            wave.enemyInWaveArray[count]._damage = Convert.ToInt32(google[k][6]);

            count++;
        }

        wavesList.Add(wave);
    }

    private int WavesCount(SheetReader sheetReader)
    {
        int count = 0;
        try
        {
            for (int i = 1; i <= 100; i++)
            {
                if (sheetReader.getSheetRange($"Wave_{i}") != null)
                    count++;
            }
            return count;

        }
        catch (System.Exception)
        {
            return count;
        }
    }

}








