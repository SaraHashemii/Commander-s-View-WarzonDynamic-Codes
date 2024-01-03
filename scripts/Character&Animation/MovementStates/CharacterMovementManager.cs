using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementManager : MonoBehaviour
{
    #region Movement
    public float currentMoveSpeed;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 7, runBackSpeed = 5;
    public float crouchSpeed = 2, crouchBackSpeed = 1;

    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;
    [HideInInspector] public Vector3 direction;
    private CharacterController m_characterController;


    #endregion

    #region GroundCheck
    [SerializeField] private float m_GroundCheck;
    [SerializeField] private LayerMask m_GroundMask;
    private Vector3 m_spherePos;
    #endregion

    #region Gravity
    [SerializeField] private float m_gravity = -9.81f;
    private Vector3 m_velocity;
    #endregion

    #region MovementStates
    private BaseState m_currentState;

    public IdleState idleState = new IdleState();
    public WalkState walkState = new WalkState();
    public RunState runState = new RunState();
    public CrouchState crouchState = new CrouchState();

    [HideInInspector] public Animator animator;
    #endregion

    //A reference to the player model, used to disable the player model when the player enters the helo
    private GameObject m_playerModel;
    [SerializeField]
    private GameObject m_helo;
    private JUFootPlacement m_jUFootPlacement;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        m_characterController = GetComponent<CharacterController>();
        m_playerModel = gameObject.transform.Find("PlayerModel").gameObject;
    }
    private void Start()
    {
        SwitchState(idleState);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();

        animator.SetFloat("hInput", horizontalInput);
        animator.SetFloat("vInput", verticalInput);

        m_currentState.UpdateState(this);

        if (Input.GetKeyDown(KeyCode.F))
        {
            EnterTheHelo();
        }

    }

    public void SwitchState(BaseState state)
    {

        m_currentState = state;
        m_currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        direction = transform.forward * verticalInput + transform.right * horizontalInput;
        m_characterController.Move(direction.normalized * currentMoveSpeed * Time.deltaTime);
    }

    bool IsGrounded()
    {
        m_spherePos = new Vector3(transform.position.x, transform.position.y - m_GroundCheck, transform.position.z);
        if (Physics.CheckSphere(m_spherePos, m_characterController.radius - 0.05f, m_GroundMask))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    void Gravity()
    {
        if (!IsGrounded())
        {
            m_velocity.y += m_gravity * Time.deltaTime;
        }
        else if (m_velocity.y < 0)
        {
            m_velocity.y = -2f;
        }
        m_characterController.Move(m_velocity * Time.deltaTime);
    }

    private void EnterTheHelo()
    {
        if(Vector3.Distance(transform.position, m_helo.transform.position) > 5)
        {
            return;
        }
        m_playerModel.SetActive(false);
        GameManager.Instance.EnterTheHelo();
        
    }
}
