using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FeatureManager : ScriptableObject
{
    public Vector3 v3_PrefabInitPosition = new Vector3(2f , -5f , 0f);
    public GameObject m_ObjCollisionItem { get; set; }
    public abstract void Fn_InitObject();
    public abstract void Fn_GetThrow();
    public abstract void Fn_ExecuteFeature();
}
