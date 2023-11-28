using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer runeSprite;
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

        RuneInfo info = playerRunes.Find(info => info.runeSinged == runeSing);
        RuneAnim(info);
    }

    private void RuneAnim(RuneInfo info) 
    {
        runeSprite.color = info.runeColor;
        runeSprite.transform.localScale = Vector3.zero;

        DOTween.Sequence().SetEase(Ease.Linear).SetAutoKill(true)
            .Append(runeSprite.transform.DOScale(Vector3.one, 1))
            .Join(runeSprite.DOFade(0, 1f))
            .OnComplete(() => {
                runeSprite.color = new Color(1f, 1f, 1f, 0f);
                runeSprite.transform.localScale = Vector3.zero;
            });
    }
}

[System.Serializable]
public class RuneInfo 
{    
    public RuneType runeSinged;
    public Color runeColor;
    public string runeSfx;
}
