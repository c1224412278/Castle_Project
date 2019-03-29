using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameSystem : MonoBehaviour
{
    #region 單例模式
    public static GameSystem Instance
    {
        get { return _Instance; }
    }
    private static GameSystem _Instance;
    #endregion
    
    private FeatureManager tmpFeature;  //存放接續要使用的功能
    [SerializeField] private KeyCode keyCode_shoot;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _Instance = this;
    }
    public void Fn_GetSlerpMove(FeatureManager featureController , GameObject m_object , float speed)
    {
        tmpFeature = featureController;
        Coroutine c = StartCoroutine(Fn_ExecuteSlerpMove(m_object , speed));
    }
    //開始射擊，點擊滑鼠左鍵時
    public void Fn_InputReadyShoot()
    {
        if (Input.GetKeyDown(keyCode_shoot))
        {
            UIController.Instance.Fn_SetGroupFade(UIController.Instance.Group_MouseLeft, 0f, 0.25f);        //控制 UI物件的alpha (點擊滑鼠左鍵提示)
            UIController.Instance.Fn_SetGroupFade(UIController.Instance.Group_InputTime, 1f, 0.25f);        //控制 UI物件的alpha (當前力道提示)

            UnityEngine.UI.Image inputingImg = UIController.Instance.Group_InputTime.transform.Find("value").GetComponent<UnityEngine.UI.Image>();      //抓取(力道提示)Image
            UIController.Instance.Fn_AutoAddImgAmount(inputingImg , PlayerController.thePlayerData.m_fUpStrSpeed);                                          //設定(力道提示)Image Amount

            PlayerController.thePlayerData.Fn_SetValueTween(true);              //開始計算 - 射擊力道
            PlayerController.del_Execute -= Fn_InputReadyShoot;
            PlayerController.del_Execute += Fn_ShootKeying;
            PlayerController.del_Execute += Fn_ShootKeyUp;
        }
    }
    //射擊中，滑鼠點擊不放
    public void Fn_ShootKeying()                        
    {
        if (Input.GetKey(keyCode_shoot))
        {
            if (PlayerController.thePlayerData.m_fCurStr >= GameData.stint)          //當施放的力道確定一定可以到對岸時
            {
                if (UIController.Instance.Img_ShootAnchor.color.a <= 0f)            //當力道累積到可以射到對岸時
                {
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(SceneObject.Instance.m_ObjLand.transform.position).z));
                    UIController.Instance.Fn_SetImageFade(UIController.Instance.Img_ShootAnchor, 1f, 0.25f);      //顯示瞄準點
                    UIController.Instance.Fn_UpdateImageToMousePosition(UIController.Instance.Img_ShootAnchor, SceneObject.Instance.m_ObjLand.transform);

                    PlayerController.del_Execute -= Fn_ShootKeying;
                }
            }
        }
    }
    //射擊完畢，放開滑鼠時
    public void Fn_ShootKeyUp()
    {
        if (Input.GetKeyUp(keyCode_shoot))
        {
            if (tmpFeature != null)
            {
                tmpFeature.Fn_GetThrow();               //獲得 - 物體拋擲方法
            }

            UIController.Instance.Fn_SetImageFade(UIController.Instance.Img_ShootAnchor, 0f, 0.25f);        //隱藏瞄準點
            UIController.Instance.Fn_SetGroupFade(UIController.Instance.Group_InputTime, 0f, 0.25f);        //控制 UI物件的alpha

            PlayerController.thePlayerData.Fn_SetValueTween(false);

            PlayerController.del_Execute -= Fn_ShootKeying;
            PlayerController.del_Execute -= Fn_ShootKeyUp;

            GameData.m_IsCompletedThrow = false;        //射擊結束
        }
    }
    private IEnumerator Fn_ExecuteSlerpMove(GameObject m_object, float speed)
    {
        float distance = 1000f;
        while (distance > 0.5f)
        {
            m_object.transform.position = Vector3.Slerp(m_object.transform.position, PlayerController.m_transform.position, speed * Time.deltaTime);
            distance = Vector3.Distance(m_object.transform.position, PlayerController.m_transform.position);

            yield return new WaitForEndOfFrame();
        }

        UIController.Instance.Fn_SetGroupFade(UIController.Instance.Group_MouseLeft , 1f , 0.25f);
        PlayerController.del_Execute += Fn_InputReadyShoot;
    }

    //拋無線的計算公式
    public void Lauch(GameData.ThrowData throwData , Rigidbody rigidbody , Vector3 target)
    {
        Physics.gravity = Vector3.up * throwData.gravity;
        rigidbody.useGravity = true;
        rigidbody.velocity = CalculateLunchVelocity(throwData , rigidbody, target);
    }
    private Vector3 CalculateLunchVelocity(GameData.ThrowData throwData , Rigidbody rigidbody , Vector3 target)
    {
        float displacementY = target.y - rigidbody.position.y;
        Vector3 displacementXZ = new Vector3(target.x - rigidbody.position.x , 0 , target.z - rigidbody.position.z);
        float time = Mathf.Sqrt(Mathf.Abs(-2 * throwData.h / throwData.gravity)) + Mathf.Sqrt(Mathf.Abs(2 * (displacementY - throwData.h) / throwData.gravity));
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(Mathf.Abs(-2 * throwData.gravity * throwData.h));
        Vector3 velocityXZ = displacementXZ / time;
        return velocityXZ + velocityY * -Mathf.Sign(throwData.gravity);
    }
    //----------------
}
