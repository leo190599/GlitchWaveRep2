using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensageiroColisaoParticulaPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private ScriptPlayer sP;
    void OnParticleCollision(GameObject other)
    {
        sP.Curar(sP.GetVidaCuradaPorColisaoParticula);
    }
}
