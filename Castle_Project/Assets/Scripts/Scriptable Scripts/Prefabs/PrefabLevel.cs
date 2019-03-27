using UnityEngine;

/// <summary>
/// 關卡物件
/// </summary>
[System.Serializable]
public class PrefabLevel
{
    public string tag;
    public int level;
    public ObjectSetting[] prefab;
    
}

/// <summary>
/// 物件資訊
/// </summary>
[System.Serializable]
public class ObjectSetting
{
    public string name;
    public GameObject prefab;
    public float realtimeStartup;
    public float waitTime;
}
