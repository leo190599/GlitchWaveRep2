using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorShaderInimigoBroca : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Material mat;
    [SerializeField]
    private static float tempoDeParada=.25f;
    void Start()
    {
        mat=GetComponent<SkinnedMeshRenderer>().material;
    }

    public void IniciarEfeitoDano()
    {
        mat.SetFloat("_DanoAtivo",1);
        StartCoroutine(EncerrarEfeitoDano());
    }
    public IEnumerator EncerrarEfeitoDano()
    {
        yield return new WaitForSeconds(tempoDeParada);
        mat.SetFloat("_DanoAtivo",0);
    }
}
