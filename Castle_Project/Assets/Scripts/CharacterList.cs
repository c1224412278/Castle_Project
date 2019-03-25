using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterList : MonoBehaviour
{
    [SerializeField]
    private List<Transform> lists;
    private Transform m_tramsform;

    void Start()
    {
        m_tramsform = transform;
    }

    public void RemoveCharacterTransform(Transform m_transform)
    {
        if (lists.Contains(m_transform))
        {
            lists.Remove(m_transform);
        }
    }

    public void AddCharacterTransform(Transform m_transform)
    {
        lists.Add(m_transform);
    }

    public Transform GetCrosestTarget(Vector3 posision)
    {
        Transform saveTransform = null;
        float distance = Vector3.Distance(posision, m_tramsform.position);
        for (int i = 0; i < lists.Count; i++)
        {
            float disTmp = Vector3.Distance(posision, lists[i].position);
            if(distance > disTmp)
            {
                distance = disTmp;
                saveTransform = lists[i];
            }
        }

        return saveTransform;
    }

}
