using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InteractionLevel : MonoBehaviour
{
    /**
     * 
         // Start is called before the first frame update


        // Update is called once per frame
        void Update()
        {
            
        }*/

    public List<GameObject> game;
    private List<bool> bools;
    public bool trigger = false;
    private SpriteRenderer spr;
    private string nombre;
    GameManager gameManager;

    void Start()
    {
        bools = new List<bool>(new bool[game.Count()]);
        nombre = gameObject.name;
        gameManager = new GameManager();
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name == "Player 1" && nombre.Contains("Green"))
        {
            trigger = true;
            agregarListaBool();
        }else if (collision.name == "Player 2" && nombre.Contains("Orange"))
        {
            trigger = true;
            agregarListaBool();
        }

        if (bools.Any(valor => valor == false))
        {

        }
        else
        {
            gameManager.LoadScene("Nivel_01");
            gameManager.LoadNextScene();
        }
    }

    private void agregarListaBool() { 

        for (int i = 0; i < game.Count; i++)
        {
            bool valor = game[i].GetComponent<InteractionLevel>().trigger;
            bools[i] = valor;
        }
    }

}