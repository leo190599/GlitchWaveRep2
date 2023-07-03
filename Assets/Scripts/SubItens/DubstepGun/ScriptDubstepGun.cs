using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDubstepGun : MonoBehaviour
{
    [SerializeField]
    private float tempoDeVida=3;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,tempoDeVida);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
