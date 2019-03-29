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

        if (!GameData.m_IsPlayingGame)
            GameData.m_IsPlayingGame = true;

        m_transform = this.transform;
        thePlayerData = new PlayerData(m_ScriptableDataObject.m_iMaxhp , m_ScriptableDataObject.m_fMaxTime , m_ScriptableDataObject.m_fUpStrSpeed);
        UIController.Instance.Fn_SetGameTime(m_ScriptableDataObject.m_fMaxTime);            //設定遊戲關卡遊玩時間
        StartCoroutine(Fn_SetUiBlood());
    }
    private void Update()
    {
        if (del_Execute != null && GameData.m_IsPlayingGame)
            del_Execute();
    }
    private IEnumerator Fn_SetUiBlood()
    {
        while (UIController.Instance.Img_HpSprite.fillAmount > 0f)
        {
            float value = Mathf.InverseLerp(0 , thePlayerData.m_iMaxHp , thePlayerData.m_iCurHp);
            UIController.Instance.Fn_UpdateAmount(UIController.Instance.Img_HpSprite , value , 2.5f);
            yield return new WaitForEndOfFrame();
        }
    }
}
[SerializeField]
public class PlayerData
{
    public int m_iMaxHp;
    public int m_iCurHp;
    public float m_fGameTime;
    public float m_fCurStr { get; set; }
    public float m_fMaxStr { get; set; }
    public float m_fUpStrSpeed;
    public Tween tween_valueController;

    public PlayerData(int m_iMaxHp, float m_GameTime , float m_fUpStrSpeed)
    {
        m_fCurStr = 0f;
        m_fMaxStr = 1f;

        this.m_iMaxHp = m_iMaxHp;
        this.m_iCurHp = m_iMaxHp;
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