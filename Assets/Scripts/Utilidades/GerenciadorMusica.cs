using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GerenciadorMusica : MonoBehaviour
{
    [SerializeField]
    private ControladorDeCena controladorDeCena;
    [SerializeField]
    private AudioSource emissorMusica;
    // Start is called before the first frame update
    private void OnEnable()
    {
        controladorDeCena.EventosEstadoJogando += TocarMusica;
        controladorDeCena.EventosEstadoPausado += PausarMusica;
        controladorDeCena.EventosEstadoVenceu += PausarMusica;
        controladorDeCena.EventosEstadoMorreu += PausarMusica;
       // if(controladorDeCena.getEstadoCena==ControladorDeCena.TipoEstadoCena.jogando)
        //{
          //  TocarMusica();
        //}
    }
    private void OnDisable()
    {
        controladorDeCena.EventosEstadoJogando -= TocarMusica;
        controladorDeCena.EventosEstadoPausado -= PausarMusica;
        controladorDeCena.EventosEstadoVenceu -= PausarMusica;
        controladorDeCena.EventosEstadoMorreu -= PausarMusica;
    }
    public void TocarMusica()
    {
        if(emissorMusica.isPlaying)
        {
            emissorMusica.UnPause();
        }
        else
        {
            emissorMusica.Play();
        }
    }
    public void PausarMusica()
    {
        if(emissorMusica.isPlaying)
        {
            emissorMusica.Pause();
        }
    }
}
