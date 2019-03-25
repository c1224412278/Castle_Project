using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public FeatureManager feature { get; set; }
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Across Land")
        {
            feature.Fn_ExecuteFeature();            //執行道具功能
            Destroy(this.gameObject);
        }
    }
}
