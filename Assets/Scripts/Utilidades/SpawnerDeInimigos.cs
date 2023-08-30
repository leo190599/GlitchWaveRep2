using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDeInimigos : MonoBehaviour
{
    [SerializeField]
    private GameObject objetoASerSpawnado;
    [SerializeField]
    private float intervaloDeSpawn;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnarInimigo());
    }
    IEnumerator SpawnarInimigo()
    {
        while (true)
        {
            Instantiate(objetoASerSpawnado,transform.position,transform.rotation);
            yield return new WaitForSeconds(intervaloDeSpawn);
        }
    }
}
