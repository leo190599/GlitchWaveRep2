using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDeMenus : MonoBehaviour
{
    [SerializeField]
    private ControladorDeCena controladorDeCena;

    [SerializeField]
    private GameObject menuAtivo;

    [SerializeField]
    private GameObject menuJogando,menuPausa,menuGameOver,menuVenceu;

    void Start()
    {
        menuJogando.SetActive(false);
        menuPausa.SetActive(false);
        menuGameOver.SetActive(false);
        menuVenceu.SetActive(false);

        switch(controladorDeCena.getEstadoCena)
        {
            case ControladorDeCena.TipoEstadoCena.jogando:
                AtivarMenuJogando();
            break;

            case ControladorDeCena.TipoEstadoCena.pausado:
                AtivarMenuPausa();
            break;
            
            case ControladorDeCena.TipoEstadoCena.venceu:
                AtivarMenuVenceu();
            break;
             
            case ControladorDeCena.TipoEstadoCena.morreu:
                AtivarMenuGameOver();
            break;

            default:
                Debug.LogError("Selecidone um estado valido");
            break;
        }
    }
    void OnEnable()
    {
        controladorDeCena.EventosEstadoJogando+=AtivarMenuJogando;
        controladorDeCena.EventosEstadoMorreu+=AtivarMenuGameOver;
        controladorDeCena.EventosEstadoPausado+=AtivarMenuPausa;
        controladorDeCena.EventosEstadoVenceu+=AtivarMenuVenceu;
    }
    void OnDisable()
    {
        controladorDeCena.EventosEstadoJogando-=AtivarMenuJogando;
        controladorDeCena.EventosEstadoMorreu-=AtivarMenuGameOver;
        controladorDeCena.EventosEstadoPausado-=AtivarMenuPausa;
        controladorDeCena.EventosEstadoVenceu-=AtivarMenuVenceu;
    }
    
    public void TrocarEstadoCenaJogando()
    {
        controladorDeCena.TrocarEstadoAtual(ControladorDeCena.TipoEstadoCena.jogando);
    }
    public void TrocarEstadoCenaPausa()
    {
        controladorDeCena.TrocarEstadoAtual(ControladorDeCena.TipoEstadoCena.pausado);
    }
    public void TrocarEstadoCenaVenceu()
    {
        controladorDeCena.TrocarEstadoAtual(ControladorDeCena.TipoEstadoCena.venceu);
    }
    public void TrocarEstadoMorreu()
    {
        controladorDeCena.TrocarEstadoAtual(ControladorDeCena.TipoEstadoCena.morreu);
    }

    public void ReiniciarCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IrParaCena(string novaCena)
    {
        SceneManager.LoadScene(novaCena);
    }

    public void DesativarMenuAtual()
    {
        if(menuAtivo!=null)
        {
            menuAtivo.SetActive(false);
        }
    }

    public void AtivarMenuJogando()
    {
        DesativarMenuAtual();
        menuJogando.SetActive(true);
        menuAtivo=menuJogando;
    }

    public void AtivarMenuPausa()
    {
        DesativarMenuAtual();
        menuPausa.SetActive(true);
        menuAtivo=menuPausa;
    }
    public void AtivarMenuGameOver()
    {
        DesativarMenuAtual();
        menuGameOver.SetActive(true);
        menuAtivo=menuGameOver;
    }
    public void AtivarMenuVenceu()
    {
        DesativarMenuAtual();
        menuVenceu.SetActive(true);
        menuAtivo=menuVenceu;
    }

}
