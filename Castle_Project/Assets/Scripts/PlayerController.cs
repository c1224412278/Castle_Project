using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public delegate void _del_Execute();

public class PlayerController : MonoBehaviour
{
    public static PlayerData thePlayerData { get; set; }
    public static Transform m_transform;
    public static _del_Execute del_Execute;
    
    [SerializeField] private PlayerScriptable m_ScriptableDataObject;
    private void Start()
    {
        if (m_ScriptableDataObject == null)
            return;

        m_transform = this.transform;
        thePlayerData = new PlayerData(m_ScriptableDataObject.m_iMaxhp , m_ScriptableDataObject.m_fMaxTime , m_ScriptableDataObject.m_fUpStrSpeed);
        UIController.Instance.Fn_Init();
    }
    private void Update()
    {
        if (del_Execute != null)
            del_Execute();
    }
}
[SerializeField]
public class PlayerData
{
    public int m_iHp;
    public float m_fGameTime;
    public float m_fCurStr { get; set; }
    public float m_fMaxStr { get; set; }
    public float m_fUpStrSpeed;
    public Tween tween_valueController;

    public PlayerData(int m_iHp , float m_GameTime , float m_fUpStrSpeed)
    {
        m_fCurStr = 0f;
        m_fMaxStr = 1f;

        this.m_iHp = m_iHp;
        this.m_fGameTime = m_GameTime;
        this.m_fUpStrSpeed = m_fUpStrSpeed;
    }
    public void Fn_SetValueTween(bool isGetTween)
    {
        if (isGetTween)
        {
            m_fCurStr = 0f;
            m_fMaxStr = 1f;

            if (tween_valueController == null)
                tween_valueController = DOTween.To(() => m_fCurStr, x => m_fCurStr = x, m_fMaxStr, m_fUpStrSpeed);
        }
        else
        {
            tween_valueController.Kill();
            tween_valueController = null;
        }
    }
}