using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptMeshRobo : MonoBehaviour
{

    [SerializeField]
    private SkinnedMeshRenderer malhaRobo;
    [SerializeField]
    private float tempoEfeitoDano = 1;
    private Material material;
    private Animator anim;
    public enum EstadosAnimacao
    {
        idle=0,
        preparandoAtaque=1,
        ataque=2
    }
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        material=malhaRobo.material;
    }
    public void TrocarAnim(EstadosAnimacao novoEstadoAnim)
    {
        anim.SetInteger("Estado",(int)novoEstadoAnim);
    }
    public void AtivarTriggerShader()
    {
        material.SetFloat("_TriggerAtivo",1);
    }
    public void DesativarTriggerShader()
    {
        material.SetFloat("_TriggerAtivo", 0);
    }
    public void AtivarEfeitoDano()
    {
        material.SetFloat("_DanoAtivo",1);
        StartCoroutine(PararEfeitoDano());
    }
    IEnumerator PararEfeitoDano()
    {
        yield return new WaitForSeconds(tempoEfeitoDano);
        material.SetFloat("_DanoAtivo",0);
    }
}
