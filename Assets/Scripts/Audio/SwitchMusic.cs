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
                // Llama al m�todo SwitchParts
                dynamicPlayerScript.SwitchParts(1); // Puedes cambiar el argumento seg�n tus necesidades
            }
            else
            {
                Debug.LogError("La referencia a DynamicPlayerScript es nula. Aseg�rate de asignarla en el inspector.");
            }
        }
    }
}