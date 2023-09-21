using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptElevador : MonoBehaviour
{
    [SerializeField]
    private GameObject porta;
    [SerializeField]
    private Animator animLuz;
    [SerializeField]
    private ControladorDeCena controladorDeCena;
    [SerializeField]
    private AudioSource emissorSino;
    
    void Start()
    {
        AbrirPorta();
    }
    // Start is called before the first frame update
    public void AbrirPorta()
    {
        porta.SetActive(false);
        animLuz.SetBool("Aberto",true);
        emissorSino.Play();
    }
    public void FecharPorta()
    {
        porta.SetActive(true);
        animLuz.SetBool("Aberto",false);
        emissorSino.Play();
    }

    public void AcaoAbertura()
    {

    }
    public void AcaoFechamento()
    {
        controladorDeCena.TrocarEstadoAtual(ControladorDeCena.TipoEstadoCena.venceu);
    }
}
