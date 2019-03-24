using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class test : MonoBehaviour
{
    public Transform target;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x , Input.mousePosition.y , 20f));
        }
    }
}
