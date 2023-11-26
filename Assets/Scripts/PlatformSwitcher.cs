using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitcher : MonoBehaviour
{
    [SerializeField] private PlatformController m_Platform;
    [SerializeField] private StateType m_InitialState = StateType.Inactive;
    [Header("Active Settings")]
    [SerializeField] private PlayerType m_ActivePlayer;
    [SerializeField] private RuneType m_ActiveRune;

    // Start is called before the first frame update
    void Start()
    {
        m_Platform.SetInitialState(m_InitialState);
    }

    public void ActiveSwitch(PlayerType player ,RuneType runeUse) 
    {
        if (runeUse == m_ActiveRune && player == m_ActivePlayer) m_Platform.SwitchState();
        AudioManager.Instance.PlaySFX("Switch");
    }
}

public enum StateType { Active, Inactive }
public enum RuneType { Rune1, Rune2, Rune3 }
