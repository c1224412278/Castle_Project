using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FeatureManager : ScriptableObject
{
    public abstract void Fn_InitObject();
    public abstract void Fn_ExecuteFeature();
}
