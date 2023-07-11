using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoAtaquePlayer : EstadoAtivoBasePlayer
{
    private bool atacou=false;
    //private bool executouEventoFinalAnimacao=false;
    // Start is called before the first frame update
    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
        player.GetRigidbody2D.velocity=new Vector2(0,player.GetRigidbody2D.velocity.y);  
        //player.StartCoroutine(player.TrocaEstadoPlayerCorrotina(new EstadoIdlePlayer(),player.GetAnimator.GetCurrentAnimatorStateInfo(1).length));
        player.TrocarAnimPlayer(ScriptPlayer.EstadosAnimacao.atacando);
    }

    public override void EventoInicioAnimacao()
    {
        base.EventoInicioAnimacao();
        player.AtivarEfeitosEspada();
    }

    public override void EventoAnimacao()
    {
        base.EventoAnimacao();
        if(!atacou)
        {
            player.Atacar();
            atacou=true;
            player.DesativarEfeitosEspada();
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
