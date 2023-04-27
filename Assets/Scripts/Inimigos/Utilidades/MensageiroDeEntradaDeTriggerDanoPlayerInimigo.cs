using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensageiroDeEntradaDeTriggerDanoPlayerInimigo : MonoBehaviour
{
    [SerializeField]
    protected InimigoBase inimigo;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("a");
    }
}
