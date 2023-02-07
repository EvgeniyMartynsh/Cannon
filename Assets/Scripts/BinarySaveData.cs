using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


public class BinarySaveData
{

    [SerializeField]  public GameData gameData;

    public void Save(GameData gameData)
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.OpenOrCreate);

        GameData data = new GameData();
        data = gameData;

        bf.Serialize(file, data);

        file.Close();
        Debug.Log("settings saved!");
    }

    public void SaveFromScriptableobject()
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Create);

        GameData data = new GameData();
        var _playerInfo = Resources.Load<PlayerData>("playerinfo");

        data = _playerInfo._gameData;

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("binary saved from SO!");
    }

    public GameData Load()
    {
        GameData data = new GameData();

        if (File.Exists(Application.persistentDataPath + "/gameData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Open);
            data = (GameData)bf.Deserialize(file);
            file.Close();
            Debug.Log("load from binary!");

        }
        return data;
    }



}
