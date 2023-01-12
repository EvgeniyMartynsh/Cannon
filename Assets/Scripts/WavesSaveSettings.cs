using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static WavesParser;

public class WavesSaveSettings : MonoBehaviour
{

    //[System.Serializable]
    //public class GameData
    //{
    //    public List<WaveSettings[]> saveWaves;
    //}

    //public void ParsAndSave()
    //{
    //    Save();
    //}

    //public void Save()
    //{
        
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Open(Application.persistentDataPath + "/wavesInfo.dat", FileMode.OpenOrCreate);

    //    GameData data = new GameData();

    //    WavesParser parser = new WavesParser();
    //    data.Waves = parser.WavesList;

    //    bf.Serialize(file, data);
    //    //bf.Serialize(file, GameData.Waves);
    //    file.Close();
    //    Debug.Log("Waves settings saved!");
    //}

    //public List<WaveSettings[]> Load()
    //{
    //    if (File.Exists(Application.persistentDataPath + "/wavesInfo.dat"))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file = File.Open(Application.persistentDataPath + "/wavesInfo.dat", FileMode.Open);

    //        //GameData data = new GameData();
    //        data = (GameData)bf.Deserialize(file);
    //        file.Close();
            
    //        return data.saveWaves;
    //    }
    //    Debug.Log("No file to load");
    //    return null;
        
    //}

}
