using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "stone Feature", menuName = "ScriptableObject/Features stone")]
public class Feature_Stone : FeatureManager
{
    public GameObject m_ObjStonePrefab;
    public float m_fSpeed;

    private GameObject prefab;
    public override void Fn_InitObject()
    {
        prefab = Instantiate(m_ObjStonePrefab) as GameObject;
        GameSystem.Instance.Fn_GetSlerpMove(this , prefab, m_fSpeed);
    }
    public override void Fn_ExecuteFeature()
    {
        GameData.ThrowData tmpData = new GameData.ThrowData();
        Rigidbody rigidbody = prefab.GetComponent<Rigidbody>();

        const float stint = 0.7f;       //力道超過這個數值，就一定會抵達對面
        float cur_str = PlayerController.thePlayerData.m_fCurStr;
        Vector3 pos = (SceneObject.Instance.m_ObjLand.transform.position - prefab.transform.position);

        if (rigidbody != null)
        {
            if (cur_str >= stint)
            {
                Debug.Log("completed");
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x , Input.mousePosition.y , Camera.main.WorldToScreenPoint(SceneObject.Instance.m_ObjLand.transform.position).z));
                GameSystem.Instance.Lauch(tmpData, rigidbody, mousePosition);
            }
            else
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(SceneObject.Instance.m_ObjLand.transform.position).z));
                mousePosition.z = pos.z * cur_str;
                GameSystem.Instance.Lauch(tmpData, rigidbody, mousePosition);
            }
        }

        prefab = null;
    }
}

