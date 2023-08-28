using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorUIGatinho : MonoBehaviour
{
    [SerializeField]
    private InformacoesPlayer2 informacoesGatinho;
    [SerializeField]
    private Image raizImagemGatinho;
    [SerializeField]
    private Image imagemGatinho;
    [SerializeField]
    private Color corImagemAtivada=Color.white;
    [SerializeField]
    private Color corImagemDesativada;
    // Start is called before the first frame update
    void Start()
    {
        raizImagemGatinho.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        informacoesGatinho.EventosAtivacaoGatinho += MostrarEstadoAtivado;
        informacoesGatinho.EventosDesativacaoGatinho += MostrarEstadoDesativado;
    }
    private void OnDisable()
    {
        informacoesGatinho.EventosAtivacaoGatinho -= MostrarEstadoAtivado;
        informacoesGatinho.EventosDesativacaoGatinho -= MostrarEstadoDesativado;
    }
    public void MostrarEstadoAtivado()
    {
        if(!imagemGatinho.gameObject.activeInHierarchy)
        {
            raizImagemGatinho.gameObject.SetActive(true);
        }
        imagemGatinho.color = corImagemAtivada;
    }
    public void MostrarEstadoDesativado()
    {
        imagemGatinho.color = corImagemDesativada;
    }
}
