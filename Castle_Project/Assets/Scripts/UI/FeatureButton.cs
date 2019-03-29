using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FeatureButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private float m_fCDTime;
    [SerializeField] private FeatureManager feature;
    private void Start()
    {
        button = this.GetComponent<Button>();
    }
    public void Fn_Get_CdTime()
    {
        if (GameData.m_IsPlayingGame && !GameData.m_IsCompletedThrow)
        {
            button.interactable = false;
            feature.Fn_InitObject();
            StartCoroutine(Fn_Calculate(m_fCDTime));

            GameData.m_IsCompletedThrow = true;                 //開始射擊
        }
    }
    private IEnumerator Fn_Calculate(float executeTime)
    {
        float _time = executeTime;
        Image Image_Cd = this.transform.Find("Gray Mask").GetComponent<Image>();
        
        while (_time > 0)
        {
            _time -= Time.deltaTime;
            Image_Cd.fillAmount = Mathf.InverseLerp(0f , m_fCDTime, _time);
            yield return new WaitForEndOfFrame();
        }
        button.interactable = true;
    }
}
