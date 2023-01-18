using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Parser;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelsData : ScriptableObject
{


    [SerializeField]
    public List<Wave> _wavesList;

    



#if UNITY_EDITOR
    [MenuItem("Assets/Create/LevelsInfo", false, 1)]
    public static void CreateLevelsInfo()
    {
        LevelsData data = ScriptableObject.CreateInstance<LevelsData>();
        Parser wavesParser = new Parser();

        data._wavesList = wavesParser.wavesList;
        AssetDatabase.CreateAsset(data, "Assets/Resources/LevelsInfo.asset");
        AssetDatabase.Refresh();
    }
#endif
}
