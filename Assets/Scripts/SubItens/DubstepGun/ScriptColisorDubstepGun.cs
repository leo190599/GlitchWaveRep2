using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptColisorDubstepGun : MonoBehaviour
{
    [SerializeField]
    private ScriptDubstepGun dubstepGun;
    void OnTriggerEnter2D(Collider2D c)
    {
        //Debug.Log(c);
        MensageiroDeEntradaDeTriggerDanoPlayerInimigo mensageiroDeEntradaDeTriggerDanoPlayerInimigo=c.gameObject.GetComponent<MensageiroDeEntradaDeTriggerDanoPlayerInimigo>();
        if(mensageiroDeEntradaDeTriggerDanoPlayerInimigo!=null)
        {
            dubstepGun.CausarDano(mensageiroDeEntradaDeTriggerDanoPlayerInimigo);
        }
    }
}
