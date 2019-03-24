using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour
{
    #region 單例模式
    public static SceneObject Instance { get { return _Instance; } }
    private static SceneObject _Instance;
    #endregion

    public GameObject m_ObjLand;

    private void Awake()
    {
        _Instance = this;
    }
}
