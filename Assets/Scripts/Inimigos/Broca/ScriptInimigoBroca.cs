using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptInimigoBroca : InimigoBase
{
    [SerializeField]
    private float vel=1;
    [SerializeField]
    public GameObject particulasMorte;
    Rigidbody2D rb;
    [SerializeField]
    private ControladorShaderInimigoBroca controladorShaderInimigoBroca;
    // Start is called before the first frame update
    void Start()
    {
        vida=vidaMaxima;
        rb=GetComponent<Rigidbody2D>();
        if(rb==null)
        {
            Debug.LogError("Coloque o componente em uma broca que possua um Rigdbody2d");

        }
    }

    public override void CausarDano(ScriptPlayer player)
    {
        base.CausarDano(player);
        player.ReceberDano(dano);
    }

    public override void LevarDano(float quantidadeDeDano)
    {
        base.LevarDano(quantidadeDeDano);
        controladorShaderInimigoBroca.IniciarEfeitoDano();
    }
    public override void Morrer()
    {
        Instantiate(particulasMorte,transform.position,Quaternion.identity);
        base.Morrer();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(new Vector2(transform.position.x+Mathf.Cos(transform.eulerAngles.z*Mathf.Deg2Rad)*vel*Time.fixedDeltaTime,
        transform.position.y+Mathf.Sin(transform.eulerAngles.z*Mathf.Deg2Rad)*vel*Time.fixedDeltaTime));
    }
}
