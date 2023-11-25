using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitcher : MonoBehaviour
{
    [SerializeField] private PlatformController m_Platform;
    [SerializeField] private StateType m_InitialState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum StateType { Active, Inactive }
