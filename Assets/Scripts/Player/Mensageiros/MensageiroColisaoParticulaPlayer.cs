using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensageiroColisaoParticulaPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private ScriptPlayer sP;
    private List<ParticleCollisionEvent> colisoes;
    private int numeroDeColisoes;
    private void Awake()
    {
        colisoes = new List<ParticleCollisionEvent>();
    }
    void OnParticleCollision(GameObject other)
    {
        ParticleSystem ps = other.GetComponent<ParticleSystem>();
        if(ps.GetComponent<ParticleSystem>() != null)
        {
            numeroDeColisoes=ParticlePhysicsExtensions.GetCollisionEvents(ps,this.gameObject,colisoes);
            for(int i=0;i<numeroDeColisoes;i++)
            {
                sP.Curar(sP.GetVidaCuradaPorColisaoParticula);
            }
            colisoes.Clear();
        }
        
    }
}
