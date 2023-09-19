using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoTomouDanoPlayer : EstadoBasePlayer
{
    // Start is called before the first frame update
    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
        player.TrocarAnimPlayer(ScriptPlayer.EstadosAnimacao.levandoDano);
        player.GetInformacoesPlayer.InvocarEventosLevarDanoInimigo();
        player.TocarAudio(player.GetAudioDano);
        if(player.GetOlhandoParaDireita)
        {
            player.GetRigidbody2D.velocity=player.GetForcaAplicadaAoEntrarNoEstadoDeTomarDano;
        }
        else
        {
            player.GetRigidbody2D.velocity=new Vector2(-player.GetForcaAplicadaAoEntrarNoEstadoDeTomarDano.x,player.GetForcaAplicadaAoEntrarNoEstadoDeTomarDano.y);
        }
    }

    public override void AtualizarEstadoFixado()
    {
        base.AtualizarEstadoFixado();
        player.GetRigidbody2D.Cast(Vector2.down,player.GetRaycastsPulo,player.GetDistanciaChecagemPulo);
        //Essa checagem é feita para garantir que a personagem não volte para o estaddo Idle antes de sair do chão
        if(player.GetRaycastsPulo.Count!=0 && player.GetRigidbody2D.velocity.y<=0)
        {
            foreach(RaycastHit2D r in player.GetRaycastsPulo)
            {
                if(r.collider.tag!="Player" && r.collider.tag!="ColiderFantasma")
                {
                    if(Input.GetAxisRaw(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal)!=0)
                    {
                        player.TrocaEstadoPlayer(new EstadoAndandoPlayer());
                    }
                    else
                    {
                        player.TrocaEstadoPlayer(new EstadoIdlePlayer());
                    }
                    return;
                }
            }
        }
        player.GetRaycastsPulo.Clear();
    }
}
