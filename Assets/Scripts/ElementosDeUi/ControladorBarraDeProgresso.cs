using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorBarraDeProgresso : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Slider slider;

    public void AlterarProgresso(float novoValor)
    {
        slider.value=Mathf.Clamp(novoValor,slider.minValue,slider.maxValue);
    }
    public float GetProgresso=>slider.value;
}
