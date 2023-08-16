using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptRaizPrefabRobo : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D colisor;
    [SerializeField]
    private ScriptRobo robo;

    // Start is called before the first frame update
    private void Awake()
    {
        colisor=GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public BoxCollider2D GetBoxCollider2D=>colisor;
    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.gameObject.tag=="Player")
        {
            robo.SetPlayerAlvo(c.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag=="Player")
        {
            robo.SetPlayerAlvo(null);
        }
    }
}
