using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaioDubstepGun : MonoBehaviour
{
    private Material material;
    [SerializeField]
    private Texture2D[] texturas;
    private int indexProximaTextura=1;
    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        if(texturas.Length > 0 )
        {
            material.SetTexture("_Textura", texturas[0]);
        }
    }
    public void TrocarParaProximaTextura()
    {
        if(indexProximaTextura>=texturas.Length)
        {
            return;
        }
        material.SetTexture("_Textura", texturas[indexProximaTextura]);
        indexProximaTextura++;
    }
    // Start is called before the first frame update

}
