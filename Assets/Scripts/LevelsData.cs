using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WavesParser;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelsData : ScriptableObject
{


    [SerializeField]
    public List<Wave> _wavesList;

    



#if UNITY_EDITOR
    [MenuItem("Assets/Create/LevelsInfo", false, 1)]
    public static void CreateImageData()
    {
        LevelsData data = ScriptableObject.CreateInstance<LevelsData>();
        WavesParser wavesParser = new WavesParser();

        data._wavesList = wavesParser.wavesList;
        AssetDatabase.CreateAsset(data, "Assets/Resources/LevelsInfo.asset");
        AssetDatabase.Refresh();
    }
#endif
}
