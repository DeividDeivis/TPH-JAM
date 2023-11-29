using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private List<PlayerController> playersInTrigger = new List<PlayerController>();

    private void CheckIfLevelComplete() 
    {
        bool player1InTrigger = playersInTrigger.Exists(player => player.m_Player == PlayerType.Player1);
        bool player2InTrigger = playersInTrigger.Exists(player => player.m_Player == PlayerType.Player2);
        if (player1InTrigger && player2InTrigger)
            if (playersInTrigger[0].GetComponent<MovementSystem2D>().m_InGround && playersInTrigger[1].GetComponent<MovementSystem2D>().m_InGround)
                StartCoroutine(WinLevel());
    }

    private IEnumerator WinLevel() 
    {
        playersInTrigger.ForEach(player => player.WinDance());
        yield return new WaitForSeconds(5);
        GameManager.Instance.LoadNextScene();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            PlayerController playerDetected = collision.GetComponent<PlayerController>();
            bool playerInList = playersInTrigger.Exists(player => player == playerDetected);
            if (!playerInList) playersInTrigger.Add(playerDetected);

            CheckIfLevelComplete();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController playerDetected = collision.GetComponent<PlayerController>();
        playersInTrigger.Remove(playerDetected);
    }
}
