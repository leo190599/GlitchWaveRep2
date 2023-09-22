using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoGlitch : MonoBehaviour
{
    [SerializeField]
    private InformacoesPlayer informacoesPlayer;
    [SerializeField]
    private Material mat;
    [SerializeField]
    private float valorOpaciadadeLigada = -5.71f;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        mat.SetFloat("_Opacidade",1);
    }
    private void OnEnable()
    {
        informacoesPlayer.EventosAtivacaoGlitch += AtivarEfeitoShader;
        informacoesPlayer.EventosDesativacaoGlitch += DesativarEfeitoShader;
    }
    private void OnDisable()
    {
        informacoesPlayer.EventosAtivacaoGlitch -= AtivarEfeitoShader;
        informacoesPlayer.EventosDesativacaoGlitch -= DesativarEfeitoShader;
    }
    public void AtivarEfeitoShader()
    {
        mat.SetFloat("_Opacidade", valorOpaciadadeLigada);
    }
    public void DesativarEfeitoShader()
    {
        mat.SetFloat("_Opacidade", 1);
    }
}
