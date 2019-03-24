using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data" , menuName = "ScriptableObject/Player Data")]
public class PlayerScriptable : ScriptableObject
{
    public int m_iMaxhp;
    public float m_fMaxTime;
    public float m_fUpStrSpeed;
}
