using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NovoMapeadorDeBotoes",menuName ="ScriptableObjectsCustomizados/MapeadorDeBotoes")]
public class MapeadorDeBotoes : ScriptableObject
{
    [SerializeField]
    private string eixoDeMovimentoHorizontal="Horizontal";
    [SerializeField]
    private KeyCode botaoPulo=KeyCode.Space;
    [SerializeField]
    private KeyCode botaoPause=KeyCode.Escape;
    [SerializeField]
    private KeyCode botaoAtaque=KeyCode.L;
    [SerializeField]
    private KeyCode botaoSubItem=KeyCode.K;
    [SerializeField]
    private KeyCode botaoAbaixar=KeyCode.S;
    [SerializeField]
    private KeyCode botaoGlitch=KeyCode.O;

    public string GetEixoDeMovimentoHorizontal=>eixoDeMovimentoHorizontal;
    public KeyCode GetBotaoPulo=>botaoPulo;
    public KeyCode GetBotaoPause=>botaoPause;
    public KeyCode GetBotaoAtaque=>botaoAtaque;
    public KeyCode GetBotaoSubItem=>botaoSubItem;
    public KeyCode GetBotaoAbaixar=>botaoAbaixar;
    public KeyCode GetBotaoGlitch=>botaoGlitch;
}
