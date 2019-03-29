using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "stone Feature", menuName = "ScriptableObject/Features stone")]
public class Feature_Stone : FeatureManager
{
    public GameObject m_ObjStonePrefab;
    public int m_iHurtValue;                  //傷害量
    public float m_fSpeed;
    
    private GameObject prefab;
    public override void Fn_InitObject()
    {
        prefab = Instantiate(m_ObjStonePrefab) as GameObject;
        prefab.transform.position = v3_PrefabInitPosition;              //設定物件的初始座標位置
        ItemController itemScript = prefab.GetComponent<ItemController>();
        itemScript.feature = this;

        GameSystem.Instance.Fn_GetSlerpMove(this , prefab, m_fSpeed);       //呼叫移動方法。(從初始位置 移動至 預備射擊位置)
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
        if (m_ObjCollisionItem.tag == "Enemy")
        {
            Debug.Log("我扔石頭囉 !!! ");
        }
    }
}

