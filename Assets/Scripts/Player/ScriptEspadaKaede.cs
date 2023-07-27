using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEspadaKaede : MonoBehaviour
{
    [SerializeField]
    private GameObject localizadorEspada1;
    [SerializeField]
    private GameObject localizadorEspada2;
    [SerializeField]
    private float proundidadeObjeto=1;
    [SerializeField]
    private Rigidbody2D objetoAMovimentar;
    void FixedUpdate()
    {
        objetoAMovimentar.MovePosition(new Vector3(localizadorEspada1.transform.position.x,localizadorEspada1.transform.position.y,proundidadeObjeto));
        objetoAMovimentar.MoveRotation(Quaternion.Euler(0,0,Mathf.Atan2(localizadorEspada2.transform.position.y-localizadorEspada1.transform.position.y,
        localizadorEspada2.transform.position.x-localizadorEspada1.transform.position.x)*Mathf.Rad2Deg));
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(localizadorEspada1.transform.position.x,localizadorEspada1.transform.position.y,proundidadeObjeto),
        new Vector3(localizadorEspada2.transform.position.x,localizadorEspada2.transform.position.y,proundidadeObjeto));
    }
}
