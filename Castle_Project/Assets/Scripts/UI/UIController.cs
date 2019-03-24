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
