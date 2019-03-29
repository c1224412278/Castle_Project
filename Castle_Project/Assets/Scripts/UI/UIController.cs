using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class UIController : MonoBehaviour
{
    #region 單例模式
    public static UIController Instance { get { return _Instance; } }
    private static UIController _Instance;
    #endregion

    public Image Img_ShootAnchor;
    public Image Img_HpSprite;
    public CanvasGroup Group_MouseLeft;
    public CanvasGroup Group_InputTime;

    [SerializeField] private TextMeshProUGUI TextPro_End;
    [SerializeField] private Image Img_GameTime;
    private void Awake()
    {
        _Instance = this;
    }
    private void Start()
    {
        Fn_Init();
    }
    public void Fn_Init()           //UI物件初始設定
    {
        #region UI介面的初始設定
        Group_MouseLeft.alpha = 0f;
        Group_InputTime.alpha = 0f;

        Image inputingImg = UIController.Instance.Group_InputTime.transform.Find("value").GetComponent<UnityEngine.UI.Image>();
        Image redLine = UIController.Instance.Group_InputTime.transform.Find("red line").GetComponent<UnityEngine.UI.Image>();
        float width = inputingImg.GetComponent<RectTransform>().rect.width * 0.5f;
        float min_width = -width;
        float max_width = width;
        float value = (Mathf.Abs(min_width) + max_width) * GameData.stint;
        redLine.transform.localPosition = new Vector3(min_width + value, redLine.transform.localPosition.y, redLine.transform.localPosition.z);

        Fn_SetImageFade(Img_ShootAnchor , 0f , 0f);
        Fn_SetTextMeshProFade(TextPro_End , 0f , 0f);
        #endregion
    }
    public void Fn_SetGameTime(float executeTime)
    {
        StartCoroutine(Fn_CaculateGameTimer(executeTime));
    }
    private IEnumerator Fn_CaculateGameTimer(float executeTime)
    {
        float time = executeTime;
        float lerp = Mathf.InverseLerp(0 , executeTime, time);
        while (time >= 0 && PlayerController.thePlayerData.m_iCurHp > 0)
        {
            time -= Time.deltaTime;
            lerp = Mathf.InverseLerp(0, executeTime, time);
            Img_GameTime.fillAmount = lerp;

            yield return new WaitForEndOfFrame();
        }

        if (GameData.m_IsPlayingGame)
        {
            Fn_SetTextMeshProFade(TextPro_End, 1f, 0.25f);                      //逐漸顯示文字
            Fn_PlayAnima(TextPro_End.GetComponent<Animator>());                 //播放遊戲結束動畫

            GameData.m_IsPlayingGame = false;
            Debug.Log("game end.");
        }
    }

    //----------- UI顯示 管理 -----------------
    public void Fn_PlayAnima(Animator animator)
    {
        if (animator != null)
        {
            animator.Play(0);
        }
    }
    public void Fn_SetTextMeshProFade(TextMeshProUGUI textPro, float endValue, float duration)          //更改 TextMeshProUGUI - alpha
    {
        if (textPro.enabled == false)
            textPro.enabled = true;

        DOTween.Sequence().Append(textPro.DOFade(endValue, duration)).OnComplete(() => 
        {
            if (endValue <= 0)
                textPro.enabled = false;
        });
    }
    public void Fn_SetImageFade(Text text , float endValue, float duration)                             //更改 Text - alpha
    {
        if (text.enabled == false)
            text.enabled = true;

        DOTween.Sequence().Append(text.DOFade(endValue, duration)).OnComplete(() => 
        {
            if(endValue <= 0)
                text.enabled = false;
        });
    }
    public void Fn_SetImageFade(Image img, float endValue, float duration)                              //更改 Image - alpha
    {
        if (img.enabled == false)
            img.enabled = true;

        DOTween.Sequence().Append(img.DOFade(endValue, duration)).OnComplete(() => 
        {
            if (endValue <= 0)
                img.enabled = false;
        });
    }
    public void Fn_SetGroupFade(CanvasGroup group, float endValue, float duration)                      //更改 Canvas Group - alpha
    {
        DOTween.Sequence().Append(group.DOFade(endValue, duration));
    }
    public void Fn_AutoAddImgAmount(Image img, float speed)                                                 //更改 Image Amount
    {
        img.fillAmount = 0f;
        img.DOFillAmount(1f, speed);
    }
    public void Fn_UpdateAmount(Image img , float value , float speed)
    {
        img.DOFillAmount(value, speed);
    }

    //使 Image 自動跟隨 -> 滑鼠座標
    public void Fn_UpdateImageToMousePosition(Image img , Transform target)                              
    {
        StartCoroutine(Fn_SetFllowMousePosition(img, target));
    }
    private IEnumerator Fn_SetFllowMousePosition(Image img, Transform target)
    {
        yield return new WaitForEndOfFrame();

        while (img.color.a > 0)
        {
            Vector3 v3_LandPosition = SceneObject.Instance.m_ObjLand.transform.position;
            Img_ShootAnchor.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Camera.main.WorldToScreenPoint(v3_LandPosition).y, Camera.main.WorldToScreenPoint(v3_LandPosition).z));

            yield return new WaitForEndOfFrame();
        }
    }
    //--------------------------------------------
    //-------------------------------------------------------------------------
}
