using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptMensagem : MonoBehaviour
{
    [SerializeField]
    private GameObject alvo;
    [SerializeField]
    private string tagAChecar="Player";
    // Start is called before the first frame update
    private void Start()
    {
        alvo.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag==tagAChecar)
        {
            alvo.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D c)
    {
        if(c.tag==tagAChecar)
        {
            alvo.SetActive(false);
        }
    }
}
