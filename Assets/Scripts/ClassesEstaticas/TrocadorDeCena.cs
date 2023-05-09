using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class TrocadorDeCena
{
    // Start is called before the first frame update
    public static void TrocarCena(string nomeDaNovaCena)
    {
        SceneManager.LoadScene(nomeDaNovaCena);
    }
}
