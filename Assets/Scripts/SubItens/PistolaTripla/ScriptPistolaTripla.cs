using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPistolaTripla : MonoBehaviour
{
    [SerializeField]
    private GameObject raizPrefab;
    [SerializeField]
    private GameObject localDeTiro;
    [SerializeField]
    private GameObject balaPrefab;
    [SerializeField]
    private AudioSource emissorDeAudioBala;
    
    public void Atirar()
    {
        if(raizPrefab.transform.localScale.x>0)
        {
            Instantiate(balaPrefab, localDeTiro.transform.position, Quaternion.Euler(0, 0, 0));
        }
        else
        {
            Instantiate(balaPrefab, localDeTiro.transform.position, Quaternion.Euler(0, 0, 180));
        }
        emissorDeAudioBala.Play();
    }
    public void DestruirPisTola()
    {
        Destroy( raizPrefab );
    }
}
