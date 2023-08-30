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
    private Vector2 forcaAplicadaAoEntrarNoEstadoDeTomarDano;
    [SerializeField]
    private float tempoDeInvencibilidadeAoTomarDano=1;
    [SerializeField]
   // private float danoRecebidoGlitch=.5f;
    //[SerializeField]
    private float tempoDash=1;
    [SerializeField]
    private float velDash=5;
    [Header("Audios")]
    [SerializeField]
    private AudioClip audioAtaqueBase;

    [Header("Parametros Debug")]
    [SerializeField]
    private bool glitchAtivo=false;
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
    [SerializeField]
    private GameObject colisorRecebimentoDeDano;
    [SerializeField]
    private GameObject colisorRecebimentoDeDanoAbaixada;
    [SerializeField]
    private MensageiroPlayerShaderPersonagem mensageiroPlayerShaderPersonagem;
    private bool InvencibilidadeAtiva=false;
    private List<RaycastHit2D>raycastsPulo;
    private Collider2D[] colisoresAtaque;
    private IEnumerator corrotinaReceberDanoGlitch;
    private EstadoBasePlayer estadoPlayerAtual;
    [SerializeField]
    private LayerMask layerChao;
    [SerializeField]
    private BoxCollider2D colisorEspada;
    private bool dashDireita=false;
    private IEnumerator corrotinaEstado;
    [SerializeField]
    private TrailRenderer trailDash;
    private bool dashDadoNoAr=false;
    [SerializeField]
    private AudioSource emissorDeAudio;
    

    [Header("Scriptable objects")]
    [SerializeField]
    private ControladorDeCena controldadorDeCenaPlayer;
    [SerializeField]
    private MapeadorDeBotoes mapeadorDeBotoes;
    
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
        levandoDano=8,
        morte=9,
        dashFrente=10,
        dashTras=11,
        puloUsoSubItem=12
    }
    void Start()
    {
        controldadorDeCenaPlayer.TrocarEstadoAtual(ControladorDeCena.TipoEstadoCena.jogando);
        informacoesPlayer.EncherVida();
        raycastsPulo=new List<RaycastHit2D>();
        rb=GetComponent<Rigidbody2D>();
        col=GetComponent<CapsuleCollider2D>();
        DesativarColisorRecebimentoDeDanoAbaixada();
        //teste
        rotacaoAlvo=meshPersonagem.transform.eulerAngles;
        //Application.targetFrameRate=1;
        informacoesPlayer.SetSubItemObjetoScriptavel(null);
        //teste
        trailDash.gameObject.SetActive(false);

        DesativarColisorEspada();
        informacoesPlayer.InvocarEventosDesativacaoGlitch();

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
            if(Input.GetKeyDown(mapeadorDeBotoes.GetBotaoGlitch))
            {
                AlternarGlitch();
            }
            if(estadoPlayerAtual is EstadoAtivoBasePlayer && !dashDadoNoAr)
            {
                if(Input.GetKeyDown(mapeadorDeBotoes.GetBotaoDashDireita))
                {
                    dashDireita=true;
                    if(estadoPlayerAtual is EstadoNoArBasePlayer)
                    {
                        dashDadoNoAr=true;
                    }
                    TrocaEstadoPlayer(new EstadoDashPlayer());
                    corrotinaEstado=CorrotinaEstadoPlayer(tempoDash);
                    StartCoroutine(corrotinaEstado);
                }
                else if(Input.GetKeyDown(mapeadorDeBotoes.GetBotaoDashEsquerda))
                {
                    dashDireita=false;
                    if(estadoPlayerAtual is EstadoNoArBasePlayer)
                    {
                        dashDadoNoAr=true;
                    }
                    TrocaEstadoPlayer(new EstadoDashPlayer());
                    corrotinaEstado=CorrotinaEstadoPlayer(tempoDash);
                    StartCoroutine(corrotinaEstado);
                }
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
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else if(controldadorDeCenaPlayer.getEstadoCena==ControladorDeCena.TipoEstadoCena.venceu)
        {
            if(Input.GetKeyDown(mapeadorDeBotoes.GetBotaoPause))
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        //Debug.Log(estadoPlayerAtual);
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

    public void instanciarObjeto(GameObject objeto,Vector3 posicao, Quaternion rotacao,Vector3 escala)
    {
        GameObject instancia;
        instancia=Instantiate(objeto,posicao,rotacao);
        instancia.transform.localScale=escala;
    }

    public void AtivarColisorEspada()
    {
        colisorEspada.enabled=true;
    }
    public void DesativarColisorEspada()
    {
        StartCoroutine(CorrotinaDesativarEspada());
    }

    private IEnumerator CorrotinaDesativarEspada()
    {
        yield return new WaitForFixedUpdate();
        colisorEspada.enabled=false;
    }

    public void AtivarColisorRecebimentoDeDanoAbaixada()
    {
        colisorRecebimentoDeDanoAbaixada.SetActive(true);
        colisorRecebimentoDeDano.SetActive(false);
    }

    public void DesativarColisorRecebimentoDeDanoAbaixada()
    {
        colisorRecebimentoDeDanoAbaixada.SetActive(false);
        colisorRecebimentoDeDano.SetActive(true);
    }

    public void ReceberDano(float quantidadeDeDano)
    {
        if(!InvencibilidadeAtiva && !glitchAtivo && controldadorDeCenaPlayer.getEstadoCena==ControladorDeCena.TipoEstadoCena.jogando)
        {
            informacoesPlayer.ReceberDano(quantidadeDeDano);
            if(informacoesPlayer.GetVidaAtual>0)
            {
                TrocaEstadoPlayer(new EstadoTomouDanoPlayer());
                AtivarInvencibilidade();
            }
            else
            {
                anim.updateMode=AnimatorUpdateMode.UnscaledTime;
                TrocarAnimPlayer(EstadosAnimacao.morte);
            }
        }
    }

    public void AtivarInvencibilidade()
    {
        InvencibilidadeAtiva=true;
        mensageiroPlayerShaderPersonagem.setEfeitoInvencibilidade(true);
        StartCoroutine(DesativarInvencibilidadeCorrotina());
    }

    public void AlternarGlitch()
    {
        if(glitchAtivo)
        {
            DesativarGlitch();
        }
        else
        {
            AtivarGlitch();
        }
    }

    public void AtivarGlitch()
    {
        if(informacoesPlayer.GetVidaAtual>informacoesPlayer.GetDanoGlitchPlayer)
        {
            glitchAtivo=true;
            informacoesPlayer.InvocarEventosAtivacaoGlitch();
            mensageiroPlayerShaderPersonagem.setEfeitoGlitch(true);
            corrotinaReceberDanoGlitch=ReceberDanoGlitch();
            StartCoroutine(corrotinaReceberDanoGlitch);
        }
    }
    public void DesativarGlitch()
    {
        glitchAtivo=false;
        informacoesPlayer.InvocarEventosDesativacaoGlitch();
        mensageiroPlayerShaderPersonagem.setEfeitoGlitch(false);
        StopCoroutine(corrotinaReceberDanoGlitch);
    }
    public IEnumerator DesativarInvencibilidadeCorrotina()
    {
        yield return new WaitForSeconds(tempoDeInvencibilidadeAoTomarDano);
        InvencibilidadeAtiva=false;
        mensageiroPlayerShaderPersonagem.setEfeitoInvencibilidade(false);
    }
    public IEnumerator ReceberDanoGlitch()
    {
        while(true)
        {
            informacoesPlayer.ReceberDano(informacoesPlayer.GetDanoGlitchPlayer);
            if(informacoesPlayer.GetVidaAtual<=informacoesPlayer.GetDanoGlitchPlayer)
            {
                informacoesPlayer.InvocarEventosDesativacaoGlitch();
                glitchAtivo=false;
                mensageiroPlayerShaderPersonagem.setEfeitoGlitch(false);
                StopCoroutine(corrotinaReceberDanoGlitch);
            }
            yield return new WaitForSeconds(informacoesPlayer.GetIntervaloTempoRecebimentoDeDanoGlitch);
        }
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
        if(corrotinaEstado!=null)
        {
            StopCoroutine(corrotinaEstado);
        }
        estadoPlayerAtual.FinalizarEstado();
        estadoPlayerAtual=novoEstado;
        estadoPlayerAtual.IniciarEstadoPlayer(this);
    }
    public IEnumerator TrocaEstadoPlayerCorrotina(EstadoBasePlayer novoEstado,float tempo)
    {
        yield return new WaitForSeconds(tempo);
        TrocaEstadoPlayer(novoEstado);
    }
    public IEnumerator CorrotinaEstadoPlayer(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        estadoPlayerAtual.EventoCorrotinaEstado();
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
    public void TocarAudio(AudioClip clipATocar)
    {
        emissorDeAudio.Stop();
        emissorDeAudio.clip=clipATocar;
        emissorDeAudio.Play();
        //Debug.Log("a");
    }
    public void SetDadoDashNoAr(bool novoValor)
    {
        dashDadoNoAr=novoValor;
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
    public Vector2 GetForcaAplicadaAoEntrarNoEstadoDeTomarDano=>forcaAplicadaAoEntrarNoEstadoDeTomarDano;
    public LayerMask GetLayerChao=>layerChao;
    public bool GetDashDireita=>dashDireita;
    public float GetVelDash=>velDash;
    public TrailRenderer GetTrailDash=>trailDash;
    public bool GetDashDadoNoAr=>dashDadoNoAr;
    //Getters audios
    public AudioClip GetAudioAtaqueBase=>audioAtaqueBase;
}
