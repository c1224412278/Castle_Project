using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnima : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;
    private AnimatorStateInfo m_info;

    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 設定動畫屬性
    /// </summary>
    public void SetAnimationBool(string name, bool value)
    {
        m_animator.SetBool(name, value);
    }

    /// <summary>
    /// 取的動畫開關
    /// </summary>
    public bool GetAnimationBool(string name)
    {
        return m_animator.GetBool(name);
    }

    /// <summary>
    /// 比較當前的東畫
    /// </summary>
    public bool CompareCurrentAniamtion(string name)
    {
        m_info = m_animator.GetCurrentAnimatorStateInfo(0);
        if (m_info.IsName(name) & !m_animator.IsInTransition(0))
            return true;
        else
            return false;
    }

    /// <summary>
    /// 取得當前的撥放位置
    /// </summary>
    public float GetNormalizedTime()
    {
        m_info = m_animator.GetCurrentAnimatorStateInfo(0);
        return m_info.normalizedTime;
    }

}
