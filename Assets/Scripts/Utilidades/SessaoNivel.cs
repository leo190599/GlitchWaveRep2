using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessaoNivel : MonoBehaviour
{
    [SerializeField]
    private Vector2 areaDaCameraPrevistaNoLocal;
    [SerializeField]
    private string tagAColidir;
    [SerializeField]
    private GameObject paiDaSubCena;
    [SerializeField]
    private GameObject[] objetosASairemDaSubCenaAoIniciar;
    private bool ativadoPrimeiraVez=false;

    private void Start()
    {
        paiDaSubCena.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == tagAColidir)
        {
            paiDaSubCena.SetActive(true);
            if (!ativadoPrimeiraVez)
            {
                foreach (GameObject g in objetosASairemDaSubCenaAoIniciar)
                {
                    if (g != null)
                    {
                        g.transform.parent = null;
                    }
                }
                ativadoPrimeiraVez = true;
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(areaDaCameraPrevistaNoLocal.x,areaDaCameraPrevistaNoLocal.y,1));
    }

}
