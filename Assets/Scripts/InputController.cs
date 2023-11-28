using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private InputDeviceType m_InputDevice;

    [Header("Move Inputs")]
    //[SerializeField] private KeyCode _keyUp;
    //[SerializeField] private KeyCode _keyDown;
    [SerializeField] private KeyCode _keyRight;
    [SerializeField] private KeyCode _keyLeft;
    public Action<float> MoveInput;

    [Header("Actions Inputs")]
    [SerializeField] private KeyCode _keyJump;
    public Action JumpInput;
    [SerializeField] private KeyCode _keySing1;
    [SerializeField] private KeyCode _keySing2;
    [SerializeField] private KeyCode _keySing3;
    public Action<int> SingInput;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(_keyRight)) MoveInput?.Invoke(1);
        else if (Input.GetKey(_keyLeft)) MoveInput?.Invoke(-1);
        else MoveInput?.Invoke(0);

        if (Input.GetKeyDown(_keyJump)) JumpInput?.Invoke();

        if (Input.GetKeyDown(_keySing1)) SingInput?.Invoke(1);
        if (Input.GetKeyDown(_keySing2)) SingInput?.Invoke(2);
        if (Input.GetKeyDown(_keySing3)) SingInput?.Invoke(3);
    }
}

public enum InputDeviceType { Keyboard, Gamepad }
