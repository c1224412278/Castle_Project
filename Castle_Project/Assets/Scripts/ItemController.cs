using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public FeatureManager feature { get; set; }
    private void Start()
    {
        StartCoroutine(Fn_DestroyObject());
    }
    private IEnumerator Fn_DestroyObject()
    {
        yield return new WaitUntil(Fn_HeightCalculation);
        Destroy(this.gameObject);
    }
    private bool Fn_HeightCalculation()
    {
        if (this.transform.position.y <= -15f)
            return true;
        else
            return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Across Land" || other.tag == "Castle" || other.tag == "Enemy")
        {
            feature.m_ObjCollisionItem = other.gameObject;          //紀錄碰到的物件
            feature.Fn_ExecuteFeature();            //執行道具功能
        }
    }
}
