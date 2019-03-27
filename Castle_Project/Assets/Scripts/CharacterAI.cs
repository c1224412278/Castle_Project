using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 角色AI行為動作
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class CharacterAI : MonoBehaviour, ISpawnObject, IDamageReceiver
{
    public Transform m_currTarget;

    public int m_life = 3;
    public int m_attackDamage = 1;
    public float m_rotSpeed = 20f;
    public float m_speed = .5f;
    public float m_waitTime = 1f;

    [SerializeField]
    private CharacterAnima m_anima;
    [SerializeField]
    private NavMeshAgent m_agent;
    [SerializeField]
    private Transform m_transform;

    // 產生角色的生產者
    private SpawnCharacter m_spawner;
    // 所有敵人角色的清單
    private CharacterList m_enemys;

    void Start()
    {
        m_agent.speed = m_speed;
    }

    /// <summary>
    /// 當產生時初始化相關資訊
    /// </summary>
    /// <param name="spawnCharacter">產生者</param>
    public void OnSpawnObjcet(SpawnCharacter spawnCharacter)
    {
        m_spawner = spawnCharacter;
        m_currTarget = m_spawner.m_target;
        m_enemys = m_spawner.m_characsList;
        StartCoroutine(AI());
    }

    /// <summary>
    /// 設定給角色的傷害
    /// </summary>
    /// <param name="damage">傷害值</param>
    /// <param name="sender">誰給我傷害</param>
    public void SetDamage(int damage, CharacterAI sender)
    {
        m_life -= damage;

        // 如果角色死亡
        if (m_life <= 0)
        {
            m_anima.SetAnimationBool("Death", true);
            
            // 從勝利方取的角色清單，將自己從清單移除 
            sender.m_enemys.RemoveCharacterTransform(m_transform);      
        }
            
    }

    private IEnumerator AI()
    {
        while (true)
        {
            // 待機狀態
            if (m_anima.CompareCurrentAniamtion("Idle"))
            {
                m_anima.SetAnimationBool("Idle", false);

                if (m_waitTime > 0)
                {
                    yield return new WaitForSeconds(m_waitTime);
                    m_currTarget = FindAttackTarget();
                    m_agent.SetDestination(m_currTarget.position);
                    m_waitTime = 0;
                }
                
                if(m_currTarget != null)
                    if (Vector3.Distance(m_transform.position, m_currTarget.position) < .5f)
                    {
                        m_anima.SetAnimationBool("Run", false);
                        m_agent.isStopped = true;
                        m_anima.SetAnimationBool("Attack", true);
                    }
                    else
                    {
                        m_waitTime = 1;
                        m_agent.SetDestination(m_currTarget.position);
                        m_anima.SetAnimationBool("Run", true);
                        m_agent.isStopped = false;
                    }


            }

            // 移動狀態
            if (m_anima.CompareCurrentAniamtion("Run"))
            {
                m_waitTime -= Time.deltaTime;
                if (m_waitTime <= 0f)
                {
                    m_currTarget = FindAttackTarget();
                    m_agent.SetDestination(m_currTarget.position);
                    m_waitTime = 1f;
                }

                if (m_currTarget != null)
                    if (Vector3.Distance(m_transform.position, m_currTarget.position) <= .5f)
                    {
                        m_anima.SetAnimationBool("Run", false);
                        m_agent.isStopped = true;
                        m_anima.SetAnimationBool("Attack", true);
                    }
            }

            // 攻擊狀態
            if (m_anima.CompareCurrentAniamtion("Attack"))
            {
                RotateTo();
                
                if (m_anima.GetAnimationBool("Attack"))
                {
                    m_anima.SetAnimationBool("Attack", false);

                    // 如果目標有接收傷害腳本，就給傷害值
                    IDamageReceiver targetReceiver = m_currTarget.GetComponent<IDamageReceiver>();
                    if(targetReceiver != null)
                        m_currTarget.GetComponent<IDamageReceiver>().SetDamage(m_attackDamage, this);

                    m_anima.SetAnimationBool("Idle", true);
                    m_waitTime = 2f;
                }
            }

            // 死亡狀態
            if (m_anima.CompareCurrentAniamtion("Death"))
            {
                if (m_anima.GetNormalizedTime() > 1.0f)
                {
                    m_spawner.m_spawnCounter--;
                    Destroy(gameObject);
                }
            }

            yield return null;
        }
    }

    /// <summary>
    /// 尋找攻擊目標，如果沒發現攻擊目標就打敵人城堡
    /// </summary>
    private Transform FindAttackTarget()
    {
        Transform target = m_enemys.GetCrosestTarget(m_transform.position);

        if (target != null) return target;
        else return m_spawner.m_target;

    }

    /// <summary>
    /// 旋轉面對目標
    /// </summary>
    private void RotateTo()
    {
        if (m_currTarget == null) return;

        Vector3 dir = ConvertYRotVector(m_currTarget.position) - ConvertYRotVector(m_transform.position);
        Vector3 newDir = Vector3.RotateTowards(m_transform.forward, dir, m_rotSpeed * Time.deltaTime, 0.0f);
        m_transform.rotation = Quaternion.LookRotation(newDir);

    }

    /// <summary>
    /// 轉換成XZ向量
    /// </summary>
    private Vector3 ConvertYRotVector(Vector3 pos)
    {
        return new Vector3(pos.x, 0, pos.z);
    }

}
