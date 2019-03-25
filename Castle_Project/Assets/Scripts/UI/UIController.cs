using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIController : MonoBehaviour
{
    #region 單例模式
    public static UIController Instance { get { return _Instance; } }
    private static UIController _Instance;
    #endregion

    public CanvasGroup Group_MouseLeft;
    public CanvasGroup Group_InputTime;
    private void Awake()
    {
        _Instance = this;
    }
    public void Fn_Init()           //UI物件初始設定
    {
        Group_MouseLeft.alpha = 0f;
        Group_InputTime.alpha = 0f;

        Image inputingImg = UIController.Instance.Group_InputTime.transform.Find("value").GetComponent<UnityEngine.UI.Image>();
        Image redLine = UIController.Instance.Group_InputTime.transform.Find("red line").GetComponent<UnityEngine.UI.Image>();
        float width = inputingImg.GetComponent<RectTransform>().rect.width * 0.5f;
        float min_width = -width;
        float max_width = width;
        float value = (Mathf.Abs(min_width) + max_width) * GameData.stint;
        redLine.transform.localPosition = new Vector3(min_width + value, redLine.transform.localPosition.y, redLine.transform.localPosition.z);
    }
    public void Fn_SetFade(CanvasGroup group , float endValue , float duration)
    {
        DOTween.Sequence().Append(group.DOFade(endValue , duration));
    }
    public void Fn_UpdateAmount(Image img , float speed)
    {
        img.fillAmount = 0f;
        img.DOFillAmount(1f , speed);
    }
}
