using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoUsandoSubItemPlayer : EstadoAtivoBasePlayer
{
    private GameObject subItem;
    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
        player.GetRigidbody2D.velocity=(new Vector2(0,player.GetRigidbody2D.velocity.y));
        player.TrocarAnimPlayer(ScriptPlayer.EstadosAnimacao.usandoSubItem); 
    }

    public override void EventoAnimacao()
    {
        base.EventoAnimacao();
        subItem=player.GetInformacoesPlayer.GetPrefabSubItem();
        if(subItem!=null)
        {
            if((player.GetInformacoesPlayer.GetVidaAtual-player.GetInformacoesPlayer.GetSubItemObjetoScriptavel.GetCustoDeVida)>0)
            {
                player.GetInformacoesPlayer.ReceberDano(player.GetInformacoesPlayer.GetSubItemObjetoScriptavel.GetCustoDeVida);

                player.instanciarObjeto(subItem,player.GetTransformPosicaoInstanciaSubItem.position,
                 player.GetOlhandoParaDireita?Quaternion.Euler(0,90,0):Quaternion.Euler(0,270,0)
                 );
            }
            else
            {
                Debug.Log("Nao ha vida suficiente");
            }
        }
        else
        {
            Debug.LogWarning("Nao ha sub item para instanciar");
        } 
    }

    public override void EventoFinalAnimacao()
    {
        base.EventoFinalAnimacao();
        if(Input.GetAxisRaw(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal)!=0)
        {
            player.TrocaEstadoPlayer(new EstadoAndandoPlayer());
        }
        else
        {
            player.TrocaEstadoPlayer(new EstadoIdlePlayer());
        }
    }
}
