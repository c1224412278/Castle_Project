using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "trap Feature", menuName = "ScriptableObject/Features trap")]
public class Feature_Trap : FeatureManager
{
    public GameObject m_ObjBloodPrefab;
    public float m_fSpeed;

    private GameObject prefab;
    public override void Fn_InitObject()
    {
        prefab = Instantiate(m_ObjBloodPrefab) as GameObject;
        prefab.transform.position = v3_PrefabInitPosition;              //設定物件的初始座標位置
        ItemController itemScript = prefab.GetComponent<ItemController>();
        itemScript.feature = this;

        GameSystem.Instance.Fn_GetSlerpMove(this, prefab, m_fSpeed);            //呼叫移動方法。(從初始位置 移動至 預備射擊位置)
    }
    public override void Fn_GetThrow()
    {
        GameData.ThrowData tmpData = new GameData.ThrowData();
        Rigidbody rigidbody = prefab.GetComponent<Rigidbody>();

        float cur_str = PlayerController.thePlayerData.m_fCurStr;
        Vector3 pos = (SceneObject.Instance.m_ObjLand.transform.position - prefab.transform.position);

        if (rigidbody != null)
        {
            if (cur_str >= GameData.stint)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(SceneObject.Instance.m_ObjLand.transform.position).z));
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
    public override void Fn_ExecuteFeature()
    {
        Debug.Log("放陷阱囉 !!! ");
    }
}

