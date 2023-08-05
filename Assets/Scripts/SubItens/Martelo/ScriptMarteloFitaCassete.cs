using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptMarteloFitaCassete : MonoBehaviour
{
    [SerializeField]
    private float dano=5;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D c)
    {
         MensageiroDeEntradaDeTriggerDanoPlayerInimigo mensageiroDeEntradaDeTriggerDanoPlayerInimigo=c.gameObject.GetComponent<MensageiroDeEntradaDeTriggerDanoPlayerInimigo>();
        if(mensageiroDeEntradaDeTriggerDanoPlayerInimigo!=null)
        {
            mensageiroDeEntradaDeTriggerDanoPlayerInimigo.LevarDano(dano);
        }
    }
    public void DestruirMartelo()
    {
        Destroy(gameObject);
    }
}
