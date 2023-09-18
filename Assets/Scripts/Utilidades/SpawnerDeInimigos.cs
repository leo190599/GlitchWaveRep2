using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDeInimigos : MonoBehaviour
{
    [SerializeField]
    private GameObject objetoASerSpawnado;
    [SerializeField]
    private float intervaloDeSpawn;
    private IEnumerator corrotinaSpawn;
    private void Start()
    {
        
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        corrotinaSpawn = SpawnarInimigo();
        StartCoroutine(corrotinaSpawn);
    }
    private void OnDisable()
    {
        StopCoroutine(corrotinaSpawn);
    }
    IEnumerator SpawnarInimigo()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloDeSpawn);
            Instantiate(objetoASerSpawnado, transform.position, transform.rotation);
        }
    }
}
