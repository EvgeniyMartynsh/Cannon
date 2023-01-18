using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif


public class PlayerData : ScriptableObject
{
    [SerializeField]
    public GameData _gameData;




#if UNITY_EDITOR
    [MenuItem("Assets/Create/PlayerInfo", false, 1)]

    public static void CreatePlayerInfo()
    {
        PlayerData data = ScriptableObject.CreateInstance<PlayerData>();
        Parser wavesParser = new Parser();

        data._gameData = wavesParser.gameData;
        AssetDatabase.CreateAsset(data, "Assets/Resources/PlayerInfo.asset");
        AssetDatabase.Refresh();
    }
#endif 
}
