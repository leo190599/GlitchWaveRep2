using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessaoNivel : MonoBehaviour
{
    [SerializeField]
    private string tagAColidir;
    [SerializeField]
    private GameObject paiDaSubCena;
    [SerializeField]
    private GameObject[] objetosASairemDaSubCenaAoIniciar;

    private void Start()
    {
        paiDaSubCena.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == tagAColidir)
        {
            paiDaSubCena.SetActive(true);
            foreach(GameObject g in objetosASairemDaSubCenaAoIniciar)
            {
                g.transform.parent = null;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D c)
    {
        if(c.tag==tagAColidir)
        {
            paiDaSubCena.SetActive(false);
        }
    }

}
