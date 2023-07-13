using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensageiroDeEntradaDeTriggerDanoInimigoPlayer : MonoBehaviour
{
    [SerializeField]
    protected InimigoBase inimigo;
    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D c)
    {
        ScriptPlayer sP=c.GetComponentInParent<ScriptPlayer>();
        if(sP!=null)
        {
            inimigo.CausarDano(sP);
        }
    }
}
