using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDubstepGun : MonoBehaviour
{
    [SerializeField]
    private float tempoDeVida=3;
    // Start is called before the first frame update
    [SerializeField]
    private float danoRaio=70;
    [SerializeField]
    private MeshRenderer meshRaio;
    void Start()
    {
        meshRaio.material.SetFloat("_Direcao",-Mathf.Sign(transform.localScale.x));
        if(ScriptCamera.GetCameraSingleton!=null)
        {
            ScriptCamera.GetCameraSingleton.AnimarCameraGrandeImpacto();
        }
        Destroy(gameObject,tempoDeVida);
    }

    public void CausarDano(MensageiroDeEntradaDeTriggerDanoPlayerInimigo mensageiroDeEntradaDeTriggerDanoPlayerInimigo)
    {
        mensageiroDeEntradaDeTriggerDanoPlayerInimigo.LevarDano(danoRaio);
    }
}
