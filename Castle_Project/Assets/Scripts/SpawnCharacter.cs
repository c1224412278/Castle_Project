using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SpawnEvent : UnityEvent<Transform> { }

public class SpawnCharacter : MonoBehaviour
{
    public Transform m_target;
    public string m_spawnTag;
    public GameObject m_prefab;
    public int m_maxSize = 5;
    public float m_waitTime = 3;
    public int m_spawnCounter = 0;
    public CharacterList m_characsList;

    [SerializeField, Space(order =16)]
    private SpawnEvent OnSpawnEvent;
    private Transform m_transform;

    void Start()
    {
        m_transform = transform;
        m_characsList = GetComponent<CharacterList>();
        StartCoroutine(StartSpawn());
    }

    public IEnumerator StartSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_waitTime);

            if(m_spawnCounter < m_maxSize)
            {
                GameObject spawnObj = Instantiate(m_prefab, m_transform.position, m_transform.rotation);
                spawnObj.GetComponent<ISpawnObject>().OnSpawnObjcet(this);

                if (m_spawnTag != "") spawnObj.tag = m_spawnTag;
                OnSpawnEvent.Invoke(spawnObj.transform);

                m_spawnCounter++;
            }

            yield return null;
        }
    }

}
