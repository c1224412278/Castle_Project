using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PrefabData", menuName = "Prefab Data/Create Data")]
public class PrefabData : PrefabEvent
{

    public PrefabLevel[] prefabs;

    /// <summary>
    /// 取得關卡物件
    /// </summary>
    /// <param name="level">關卡</param>
    /// <returns></returns>
    public override PrefabLevel GetPrefabData(int level)
    {
        foreach (var prefab in prefabs)
        {
            if (prefab.level == level)
                return prefab;
        }

        return null;
    }
}
