using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ScriptRobo : InimigoBase
{
    // Start is called before the first frame update

    private BoxCollider2D colisorPai;
    private float direcaoMovimento;
    private Rigidbody2D rb;
    private estado estadoAtual=estado.idle;
    //private float rotacaoAlvo = 90;
    [SerializeField]
    private GameObject meshRobo;
    [SerializeField]
    private ScriptMeshRobo controladorMeshRobo;
  
    [SerializeField]
    private GameObject playerAlvo=null;
    [Header("Parametros de desing")]

    [SerializeField]
    private float vel;
    [SerializeField]
    private float distanciaPlayerParaAtacar = .5f;

    private enum estado
    {
        idle=0,
        perseguicao=1
    }
    void Start()
    {
        direcaoMovimento=Random.Range(0,100)>49?-1:1;
        vida = vidaMaxima;
        colisorPai=GetComponentInParent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
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
                    Debug.Log(transform.position.x);
                    direcaoMovimento = 1;
                }
                else if(transform.position.x >= colisorPai.bounds.max.x)
                {
                    Debug.Log(transform.position.x);
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
                }
                break;
        }
        meshRobo.transform.eulerAngles = new Vector3(0,Mathf.Lerp(meshRobo.transform.eulerAngles.y,180-(90*direcaoMovimento),0.5f),0);
    }
    public void EntrarEmEstadoIdle()
    {
        estadoAtual = estado.idle;
        controladorMeshRobo.DesativarTriggerShader();
    }
    public void EntrarEmEstadoDePerseguicao()
    {
        estadoAtual = estado.perseguicao;
        controladorMeshRobo.AtivarTriggerShader();

    }
    public void SetPlayerAlvo(GameObject player)
    {
        playerAlvo = player;
    }
}
