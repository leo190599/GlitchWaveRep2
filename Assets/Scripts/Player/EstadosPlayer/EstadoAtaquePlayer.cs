using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoAtaquePlayer : EstadoAtivoBasePlayer
{
    private float tempoDecorridoNoEstado=0;
    private float tempoMaximoNoEstado;
    //private bool executouEventoFinalAnimacao=false;
    // Start is called before the first frame update
    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
        player.GetRigidbody2D.velocity=new Vector2(0,0);  
        //player.StartCoroutine(player.TrocaEstadoPlayerCorrotina(new EstadoIdlePlayer(),player.GetAnimator.GetCurrentAnimatorStateInfo(1).length));
        player.GetRigidbody2D.constraints=RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        player.TrocarAnimPlayer(ScriptPlayer.EstadosAnimacao.atacando);
        player.GetRigidbody2D.sharedMaterial=player.GetMaterialFisicoParado;
    }

    public override void AtualizarEstado()
    {
        base.AtualizarEstado();
        tempoDecorridoNoEstado+=Time.deltaTime;
        if(tempoDecorridoNoEstado>tempoMaximoNoEstado+.05)
        {
            EventoFinalAnimacao();
        }
    }
    public override void EventoInicioAnimacao()
    {
        base.EventoInicioAnimacao();
        
        tempoMaximoNoEstado=player.GetAnimator.GetCurrentAnimatorStateInfo(1).length;
    }

    public override void EventoAnimacao()
    {
        base.EventoAnimacao();
        player.TocarAudio(player.GetAudioAtaqueBase);
        //Debug.Log("a");
        player.AtivarEfeitosEspada();
        player.AtivarColisorEspada();
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

    public override void FinalizarEstado()
    {
        base.FinalizarEstado();
        player.GetRigidbody2D.constraints=RigidbodyConstraints2D.FreezeRotation;
        player.DesativarEfeitosEspada();
        player.DesativarColisorEspada();
    }
}
