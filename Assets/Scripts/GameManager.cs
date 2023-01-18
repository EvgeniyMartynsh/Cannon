using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using static Parser;

public class GameManager : MonoBehaviour
{

    //public static GameManager instance = null;

    static int playerHealth;
    static float fireRange;
    static int startScore;
    static int extraCoins;


    public static int StartHealth { get => playerHealth; set { playerHealth = value; } }
    public static int GameHealth { get; set; }
    public static float FireRange { get => fireRange; set { fireRange = value; } }
    public static int GameScore { get; set; }
    public static int StartScore { get => startScore; set { startScore = value; } }
    public static int ExtraCoins { get => extraCoins; set { extraCoins = value; } }
    public static bool IsGameOver { get; set; }
    public static int EnemyCount { get; set; } = 1;



    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI extraCoinsText;
    [SerializeField] TextMeshProUGUI fireRangeText;
    [SerializeField] TextMeshProUGUI rotationSpeedText;
    [SerializeField] TextMeshProUGUI bulletSpeedText;
    [SerializeField] TextMeshProUGUI healthPlayerText;

    private void Awake()
    {
        //    if (instance == null)
        //        instance = this;
        //    else if (instance != this)
        //        Destroy(gameObject);

        //    DontDestroyOnLoad(gameObject);

        //if (!File.Exists(Application.persistentDataPath + "/gameData.dat"))
        //{
        //    BinarySaveData binaryData = new BinarySaveData();
        //    binaryData.SaveFromScriptableobject();
        //    Debug.Log("SaveFromSO - ok");
        //}
        //if (File.Exists(Application.persistentDataPath + "/gameData.dat"))
        //{
        //    Debug.Log("File Found");

        //}


    }


    void Update()
    {
        UpdateUILayer();
        GameOver();
    }

    public static void Initialisation()
    {
        IsGameOver = false;

        //var _playerInfo = Resources.Load<PlayerData>("playerinfo");
        BinarySaveData binarySave = new BinarySaveData();
        GameData data = new GameData();
        data = binarySave.Load();


        StartHealth = data.initPlayerHealth;
        FireRange = data.fireRange;
        StartScore = data.startScore;
        ExtraCoins = data.extraCoins;
        GameScore = StartScore;
        GameHealth = StartHealth;

        Debug.Log(data.initPlayerHealth);
        Debug.Log(ExtraCoins);
    }

    void GameOver()
    {
        if (GameHealth <= 0)
        {
            IsGameOver = true;
        }
    }

    void UpdateUILayer()
    {
        scoreText.text = "$ " + GameScore;
        extraCoinsText.text = "$ extra " + ExtraCoins;
        fireRangeText.text = "Range " + FireRange;
        rotationSpeedText.text = "Rotation speed:  " + Cannon.RotationSpeed;
        bulletSpeedText.text = "Bullet speed: " + Projectile.Speed;
        healthPlayerText.text = "Health: " + GameHealth;

    }



}
