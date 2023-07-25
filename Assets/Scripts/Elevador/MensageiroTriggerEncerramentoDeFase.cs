using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensageiroTriggerEncerramentoDeFase : MonoBehaviour
{
    [SerializeField]
    private ScriptElevador scriptElevador;
    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag=="Player")
        {
            scriptElevador.FecharPorta();
        }
    }
}
