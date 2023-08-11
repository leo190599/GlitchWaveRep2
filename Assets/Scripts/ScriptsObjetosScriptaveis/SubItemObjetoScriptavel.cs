using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="novoSubItemObjetoScriptavel",menuName ="ScriptableObjectsCustomizados/SubItem")]
public class SubItemObjetoScriptavel : ScriptableObject
{
    [SerializeField]
    // Start is called before the first frame update
    private GameObject prefabSubItem;
    [SerializeField]
    private float custoDeVida=1;
    [SerializeField]
    private GameObject prefabColetavel;

    public float GetCustoDeVida=>custoDeVida;
    public GameObject GetPrefabSubItem=>prefabSubItem;
    public GameObject GetPrefabColetavel => prefabColetavel;
}
