using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptRobo : InimigoBase
{
    // Start is called before the first frame update

    private BoxCollider2D colisorPai;
    private float direcaoMovimento;
    private Rigidbody2D rb;
    private estado estadoAtual=estado.idle;

    [SerializeField]
    private GameObject meshRobo;
    [SerializeField]
    private ScriptMeshRobo controladorMeshRobo;
  
    [SerializeField]
    private GameObject playerAlvo=null;

    private IEnumerator corrotinaPreparoAtaque;
    private IEnumerator corrotinaAtaque;
    [SerializeField]
    private GameObject triggerCausadorDeDano;
    [SerializeField]
    private GameObject particulasVida;
    [SerializeField]
    private GameObject efeitosParticulasAtaque;

    [Header("Parametros de desing")]

    [SerializeField]
    private float vel;
    [SerializeField]
    private float distanciaPlayerParaAtacar = .5f;
    [SerializeField]
    private float tempoPreparoAtaque = 1;
    [SerializeField]
    private float tempoEmAtaque = 1;
    [SerializeField]
    private float velAtaque=5;

    private enum estado
    {
        idle=0,
        perseguicao=1,
        prepararAtaque=2,
        ataque=3
    }
    void Start()
    {
        direcaoMovimento=Random.Range(0,100)>49?-1:1;
        vida = vidaMaxima;
        colisorPai=GetComponentInParent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        triggerCausadorDeDano.SetActive(false);
        efeitosParticulasAtaque.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        switch (estadoAtual)
        {
            case estado.idle:
                if(transform.position.x<=colisorPai.bounds.min.x)
                {
                    direcaoMovimento = 1;
                }
                else if(transform.position.x >= colisorPai.bounds.max.x)
                {
                    direcaoMovimento = -1;
                }
                rb.MovePosition(new Vector2(Mathf.Clamp(transform.position.x + vel * direcaoMovimento, colisorPai.bounds.min.x, colisorPai.bounds.max.x), transform.position.y));
                if(playerAlvo!=null)
                {
                    EntrarEmEstadoDePerseguicao();
                }
            break;
            case estado.perseguicao:
                if (playerAlvo == null)
                {
                    EntrarEmEstadoIdle();
                    return;
                }
                else
                {
                    if (playerAlvo.transform.position.x>transform.position.x)
                    {
                        direcaoMovimento=1;
                    }
                    else
                    {
                        direcaoMovimento=-1;
                    }
                    if (Mathf.Abs(playerAlvo.transform.position.x - transform.position.x) > distanciaPlayerParaAtacar)
                    {
                        rb.MovePosition(new Vector2(Mathf.Clamp(transform.position.x + vel * direcaoMovimento, colisorPai.bounds.min.x, colisorPai.bounds.max.x), transform.position.y));
                    }
                    else
                    {
                        if (ChecarSeUmAtaqueValeAPena())
                        {
                            EntrarEmEstadoPreparoAtaque();
                        }
                    }
                }
                break;
            case estado.prepararAtaque:

                break;

            case estado.ataque:
                rb.MovePosition(new Vector2(Mathf.Clamp(transform.position.x + velAtaque * direcaoMovimento, colisorPai.bounds.min.x, colisorPai.bounds.max.x), transform.position.y));
                if(transform.position.x>=colisorPai.bounds.max.x||transform.position.x<=colisorPai.bounds.min.x)
                {
                    if(playerAlvo==null)
                    {
                        EntrarEmEstadoIdle();
                    }
                    else
                    {
                        EntrarEmEstadoDePerseguicao();
                    }
                }
                break;
        }
        meshRobo.transform.eulerAngles = new Vector3(0,Mathf.Lerp(meshRobo.transform.eulerAngles.y,180-(90*direcaoMovimento),0.5f),0);
    }

    private bool ChecarSeUmAtaqueValeAPena()
    {
        if((direcaoMovimento>0 && transform.position.x<colisorPai.bounds.max.x)||(direcaoMovimento<0&&transform.position.x>colisorPai.bounds.min.x))
        {
            return true;
        }
        return false;
    }


    public override void CausarDano(ScriptPlayer player)
    {
        base.CausarDano(player);
        player.ReceberDano(dano);
    }
    public override void LevarDano(float quantidadeDeDano)
    {
        base.LevarDano(quantidadeDeDano);
        controladorMeshRobo.AtivarEfeitoDano();
    }
    public override void Morrer()
    {
        Instantiate(particulasVida,transform.position,Quaternion.identity);
        Destroy(colisorPai.gameObject);
    }
    public void EntrarEmEstadoIdle()
    {
        estadoAtual = estado.idle;
        controladorMeshRobo.TrocarAnim(ScriptMeshRobo.EstadosAnimacao.idle);
        controladorMeshRobo.DesativarTriggerShader();
        triggerCausadorDeDano.SetActive(false);
        efeitosParticulasAtaque.SetActive(false);
    }
    public void EntrarEmEstadoDePerseguicao()
    {
        estadoAtual = estado.perseguicao;
        controladorMeshRobo.TrocarAnim(ScriptMeshRobo.EstadosAnimacao.idle);
        controladorMeshRobo.AtivarTriggerShader();
        triggerCausadorDeDano.SetActive(false);
        efeitosParticulasAtaque.SetActive(false);

    }
    public void EntrarEmEstadoPreparoAtaque()
    {
        estadoAtual = estado.prepararAtaque;
        controladorMeshRobo.TrocarAnim(ScriptMeshRobo.EstadosAnimacao.preparandoAtaque);
        corrotinaPreparoAtaque = CorrotinaPreparoAtaque(tempoPreparoAtaque);
        StartCoroutine(corrotinaPreparoAtaque);
        triggerCausadorDeDano.SetActive(false);
        efeitosParticulasAtaque.SetActive(true);
    }
    IEnumerator CorrotinaPreparoAtaque(float tempoParaAtaque)
    {
        yield return new WaitForSeconds(tempoParaAtaque);
        corrotinaPreparoAtaque = null;
        EntrarEstadoAtaque();
    }
    public void EntrarEstadoAtaque()
    {
        estadoAtual = estado.ataque;
        controladorMeshRobo.TrocarAnim(ScriptMeshRobo.EstadosAnimacao.ataque);
        corrotinaAtaque = CorrotinaAtaque(tempoEmAtaque);
        triggerCausadorDeDano.SetActive(true);
        efeitosParticulasAtaque.SetActive(false);
        StartCoroutine(corrotinaAtaque);
    }
    IEnumerator CorrotinaAtaque(float tempoNoEstadoAtaque)
    {
        yield return new WaitForSeconds(tempoNoEstadoAtaque);
        corrotinaAtaque = null;
        if(playerAlvo==null)
        {
            EntrarEmEstadoIdle();
        }
        else
        {
            EntrarEmEstadoDePerseguicao();
        }
    }
    public void SetPlayerAlvo(GameObject player)
    {
        playerAlvo = player;
    }
}
