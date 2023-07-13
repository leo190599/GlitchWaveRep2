using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensageiroPlayerShaderPersonagem : MonoBehaviour
{
    [SerializeField]
    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat=GetComponent<SkinnedMeshRenderer>().material;
    }

    public void setEfeitoGlitch(bool novoValor)
    {
        if(novoValor)
        {
            mat.SetFloat("_GlitchAtivo",1);
        }
        else
        {
            mat.SetFloat("_GlitchAtivo",0);
        }
    }
    public void setEfeitoInvencibilidade(bool novoValor)
    {
        if(novoValor)
        {
            mat.SetFloat("_InvencibilidadeAtiva",1);
        }
        else
        {
            mat.SetFloat("_InvencibilidadeAtiva",0);
        }
    }
}
