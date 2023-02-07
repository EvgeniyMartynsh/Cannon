using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif
public class HealthCostData : ScriptableObject
{
    [SerializeField]
    public List<DictionaryElement> dictionaryElements;


#if UNITY_EDITOR
    [MenuItem("Assets/Create/HealthCostDicInfo", false, 1)]
    public static void CreateHealthDicInfo()
    {
        HealthCostData data = ScriptableObject.CreateInstance<HealthCostData>();
        Parser wavesParser = new Parser();

        data.dictionaryElements = wavesParser.healthCostDicList;
        AssetDatabase.CreateAsset(data, "Assets/Resources/HealthCostDicInfo.asset");
        AssetDatabase.Refresh();
    }
#endif
}
