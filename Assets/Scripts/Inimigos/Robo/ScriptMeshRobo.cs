using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptMeshRobo : MonoBehaviour
{

    [SerializeField]
    private SkinnedMeshRenderer malhaRobo;
    private Material material;
    //[SerializeField]
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        material=malhaRobo.material;
    }

    public void AtivarTriggerShader()
    {
        material.SetFloat("_TriggerAtivo",1);
    }
    public void DesativarTriggerShader()
    {
        material.SetFloat("_TriggerAtivo", 0);
    }
}
