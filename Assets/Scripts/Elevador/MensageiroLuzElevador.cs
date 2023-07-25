using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensageiroLuzElevador : MonoBehaviour
{
    [SerializeField]
    private ScriptElevador scriptElevador;
    public void Abrir()
    {
        scriptElevador.AcaoAbertura();
    }
    public void Fechar()
    {
        scriptElevador.AcaoFechamento();
    }
}
