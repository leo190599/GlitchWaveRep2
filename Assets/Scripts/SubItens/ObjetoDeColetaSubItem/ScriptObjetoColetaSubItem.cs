using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptObjetoColetaSubItem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private SubItemObjetoScriptavel subItem;
    [SerializeField]
    private GameObject centroVisualColeta;

    private void Start()
    {
        if(subItem != null)
        {
            if (subItem.GetPrefabColetavel != null)
            {
                GameObject visualSubItem = Instantiate(subItem.GetPrefabColetavel, centroVisualColeta.transform);
                visualSubItem.transform.parent = centroVisualColeta.transform;
            }
            else
            {
                Debug.LogWarning("O subItem nao possui prefab visual");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag=="Player")
        {
            if(subItem!=null)
            {
                c.gameObject.GetComponent<ScriptPlayer>().GetInformacoesPlayer.SetSubItemObjetoScriptavel(subItem);
            }
            else
            {
                Debug.LogError("Nao ha um subItem para colocar");
            }
            Destroy(gameObject);
        }
    }
}
