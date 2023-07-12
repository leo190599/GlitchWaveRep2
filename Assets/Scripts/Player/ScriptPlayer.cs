using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScriptPlayer : MonoBehaviour
{
    [Header("Parametros Design")]
    [SerializeField]
    private float velocidadeDeMovimento=5f;
    [SerializeField]
    private float forcaPulo=10;
    [SerializeField]
    private bool olhandoParaDireita=true;

    [SerializeField]
    [Range(0f,1f)]
    private float perdaDeMovimentoNoAr=0f;
    [SerializeField]
    private float vidaCuradaPorColisaoParticula=1;
    [SerializeField]
    private Vector2 offSetCentroAtaquePlayer;
    [SerializeField]
    private Vector2 dimensoesAtaque;
    [SerializeField]
    private Vector2 offSetAtaqueAbaixada;

    [Header("Parametros Debug")]
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private InformacoesPlayer informacoesPlayer;
    [SerializeField]
    private float distanciaChecagemPulo=1;
    [SerializeField]
    private GameObject meshPersonagem;
    [SerializeField]
    private ControladorEfeitosEspadaKaede controladorEfeitosEspadaKaede;

    [SerializeField]
    private Transform transformPosicaoInstanciaSubItem;
    private List<RaycastHit2D>raycastsPulo;
    private Collider2D[] colisoresAtaque;
    

    [Header("Scriptable objects")]
    [SerializeField]
    private ControladorDeCena controldadorDeCenaPlayer;
    [SerializeField]
    private MapeadorDeBotoes mapeadorDeBotoes;
    private EstadoBasePlayer estadoPlayerAtual;
    
    [Header("Materiais Fisicos")]
    [SerializeField]
    private PhysicsMaterial2D materialFisicoParado;
    [SerializeField]
    private PhysicsMaterial2D materialFisicoAndando;

    [Header("Componentes fisicos")]
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private CapsuleCollider2D col;
    
    private Vector3 rotacaoAlvo;
    // Start is called before the first frame update

    public enum EstadosAnimacao
    {
        idle=0,
        correndo=1,
        pulando=2,
        usandoSubItem=3,
        atacando=4,
        abaixada=5,
        abaixadaAtacando=6,
        pulandoAtacando=7,
        levandoDano=8
    }
    void Start()
    {
        controldadorDeCenaPlayer.TrocarEstadoAtual(ControladorDeCena.TipoEstadoCena.jogando);
        informacoesPlayer.EncherVida();
        raycastsPulo=new List<RaycastHit2D>();
        rb=GetComponent<Rigidbody2D>();
        col=GetComponent<CapsuleCollider2D>();

        //teste
        rotacaoAlvo=meshPersonagem.transform.eulerAngles;
        //teste

        estadoPlayerAtual=new EstadoIdlePlayer();
        estadoPlayerAtual.IniciarEstadoPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(controldadorDeCenaPlayer.getEstadoCena==ControladorDeCena.TipoEstadoCena.jogando)
        {
            estadoPlayerAtual.AtualizarEstado();
            if(Input.GetKeyDown(mapeadorDeBotoes.GetBotaoPause))
            {
                controldadorDeCenaPlayer.TrocarEstadoAtual(ControladorDeCena.TipoEstadoCena.pausado);
            }
        }
        else if(controldadorDeCenaPlayer.getEstadoCena==ControladorDeCena.TipoEstadoCena.pausado)
        {
            if(Input.GetKeyDown(mapeadorDeBotoes.GetBotaoPause))
            {
                controldadorDeCenaPlayer.TrocarEstadoAtual(ControladorDeCena.TipoEstadoCena.jogando);
            }
        }
        else if(controldadorDeCenaPlayer.getEstadoCena==ControladorDeCena.TipoEstadoCena.morreu)
        {
            if(Input.GetKeyDown(mapeadorDeBotoes.GetBotaoPause))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        //Mudar depois
        
        if(Input.GetKeyDown(KeyCode.W))
        {
            informacoesPlayer.ReceberDano(10);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            informacoesPlayer.Curar(10);
        }
        Debug.Log(estadoPlayerAtual);
    }

    void FixedUpdate()
    {   if(controldadorDeCenaPlayer.getEstadoCena==ControladorDeCena.TipoEstadoCena.jogando)
        {
            estadoPlayerAtual.AtualizarEstadoFixado();
            
            meshPersonagem.transform.eulerAngles=new Vector3(0,Mathf.Lerp(meshPersonagem.transform.eulerAngles.y,rotacaoAlvo.y,.5f),0);
            
            if(estadoPlayerAtual is EstadoAtivoBasePlayer && !(estadoPlayerAtual is EstadoNoArBasePlayer))
            {
                rb.Cast(Vector2.down,raycastsPulo,distanciaChecagemPulo);
                if(raycastsPulo.Count!=0)
                {
                    foreach(RaycastHit2D r in raycastsPulo)
                    {
                        if(r.collider.tag!="Player")
                        {
                            raycastsPulo.Clear();
                            return;
                        }
                    }
                    TrocaEstadoPlayer(new EstadoPuloPlayer());
                }
                else
                {
                    TrocaEstadoPlayer(new EstadoPuloPlayer());
                }
                raycastsPulo.Clear();
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(col.bounds.center,new Vector3(col.bounds.center.x,col.bounds.min.y-distanciaChecagemPulo,col.bounds.min.z));
        if(olhandoParaDireita)
        {
            Gizmos.DrawWireCube(new Vector3(transform.position.x+offSetCentroAtaquePlayer.x,transform.position.y+offSetCentroAtaquePlayer.y,transform.position.z),dimensoesAtaque);
        }
        else
        {
            Gizmos.DrawWireCube(new Vector3(transform.position.x-offSetCentroAtaquePlayer.x,transform.position.y+offSetCentroAtaquePlayer.y,transform.position.z),dimensoesAtaque);
        }

        Gizmos.color=Color.green;

        if(olhandoParaDireita)
        {
            Gizmos.DrawWireCube(new Vector3(transform.position.x+offSetCentroAtaquePlayer.x+offSetAtaqueAbaixada.x,transform.position.y
            +offSetCentroAtaquePlayer.y+offSetAtaqueAbaixada.y,transform.position.z),dimensoesAtaque);
        }
        else
        {
            Gizmos.DrawWireCube(new Vector3(transform.position.x-offSetCentroAtaquePlayer.x-offSetAtaqueAbaixada.x,transform.position.y
            +offSetCentroAtaquePlayer.y+offSetAtaqueAbaixada.y,transform.position.z),dimensoesAtaque);
        }
        Gizmos.color=Color.white;
    }

    void OnEnable()
    {
        informacoesPlayer.EventosMorte+=Morrer;
    }
    void OnDisable()
    {
        informacoesPlayer.EventosMorte-=Morrer;
    }

    public void AtivarEfeitosEspada()
    {
        controladorEfeitosEspadaKaede.AtivarTrails();
    }
    public void DesativarEfeitosEspada()
    {
        controladorEfeitosEspadaKaede.DesativarTrails();
    }

    public void instanciarObjeto(GameObject objeto,Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(objeto,posicao,rotacao);
    }

    public void Atacar()
    {
        MensageiroDeEntradaDeTriggerDanoPlayerInimigo mensageiroDeEntradaDeTriggerDanoPlayerInimigo;
        //Debug.Log("a");
        if(olhandoParaDireita)
        {
            colisoresAtaque=Physics2D.OverlapBoxAll(new Vector2(transform.position.x,transform.position.y)+offSetCentroAtaquePlayer,dimensoesAtaque,0);
        }
        else
        {
            colisoresAtaque=Physics2D.OverlapBoxAll(new Vector2(transform.position.x,transform.position.y)-offSetCentroAtaquePlayer,dimensoesAtaque,0);
        }
        foreach(Collider2D c in colisoresAtaque)
        {
            mensageiroDeEntradaDeTriggerDanoPlayerInimigo=c.gameObject.GetComponent<MensageiroDeEntradaDeTriggerDanoPlayerInimigo>();
            if(mensageiroDeEntradaDeTriggerDanoPlayerInimigo!=null)
            {
                mensageiroDeEntradaDeTriggerDanoPlayerInimigo.LevarDano(informacoesPlayer.GetDanoAtaqueBasico);
            }
        }
    }

    public void AtacarAbaixada()
    {
        MensageiroDeEntradaDeTriggerDanoPlayerInimigo mensageiroDeEntradaDeTriggerDanoPlayerInimigo;
        //Debug.Log("a");
        if(olhandoParaDireita)
        {
            colisoresAtaque=Physics2D.OverlapBoxAll(new Vector2(transform.position.x+offSetAtaqueAbaixada.x,transform.position.y
            +offSetAtaqueAbaixada.y)+offSetCentroAtaquePlayer,dimensoesAtaque,0);
        }
        else
        {
            colisoresAtaque=Physics2D.OverlapBoxAll(new Vector2(transform.position.x-offSetAtaqueAbaixada.x,transform.position.y
            +offSetAtaqueAbaixada.y)-offSetCentroAtaquePlayer,dimensoesAtaque,0);
        }
        foreach(Collider2D c in colisoresAtaque)
        {
            mensageiroDeEntradaDeTriggerDanoPlayerInimigo=c.gameObject.GetComponent<MensageiroDeEntradaDeTriggerDanoPlayerInimigo>();
            if(mensageiroDeEntradaDeTriggerDanoPlayerInimigo!=null)
            {
                mensageiroDeEntradaDeTriggerDanoPlayerInimigo.LevarDano(informacoesPlayer.GetDanoAtaqueBasico);
            }
        }
    }
    public void ReceberDano(float quantidadeDeDano)
    {
        informacoesPlayer.ReceberDano(quantidadeDeDano);
    }
    public void Curar(float quantidadeDeCura)
    {
        informacoesPlayer.Curar(quantidadeDeCura);
    }

    public void Morrer()
    {
        rb.sharedMaterial=materialFisicoParado;
        rb.velocity=new Vector2(0,rb.velocity.y);
        controldadorDeCenaPlayer.TrocarEstadoAtual(ControladorDeCena.TipoEstadoCena.morreu);
    }

    public void TrocaEstadoPlayer(EstadoBasePlayer novoEstado)
    {
        estadoPlayerAtual.FinalizarEstado();
        estadoPlayerAtual=novoEstado;
        estadoPlayerAtual.IniciarEstadoPlayer(this);
    }
    public IEnumerator TrocaEstadoPlayerCorrotina(EstadoBasePlayer novoEstado,float tempo)
    {
        yield return new WaitForSeconds(tempo);
        TrocaEstadoPlayer(novoEstado);
    }
    public void RodarPersonagem(bool olhandoParaDireita)
    {
        if(olhandoParaDireita)
        {
            rotacaoAlvo.y=270;
            this.olhandoParaDireita=true;
        }
        else
        {
            rotacaoAlvo.y=90;
            this.olhandoParaDireita=false;
        }
    }

    public void AtivarEventoInicioAnimacao()
    {
        estadoPlayerAtual.EventoInicioAnimacao();
    }

    public void AtivarEventoAnimacao()
    {
        estadoPlayerAtual.EventoAnimacao();
    }
    public void AtivarEventoFinalAnimacao()
    {
        estadoPlayerAtual.EventoFinalAnimacao();
    }

    public void TrocarAnimPlayer(EstadosAnimacao novoEstado)
    {
        StopCoroutine("TrocarAnimPlayerCorrotina");
        anim.SetInteger("EstadoAnim",(int)novoEstado);
    }

    public IEnumerator TrocarAnimPlayerCorrotina(EstadosAnimacao novoEstado,float tempo)
    {
        yield return new WaitForSeconds(tempo);
        TrocarAnimPlayer(novoEstado);
    }

    public PhysicsMaterial2D GetMaterialFisicoParado=> materialFisicoParado;
    public PhysicsMaterial2D GetMaterialFisicoAndando=>materialFisicoAndando;
    public MapeadorDeBotoes GetMapeadorDeBotoes=>mapeadorDeBotoes;
    public Rigidbody2D GetRigidbody2D=>rb;
    public CapsuleCollider2D GetCapsuleCollider2D=>col;
    public float GetVelocidadeDeMovimento=>velocidadeDeMovimento;
    public float GetForcaPulo=>forcaPulo;
    public bool GetOlhandoParaDireita=>olhandoParaDireita;
    public float GetPerdaDeMovimentoNoAr=>perdaDeMovimentoNoAr;
    public List<RaycastHit2D> GetRaycastsPulo=>raycastsPulo;
    public float GetDistanciaChecagemPulo=>distanciaChecagemPulo;
    public float GetVidaCuradaPorColisaoParticula=>vidaCuradaPorColisaoParticula;
    public InformacoesPlayer GetInformacoesPlayer=>informacoesPlayer;
    public Transform GetTransformPosicaoInstanciaSubItem=>transformPosicaoInstanciaSubItem;
    public Animator GetAnimator=>anim;
}
