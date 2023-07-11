using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorEfeitosEspadaKaede : MonoBehaviour
{
    [SerializeField]
    private TrailRenderer[] trailsEspada;

    public void AtivarTrails()
    {
        foreach(TrailRenderer t in trailsEspada)
        {
            t.gameObject.SetActive(true);
        }
    }
    public void DesativarTrails()
    {
        foreach(TrailRenderer t in trailsEspada)
        {
            t.Clear();
            t.gameObject.SetActive(false);
        }
    }
}
