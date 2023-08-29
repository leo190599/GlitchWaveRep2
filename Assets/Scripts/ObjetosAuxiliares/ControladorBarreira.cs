using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBarreira : MonoBehaviour
{
    [SerializeField]
    private InformacoesPlayer informacoesPlayer;
    [SerializeField]
    private GameObject objetoColisor;
    // Start is called before the first frame update
    void Start()
    {
        if(informacoesPlayer.GetGlitchAtivo)
        {
            objetoColisor.SetActive(false);
        }
    }

    private void OnEnable()
    {
        informacoesPlayer.EventosAtivacaoGlitch += DesativarColisor;
        informacoesPlayer.EventosDesativacaoGlitch += AtivarColisor;
    }
    private void OnDisable()
    {
        informacoesPlayer.EventosAtivacaoGlitch -= DesativarColisor;
        informacoesPlayer.EventosDesativacaoGlitch -= AtivarColisor;
    }

    public void AtivarColisor()
    {
        objetoColisor.SetActive(true);
    }
    public void DesativarColisor()
    {
        objetoColisor.SetActive(false);
    }
}
