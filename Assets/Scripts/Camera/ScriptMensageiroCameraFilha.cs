using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptMensageiroCameraFilha : MonoBehaviour
{
    [SerializeField] 
    private ScriptCamera objetoPai;
    public void RetornarAoIdle()
    {
        objetoPai.AnimarCameraIdle();
    }
}
