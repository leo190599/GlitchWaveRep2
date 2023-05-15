using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptGatinho : MonoBehaviour
{
    public enum EstadoGatinho
    {
        desativado=0,
        mirando=1,
        atacando=2
    }
    [SerializeField]
    private float dano=5;
    [SerializeField]
    private float vel=5;
    private Rigidbody2D rb;
    
    [SerializeField]
    private float tempoDeAtaque=1;
    
    [SerializeField]
    private GameObject malha;

    [SerializeField]
    private ControladorDeCena controladorDeCena;

    //Serve para que nao haja necessidade de alocar memoria a cada fixedUpdate
    private MensageiroDeEntradaDeTriggerDanoPlayerInimigo mensageiroDeEntradaDeTriggerDanoPlayerInimigo;
    private MensageiroDeEntradaDeTriggerDanoPlayerInimigo ultimoInimigoAtingido;
    private EstadoGatinho estadoGatinho;
    private Camera cam;
    private Vector2 direcaoDeAtaque;
    private float anguloGatinho;
    private bool EsperandoADesativacao=false;
    private List<RaycastHit2D> rh;
 
    // Start is called before the first frame update
    void Start()
    {
        rh=new List<RaycastHit2D>();
        rb=GetComponent<Rigidbody2D>();
        direcaoDeAtaque=new Vector2();
        cam=Camera.main;
        malha.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(controladorDeCena.getEstadoCena==ControladorDeCena.TipoEstadoCena.jogando)
        {
            if(estadoGatinho==EstadoGatinho.desativado)
            {
                if(Input.GetMouseButtonDown(0))
                {  
                    transform.position=new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x,cam.ScreenToWorldPoint(Input.mousePosition).y,0);
                    malha.SetActive(true);
                    estadoGatinho=EstadoGatinho.mirando;
                }
            }
            else if(estadoGatinho==EstadoGatinho.mirando)
            {
                anguloGatinho=Mathf.Atan2(cam.ScreenToWorldPoint(Input.mousePosition).y-transform.position.y,cam.ScreenToWorldPoint(Input.mousePosition).x-transform.position.x);
                direcaoDeAtaque.y=Mathf.Sin(anguloGatinho);
                direcaoDeAtaque.x=Mathf.Cos(anguloGatinho);
                transform.rotation=Quaternion.Euler(new Vector3(0,0,anguloGatinho*Mathf.Rad2Deg));
            
                if(Input.GetMouseButtonUp(0))
                {
                    estadoGatinho=EstadoGatinho.atacando;
                }
            }
            else if(estadoGatinho==EstadoGatinho.atacando)
            {
                if(!EsperandoADesativacao)
                {
                    StartCoroutine(CorrotinaDesativar(tempoDeAtaque));
                    transform.rotation=Quaternion.Euler(new Vector3(0,0,anguloGatinho*Mathf.Rad2Deg));
                }
            }
        }
    }

    void FixedUpdate()
    {
        if(estadoGatinho==EstadoGatinho.atacando)
        {
            if(rb.Cast(direcaoDeAtaque,rh,vel)!=0)
            {
                foreach(RaycastHit2D r in rh)
                {
                    mensageiroDeEntradaDeTriggerDanoPlayerInimigo=r.collider.GetComponent<MensageiroDeEntradaDeTriggerDanoPlayerInimigo>();
                    if(mensageiroDeEntradaDeTriggerDanoPlayerInimigo!=null && mensageiroDeEntradaDeTriggerDanoPlayerInimigo!=ultimoInimigoAtingido)
                    {
                        direcaoDeAtaque=Vector2.Reflect(direcaoDeAtaque,r.normal);
                        anguloGatinho=Mathf.Atan2(direcaoDeAtaque.y,direcaoDeAtaque.x);
                        transform.rotation=Quaternion.Euler(new Vector3(0,0,anguloGatinho*Mathf.Rad2Deg));
                        r.collider.GetComponent<MensageiroDeEntradaDeTriggerDanoPlayerInimigo>().LevarDano(dano);
                        ultimoInimigoAtingido=mensageiroDeEntradaDeTriggerDanoPlayerInimigo;
                        //Debug.Log(direcaoDeAtaque);
                        rh.Clear();
                        return;
                    }
                }
                rh.Clear();
            }
            rb.MovePosition(new Vector2(transform.position.x+direcaoDeAtaque.x*vel,transform.position.y+direcaoDeAtaque.y*vel));
        }
    }

    IEnumerator CorrotinaDesativar(float temposParaDesativamento)
        {
            EsperandoADesativacao=true;
            yield return new WaitForSeconds(temposParaDesativamento);
            estadoGatinho=EstadoGatinho.desativado;
            EsperandoADesativacao=false;ultimoInimigoAtingido=null;
            malha.SetActive(false);
        }
}
