using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoPuloPlayer : EstadoNoArBasePlayer
{
    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
        player.TrocarAnimPlayer(ScriptPlayer.EstadosAnimacao.pulando);
    }
    public override void AtualizarEstado()
    {
        base.AtualizarEstado();
        if(Input.GetKeyDown(player.GetMapeadorDeBotoes.GetBotaoAtaque))
        {
            player.TrocaEstadoPlayer(new EstadoPuloAtaquePlayer());
        }
        if(Input.GetKeyDown(player.GetMapeadorDeBotoes.GetBotaoSubItem))
        {
            player.TrocaEstadoPlayer(new EstadoPuloUsandoSubItemPlayer());
        }
    }
    public override void AtualizarEstadoFixado()
    {
        base.AtualizarEstadoFixado();
        //if(Mathf.Abs(player.GetRigidbody2D.velocity.x+Mathf.Lerp(Input.GetAxis(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal)*player.GetVelocidadeDeMovimento,0,player.GetPerdaDeMovimentoNoAr))<player.GetVelocidadeDeMovimento)
        //{
            player.GetRigidbody2D.AddForce(new Vector2(
            Mathf.Lerp(Input.GetAxis(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal)*player.GetVelocidadeDeMovimento,0,player.GetPerdaDeMovimentoNoAr),0));
        //}
        player.GetRigidbody2D.velocity=new Vector2(Mathf.Clamp(player.GetRigidbody2D.velocity.x,-player.GetVelocidadeDeMovimento,player.GetVelocidadeDeMovimento)
        ,player.GetRigidbody2D.velocity.y);
        
        //Debug.Log(player.GetRigidbody2D.velocity);

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
