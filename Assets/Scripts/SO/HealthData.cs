using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Parser;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HealthData : ScriptableObject
{

    [SerializeField]
    public List<DictionaryElement> dictionaryElements;


#if UNITY_EDITOR
    [MenuItem("Assets/Create/HealthDicInfo", false, 1)]
    public static void CreateHealthDicInfo()
    {
        HealthData data = ScriptableObject.CreateInstance<HealthData>();
        Parser wavesParser = new Parser();

        data.dictionaryElements = wavesParser.healthDicList;
        AssetDatabase.CreateAsset(data, "Assets/Resources/HealthDicInfo.asset");
        AssetDatabase.Refresh();
    }
#endif

}
