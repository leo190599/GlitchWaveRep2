using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensageiroDeEntradaDeTriggerDanoInimigoPlayer : MonoBehaviour
{
    [SerializeField]
    protected InimigoBase inimigo;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D c)
    {
        ScriptPlayer sP=c.GetComponent<ScriptPlayer>();
        if(sP!=null)
        {
            inimigo.CausarDano(sP);
        }
    }
}
