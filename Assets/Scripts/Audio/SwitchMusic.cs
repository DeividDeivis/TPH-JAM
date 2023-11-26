using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusic : MonoBehaviour
{
    public DynamicPlayer dynamicPlayerScript;

    void Update()

    { if (Input.GetKeyDown(KeyCode.J))
        {
            // Verifica si la referencia no es nula
            if (dynamicPlayerScript != null)
            {
                // Llama al método SwitchParts
                dynamicPlayerScript.SwitchParts(1); // Puedes cambiar el argumento según tus necesidades
            }
            else
            {
                Debug.LogError("La referencia a DynamicPlayerScript es nula. Asegúrate de asignarla en el inspector.");
            }
        }
    }
}