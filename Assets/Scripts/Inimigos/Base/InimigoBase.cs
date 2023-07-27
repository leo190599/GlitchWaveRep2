using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoBase : MonoBehaviour
{
    [SerializeField]
    protected float vida=100;
    [SerializeField]
    protected float vidaMaxima=100;
    [SerializeField]
    protected float dano=10;
    // Start is called before the first frame update
    public virtual void LevarDano(float quantidadeDeDano)
    {
        vida-=quantidadeDeDano;
        Debug.Log("A");
        if(vida<=0)
        {
            Morrer();
        }
    }

    public virtual void CausarDano(ScriptPlayer player)
    {

    }

    public virtual void Morrer()
    {
        Destroy(gameObject);
    }

    public float GetVida=>vida;
    public float GetVidaMaxima=>vidaMaxima;
}
