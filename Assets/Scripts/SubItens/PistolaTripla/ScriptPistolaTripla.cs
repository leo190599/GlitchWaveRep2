using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPistolaTripla : MonoBehaviour
{
    [SerializeField]
    private GameObject raizPrefab;
    [SerializeField]
    private GameObject localDeTiro;
    
    public void Atirar()
    {
        if(raizPrefab.transform.localScale.x>0)
        {
            Debug.Log("a");
        }
        else
        {
            Debug.Log("b");
        }
    }
    public void DestruirPisTola()
    {
        Destroy( raizPrefab );
    }
}
