using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrefabEvent : ScriptableObject
{
    public abstract PrefabLevel GetPrefabData(int level);
}
