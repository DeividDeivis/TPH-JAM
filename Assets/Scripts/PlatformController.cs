using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlatformController : MonoBehaviour
{
    [Tooltip("Las plataformas Switcher cambian de estado al interactuar con el switcher, moviendose de una posicion a otra, " +
        "mientras que las plataformas Cooldown se mueven a una posicion durante un tiempo y regresan a su posicion original.")]
    [SerializeField] private PlatformType platformType;
    [SerializeField] private StateType platformState;
    [Header("If is Cooldown")]
    [SerializeField] private float CooldownTime;
    [SerializeField] private bool isMoving = false;
    [Header("Animation Settings")]
    [SerializeField] private Transform activePosition;
    private Vector2 initialPos;
    private Vector2 finalPos;
    [SerializeField] private float animationTime = 2f;
    [SerializeField] private AnimationCurve _AnimationCurve;


    public void SetInitialState(StateType newState) 
    {
        initialPos = transform.position;
        finalPos = activePosition.position;
        activePosition.gameObject.SetActive(false);

        platformState = newState;

        transform.localPosition = platformState == StateType.Inactive ? initialPos : finalPos;
    }

    public void SwitchState() 
    {
        if (!isMoving) 
        { 
            isMoving = true;

            MoveAnim(() => {
                if (platformType == PlatformType.Switcher)
                    isMoving = false;
                else
                    StartCoroutine(CooldownAnim());
            });
        }
    }

    private void MoveAnim(Action _OnComplete) 
    {
        Vector2 newPos = platformState == StateType.Inactive ? finalPos : initialPos;

        transform.DOMove(newPos, animationTime).SetEase(_AnimationCurve)
            .OnComplete(() => {
                platformState = platformState == StateType.Active ? StateType.Inactive : StateType.Active;
                _OnComplete?.Invoke(); 
            });
    }

    private IEnumerator CooldownAnim() 
    {
        yield return new WaitForSeconds(CooldownTime);
        MoveAnim(()=> isMoving = false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }
}

public enum PlatformType { Switcher, Cooldown }
