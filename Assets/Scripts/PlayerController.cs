using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public PlayerType m_Player;
    // Controllers
    [SerializeField] private MovementSystem2D MoveController; 
    [SerializeField] private InteractionSystem InteractionController;
    [SerializeField] private InputController InputController;

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

    [Header("Player Interaction Parameters")]
    [SerializeField] private float interactionCD = 2;
    private bool waitToInteract = false;

    void Start()
    {
        currentIdle = idleTime;
        currentBlink = blinkTime;

        InputController.MoveInput += Move;
        InputController.JumpInput += Jump;
        InputController.SingInput += Singing;
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

    #region NewInputSystem
    /*public void OnMove(InputAction.CallbackContext context)
    {
        m_MovementX = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //m_IsJump = context.ReadValue<bool>();
        m_IsJump = context.action.triggered;
        currentIdle = idleTime;
        if(MoveController.m_InGround) AudioManager.Instance.PlaySFX("Jump");
    }

    public void OnSinging(InputAction.CallbackContext context) 
    {
        if (!waitToInteract) 
        {
            RuneType runeSing = RuneType.Rune1;

            switch (context.action.name)
            {
                case "Sing 1": runeSing = RuneType.Rune1; AudioManager.Instance.PlaySFX("SingC"); break;
                case "Sing 2": runeSing = RuneType.Rune2; AudioManager.Instance.PlaySFX("SingE"); break;
                case "Sing 3": runeSing = RuneType.Rune3; AudioManager.Instance.PlaySFX("SingG"); break;
            }

            InteractionController.Interaction(m_Player, runeSing);

            animator.SetTrigger("Sing");

            waitToInteract = true;
            StartCoroutine(Wait());
        }     
    }*/
    #endregion

    #region OldInputSystem
    public void Move(float MovX)
    {
        m_MovementX = MovX;
    }

    public void Jump()
    {
        //m_IsJump = context.ReadValue<bool>();
        m_IsJump = true;
        currentIdle = idleTime;
        if (MoveController.m_InGround) AudioManager.Instance.PlaySFX("Jump");
    }

    public void Singing(int Sing)
    {
        if (!waitToInteract)
        {
            RuneType runeSing = RuneType.Rune1;

            switch (Sing)
            {
                case 1: runeSing = RuneType.Rune1; AudioManager.Instance.PlaySFX("SingC"); break;
                case 2: runeSing = RuneType.Rune2; AudioManager.Instance.PlaySFX("SingE"); break;
                case 3: runeSing = RuneType.Rune3; AudioManager.Instance.PlaySFX("SingG"); break;
            }

            InteractionController.Interaction(m_Player, runeSing);

            animator.SetTrigger("Sing");

            waitToInteract = true;
            StartCoroutine(Wait());
        }
    }
    #endregion

    public void OnLanding()
    {
        animator.SetBool("Jump", false);    
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(interactionCD);
        waitToInteract = false;
    }

    public void WinDance() 
    {
        movement = false;
        animator.SetTrigger("Happy");
    }
}

public enum PlayerType { Player1, Player2 }
