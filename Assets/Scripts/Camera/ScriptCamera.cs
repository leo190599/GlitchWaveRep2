using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Vector3 velocidadeDeSeguimento;
    [SerializeField]
    private Vector3 maximaDistanciaDoLocalFinal;

    private Vector3 offSet;
    private Vector3 novaTransformada;
    [SerializeField]
    private InformacoesPlayer informacoesPlayer;
    [SerializeField]
    private Animator animator;
    //Startis called before the first frame update
    void Start()
    {
        offSet=transform.position-player.transform.position;    
        novaTransformada=new Vector3();
        AnimarCameraIdle();
    }
    private void OnEnable()
    {
        informacoesPlayer.EventosLevarDanoDeInimigo += AnimarCameraLevarDano;
        informacoesPlayer.EventosDashFrente+= AnimarCameraDashFrente;
        informacoesPlayer.EventosDashTras += AnimarCameraDashTras;
    }
    private void OnDisable()
    {
        informacoesPlayer.EventosLevarDanoDeInimigo -= AnimarCameraLevarDano;
        informacoesPlayer.EventosDashFrente -= AnimarCameraDashFrente;
        informacoesPlayer.EventosDashTras -= AnimarCameraDashTras;
    }

    public void AnimarCameraDashFrente()
    {
        animator.SetInteger("AnimIndex", 1);
    }
    public void AnimarCameraDashTras()
    {
        animator.SetInteger("AnimIndex", 2);
    }
    public void AnimarCameraLevarDano()
    {
        animator.SetInteger("AnimIndex", 3);
    }
    public void AnimarCameraIdle()
    {
        animator.SetInteger("AnimIndex", 0);
    }
    // Update is called once per frame
    void Update()
    {
        
        /*
        novaTransformada.x=Mathf.Lerp(transform.position.x,player.transform.position.x+offSet.x,velocidadeDeSeguimento.x);
        novaTransformada.y=Mathf.Lerp(transform.position.y,player.transform.position.y+offSet.y, velocidadeDeSeguimento.y);
        novaTransformada.z=Mathf.Lerp(transform.position.z,player.transform.position.z+offSet.z,velocidadeDeSeguimento.z);

        novaTransformada.x=Mathf.Clamp(novaTransformada.x,
        player.transform.position.x+offSet.x-maximaDistanciaDoLocalFinal.x,
        player.transform.position.x+offSet.x+maximaDistanciaDoLocalFinal.x);
        
        novaTransformada.y=Mathf.Clamp(novaTransformada.y,
        player.transform.position.y+offSet.y-maximaDistanciaDoLocalFinal.y,
        player.transform.position.y+offSet.y+maximaDistanciaDoLocalFinal.y);
        
        novaTransformada.z=Mathf.Clamp(novaTransformada.z,
        player.transform.position.z+offSet.z-maximaDistanciaDoLocalFinal.z,
        player.transform.position.z+offSet.z+maximaDistanciaDoLocalFinal.z);
        */
        novaTransformada=player.transform.position+offSet;
        transform.SetPositionAndRotation(novaTransformada,transform.rotation);
        

    }
}
