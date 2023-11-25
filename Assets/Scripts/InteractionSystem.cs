using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private List<RuneInfo> playerRunes;

    [SerializeField] private LayerMask m_WhatIsInteractable;        // Layer de los objetos interactuables.
    [SerializeField] private Transform m_InteractionCheck;          // Verifica si esta en contacto con un objeto interactuable.

    [SerializeField] private float k_InteractionCheckRadio = 2f; 	// Radio del circulo que detecta la interaccion.

    public void Interaction(PlayerType playerSing, RuneType runeSing) 
    {
        Collider2D collider = Physics2D.OverlapCircle(m_InteractionCheck.position, k_InteractionCheckRadio, m_WhatIsInteractable); // Detecta si SueloChack detecta Suelo.

        if (collider != null && collider.gameObject.tag == "Switcher")
        {
            collider.GetComponent<PlatformSwitcher>().ActiveSwitch(playerSing, runeSing);
        }
    }
}

[System.Serializable]
public class RuneInfo 
{    
    public RuneType runeSinged;
    public Sprite runeSrite;
    public string runeSfx;
}
