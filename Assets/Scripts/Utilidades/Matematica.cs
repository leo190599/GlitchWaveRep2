using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Matematica
{
    public static Vector2 RotacaoDeVetor(Vector2 vetor, float angulo)
    {
        angulo=angulo*Mathf.Deg2Rad;
        return(new Vector2(
        (vetor.x*Mathf.Cos(angulo)-vetor.y*Mathf.Sin(angulo)),
        (vetor.y*Mathf.Cos(angulo)+vetor.x*Mathf.Sin(angulo))));
    }
}
