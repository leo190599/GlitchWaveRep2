using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorMiraALaser : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lr;
    [SerializeField]
    private float profundidade =-1;
    [SerializeField]
    private float tamanhoDaLinha=1;
    [SerializeField]
    private LayerMask lm;
    private RaycastHit2D rh;
    private Vector3 vetorProfundidade;
    // Start is called before the first frame update
    void Start()
    {
        vetorProfundidade=new Vector3(0,0,profundidade);
        lr=GetComponent<LineRenderer>();
        lr.SetPosition(0,vetorProfundidade);
    }

    // Update is called once per frame
    void Update()
    {
        rh=Physics2D.Raycast(transform.position,transform.right,tamanhoDaLinha,lm);
        if(rh)
        {
            lr.SetPosition(1,transform.InverseTransformPoint(rh.point)+vetorProfundidade);
        }
        else
        {
            lr.SetPosition(1,Vector3.right*tamanhoDaLinha+vetorProfundidade);
        }
    }
}
