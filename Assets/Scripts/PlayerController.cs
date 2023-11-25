using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float m_MovementX;
    private bool m_IsJump;

    public void OnMove(InputAction.CallbackContext context) 
    {
        m_MovementX = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context) 
    {
        //m_IsJump = context.ReadValue<bool>();
        m_IsJump = context.action.triggered;
    }
}
