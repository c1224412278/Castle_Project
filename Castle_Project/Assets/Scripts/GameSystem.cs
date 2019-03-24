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
        if (tmpFeature == null)
            tmpFeature = featureController;

        Coroutine c = StartCoroutine(Fn_ExecuteSlerpMove(m_object , speed));
    }
    public void Fn_InputReadyShoot()            //點擊準備射擊
    {
        if (Input.GetKeyDown(keyCode_shoot))
        {
            UIController.Instance.Fn_SetFade(UIController.Instance.Group_MouseLeft, 0f, 0.25f);
            UIController.Instance.Fn_SetFade(UIController.Instance.Group_InputTime, 1f, 0.25f);

            UnityEngine.UI.Image inputingImg = UIController.Instance.Group_InputTime.transform.Find("value").GetComponent<UnityEngine.UI.Image>();
            //UIController.Instance.Fn_UpdateAmount(inputingImg , 1f);  //整理 code 、 集器的分隔紅線 position

            PlayerController.thePlayerData.Fn_SetValueTween(true);
            PlayerController.del_Execute -= Fn_InputReadyShoot;
            PlayerController.del_Execute += Fn_ShootKeyUp;
        }
    }
    public void Fn_ShootKeyUp()
    {
        if (Input.GetKeyUp(keyCode_shoot))
        {
            if(tmpFeature != null)
                tmpFeature.Fn_ExecuteFeature();

            UIController.Instance.Fn_SetFade(UIController.Instance.Group_InputTime, 0f, 0.25f);

            PlayerController.thePlayerData.Fn_SetValueTween(false);
            PlayerController.del_Execute -= Fn_ShootKeyUp;
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

        UIController.Instance.Fn_SetFade(UIController.Instance.Group_MouseLeft , 1f , 0.25f);
        PlayerController.del_Execute += Fn_InputReadyShoot;
    }
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
        float time = Mathf.Sqrt(-2 * throwData.h / throwData.gravity) + Mathf.Sqrt(2 * (displacementY - throwData.h) / throwData.gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * throwData.gravity * throwData.h);
        Vector3 velocityXZ = displacementXZ / time;
        return velocityXZ + velocityY * -Mathf.Sign(throwData.gravity);
    }
}
