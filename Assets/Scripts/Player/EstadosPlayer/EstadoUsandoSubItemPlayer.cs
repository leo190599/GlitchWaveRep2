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
        subItem=player.GetInformacoesPlayer.GetPrefabSubItem();
        if(subItem!=null)
        {

        }
        else
        {
            Debug.LogWarning("Nao ha sub item para instanciar");
        }
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
