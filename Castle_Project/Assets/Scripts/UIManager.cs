using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance
    {
        get { return _Instance; }
    }
    private static UIManager _Instance;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _Instance = this;
    }
}
