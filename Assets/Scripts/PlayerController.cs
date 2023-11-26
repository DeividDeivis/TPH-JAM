using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private PlayerType m_Player;
    // Controllers
    [SerializeField] private MovementSystem2D MoveController; 
    [SerializeField] private InteractionSystem InteractionController;

    [Header("Character Stats")]
    [SerializeField] private float moveSpeed = 40f;

    [Header("Animations Parameters")]
    [SerializeField] private Animator animator;
    [SerializeField] private float idleTime;
    private float currentIdle;
    [SerializeField] private float blinkTime;
    private float currentBlink;

    [Header("Player Actions Permition")]
    public bool movement; // movement = can player move?
    [SerializeField] private bool m_IsJump;
    private float m_MovementX;
    private float horizontalMove = 0f;

    /*[Header("Walk SFX")]
    [SerializeField] private EventReference m_FootStepEventRef;
    private EventInstance m_FootStepInstance;
    [SerializeField] private float m_FootStepTime;
    private float curretStep;
    [Header("Damage SFX")]
    [SerializeField] private EventReference m_DeadSfx;*/

    void Start()
    {
        //m_FootStepInstance = RuntimeManager.CreateInstance(m_FootStepEventRef);
        currentIdle = idleTime;
        currentBlink = blinkTime;
    }

    // Update is called once per frame    
    void Update()
    {
        //animator.SetBool("EnSuelo", controller.m_EnSuelo);       

        if (movement == true)
        {   // Si el movimiento esta activado, habilita las animaciones.
            horizontalMove = m_MovementX * moveSpeed; // Varia entre -1 y 1. Funciona para teclado o joystick, ver Conf del proyecto.
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            animator.SetBool("Jump", m_IsJump);
            animator.SetBool("Landing", MoveController.m_InGround);

            /*if (Mathf.Abs(horizontalMove) > 0)
            {
                FootStepSFX();
                currentIdle = idleTime;
            }
            else
                curretStep = 0;*/

            //Idle Anim
            if (currentIdle > 0)
                currentIdle -= Time.deltaTime;
            else
            {
                animator.SetTrigger("LookAround");
                currentIdle = idleTime;
            }

            //Blink Anim
            if (currentBlink > 0)
                currentBlink -= Time.deltaTime;
            else
            {
                animator.SetTrigger("Blink");
                currentBlink = blinkTime;
            }
        }
    }

    // Para las Fisicas, usamos Input para mover el personaje.
    void FixedUpdate()
    {
        if (movement == true)
        {
            MoveController.Move(horizontalMove * Time.fixedDeltaTime, m_IsJump);
            m_IsJump = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_MovementX = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //m_IsJump = context.ReadValue<bool>();
        m_IsJump = context.action.triggered;
        currentIdle = idleTime;
        AudioManager.Instance.PlaySFX("Jump"); ;
    }

    public void OnSinging(InputAction.CallbackContext context) 
    {
        RuneType runeSing = RuneType.Rune1;

        switch (context.action.name) 
        {
            case "Sing 1": runeSing = RuneType.Rune1; AudioManager.Instance.PlaySFX("SingC"); break;
            case "Sing 2": runeSing = RuneType.Rune2; AudioManager.Instance.PlaySFX("SingE"); break;
            case "Sing 3": runeSing = RuneType.Rune3; AudioManager.Instance.PlaySFX("SingG"); break;
        }
        context.action.started += (ctx) => InteractionController.Interaction(m_Player, runeSing);

        animator.SetTrigger("Sing");
    }

    public void OnLanding()
    {
        animator.SetBool("Jump", false);    
    }

    private void Die()
    {
        // Do Something.       
    }

  /*  public void FootStepSFX()
    {
        curretStep -= Time.deltaTime;
        if (curretStep <= 0)
        {
           
            if (run)
            {
                m_FootStepInstance.setParameterByNameWithLabel("Movement Speed", "Run");
                m_FootStepInstance.start();
                m_FootStepInstance.release();
            }
            else
            {
                m_FootStepInstance.setParameterByNameWithLabel("Movement Speed", "Walk");
                m_FootStepInstance.start();
                m_FootStepInstance.release();
            }
            curretStep = m_FootStepTime;
        }
    }*/
}

public enum PlayerType { Player1, Player2 }
