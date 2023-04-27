using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptInimigoBroca : InimigoBase
{
    [SerializeField]
    private float vel=1;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
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
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(new Vector2(transform.position.x+Mathf.Cos(transform.eulerAngles.z*Mathf.Deg2Rad)*vel*Time.fixedDeltaTime,
        transform.position.y+Mathf.Sin(transform.eulerAngles.z*Mathf.Deg2Rad)*vel*Time.fixedDeltaTime));
    }
}
