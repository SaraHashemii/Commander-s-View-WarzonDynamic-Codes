using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterAimManager : MonoBehaviour
{
    private float m_xAxis, m_yAxis;
    [SerializeField] private float m_mouseSensitivity = 1f;
    [SerializeField] private Transform m_thridPersonCameraPos;

    #region Aim States
    BaseAimState m_currentAimState;
    public HipFireState m_hipFireState = new HipFireState();
    public AimState m_aimState = new AimState();

    [HideInInspector] public Animator animator;
    #endregion

    #region Aim Camera
    [HideInInspector] public CinemachineVirtualCamera vCam;
    public float aimingFov = 40;
    [HideInInspector] public float hipFireFov;
    [HideInInspector] public float currentFov;
    public float fovSmoothness = 10;
    #endregion

    public Transform aimPos;
    [SerializeField] private float m_aimSmoothness = 20;
    [SerializeField] private LayerMask m_aimMask;

    void Awake()
    {
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFireFov = vCam.m_Lens.FieldOfView;
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

        SwitchState(m_hipFireState);
    }

    // Update is called once per frame
    void Update()
    {
        m_xAxis += Input.GetAxis("Mouse X") * m_mouseSensitivity;
        m_yAxis += Input.GetAxis("Mouse Y") * m_mouseSensitivity;
        m_yAxis = Mathf.Clamp(m_yAxis, -60f, 60f);

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothness * Time.deltaTime);

        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCentre);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, m_aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, m_aimSmoothness * Time.deltaTime);
        }


        m_currentAimState.UpdateState(this);
    }

    private void LateUpdate()
    {
        m_thridPersonCameraPos.localEulerAngles = new Vector3(m_yAxis, m_thridPersonCameraPos.localEulerAngles.y, m_thridPersonCameraPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(BaseAimState state)
    {

        m_currentAimState = state;
        m_currentAimState.EnterState(this);
    }
}
