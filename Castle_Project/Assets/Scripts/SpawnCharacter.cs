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
    public PrefabData m_prefabData;
    public int m_maxSize = 5;
    public float m_waitTime = 3;
    public int m_spawnCounter = 0;
    public CharacterList m_characsList;

    [SerializeField, Space(order =16)]
    private SpawnEvent OnSpawnEvent;
    private Transform m_transform;
    private GameObject m_prefab;
    private int m_index;

    void Start()
    {
        m_transform = transform;
        m_characsList = GetComponent<CharacterList>();
        StartCoroutine(StartSpawn(1));
    }

    public IEnumerator StartSpawn(int level)
    {
        PrefabLevel prefabLevel = m_prefabData.GetPrefabData(level);

        while (true)
        {
            yield return new WaitForSeconds(m_waitTime);
            CheckCurrentLevelPrefab(prefabLevel);

            if (m_spawnCounter < m_maxSize)
            {
                GameObject spawnObj = Instantiate(m_prefab, m_transform.position, m_transform.rotation);
                spawnObj.GetComponent<ISpawnObject>().OnSpawnObjcet(this);

                if (m_spawnTag != "") spawnObj.tag = m_spawnTag;
                OnSpawnEvent.Invoke(spawnObj.transform);

                m_spawnCounter++;
            }
        }
    }

    private void CheckCurrentLevelPrefab(PrefabLevel prefabLevel)
    {
        if(0 >= prefabLevel.prefab.Length)
        {
            Debug.Log("沒有設定關卡預製物件");
            return;
        }

        if (m_index < prefabLevel.prefab.Length)
        {
 
            float nextReadltime = prefabLevel.prefab[m_index].realtimeStartup;
            if (nextReadltime < Time.realtimeSinceStartup)
            {
                ObjectSetting setting = prefabLevel.prefab[m_index];
                setting = prefabLevel.prefab[m_index];
                nextReadltime = setting.realtimeStartup;
                m_prefab = setting.prefab;
                m_waitTime = setting.waitTime;
                m_index++;
            }
        }
    }

}
