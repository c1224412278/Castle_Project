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
    public GameObject m_ObjCastle;
    private void Awake()
    {
        _Instance = this;
    }
}
