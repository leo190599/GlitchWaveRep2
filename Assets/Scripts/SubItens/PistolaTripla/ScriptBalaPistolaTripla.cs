using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBalaPistolaTripla : MonoBehaviour
{
    [SerializeField]
    private float tempoDeVida = 1;
    [SerializeField]
    private float vel=5;
    [SerializeField]
    private float dano = 5;
    [SerializeField]
    private Rigidbody2D rb;
    private MensageiroDeEntradaDeTriggerDanoPlayerInimigo mensageiroDeEntradaDeTriggerDanoPlayerInimigo;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, tempoDeVida);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position+transform.right*vel);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        mensageiroDeEntradaDeTriggerDanoPlayerInimigo = c.GetComponent<MensageiroDeEntradaDeTriggerDanoPlayerInimigo>();
        if(mensageiroDeEntradaDeTriggerDanoPlayerInimigo != null )
        {
            mensageiroDeEntradaDeTriggerDanoPlayerInimigo.LevarDano(dano);
            Destroy(gameObject);
        }
    }
}
