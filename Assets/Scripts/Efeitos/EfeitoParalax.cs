using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoParalax : MonoBehaviour
{
    [Header("Parametros de design")]
    [SerializeField]
    private float velHorizontal;
    [SerializeField]
    private float velVertical;
    [Header("Parametros de debug")]
    [SerializeField]
    private Vector3 offsetComACamera;
    [SerializeField]
    private Transform cameraASeguir;
    [SerializeField]
    private Material mat;
    [SerializeField]
    private Vector3 antigaPosicaoCamera;
    private Vector2 novoOffset;
    private Vector3 novaTransformadaEmRelacaoACamera;
    private Vector3 primeiraPosicaoMesh;

    // Start is called before the first frame update
    void Start()
    {
        novaTransformadaEmRelacaoACamera=new Vector3();

        primeiraPosicaoMesh=transform.position;

        cameraASeguir=Camera.main.transform;
        offsetComACamera=transform.position-cameraASeguir.transform.position;
        antigaPosicaoCamera=cameraASeguir.position;
        mat=GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculo do offset do material
        novoOffset=mat.GetTextureOffset("_BaseMap");
        novoOffset.x=novoOffset.x+(cameraASeguir.transform.position.x-antigaPosicaoCamera.x)*velHorizontal;

        novoOffset.x=novoOffset.x%1;

        //Calculo da nova posicao da mesh que contem o material de fundo
        antigaPosicaoCamera=cameraASeguir.transform.position;

        novaTransformadaEmRelacaoACamera.x=cameraASeguir.position.x+offsetComACamera.x;
        novaTransformadaEmRelacaoACamera.y=primeiraPosicaoMesh.y+(cameraASeguir.transform.position.y+offsetComACamera.y-primeiraPosicaoMesh.y)*velVertical;
        novaTransformadaEmRelacaoACamera.z=cameraASeguir.position.z+offsetComACamera.z;

        //aplicacao dos dois valores
        transform.position=novaTransformadaEmRelacaoACamera;

        mat.SetTextureOffset("_BaseMap",novoOffset);
    }
}
