using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoAndandoPlayer : EstadoAtivoBasePlayer
{
    private RaycastHit2D rh;
    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
         player.GetRigidbody2D.velocity=new Vector2(Input.GetAxis(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal)*player.GetVelocidadeDeMovimento,player.GetRigidbody2D.velocity.y);
        player.GetRigidbody2D.sharedMaterial=player.GetMaterialFisicoAndando;
        player.SetDadoDashNoAr(false);
        player.TrocarAnimPlayer(ScriptPlayer.EstadosAnimacao.correndo);
    }
    public override void AtualizarEstado()
    {
        base.AtualizarEstado();
        if(Input.GetAxisRaw(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal)!=0)
        {
            player.RodarPersonagem(Mathf.Sign(Input.GetAxisRaw(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal))>0);

            rh=Physics2D.Raycast(new Vector2(player.GetCapsuleCollider2D.bounds.center.x,player.GetCapsuleCollider2D.bounds.min.y),Vector2.down,3,player.GetLayerChao);
            
            //Debug.Log(Matematica.RotacaoDeVetor(rh.normal,-90*Input.GetAxisRaw("Horizontal")));
            if(rh.collider!=null)
            {
                player.GetRigidbody2D.velocity=Matematica.RotacaoDeVetor(rh.normal,-90*Input.GetAxisRaw(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal))
                *player.GetVelocidadeDeMovimento*Mathf.Abs(Input.GetAxis(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal));
            }
            else
            {
                player.GetRigidbody2D.velocity=new Vector2(Input.GetAxis(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal)*player.GetVelocidadeDeMovimento,player.GetRigidbody2D.velocity.y);
            }
            //Debug.Log(player.GetRigidbody2D.velocity);
        }
        else
        {
            if(Physics2D.Raycast(new Vector2(player.GetCapsuleCollider2D.bounds.center.x,player.GetCapsuleCollider2D.bounds.min.y),Vector2.down,3,player.GetLayerChao))
            {
                player.GetRigidbody2D.velocity=Vector2.zero;
            }
            player.TrocaEstadoPlayer(new EstadoIdlePlayer());
            return;
        }
        if(Input.GetKeyDown(player.GetMapeadorDeBotoes.GetBotaoPulo))
        {
           player.GetRigidbody2D.velocity=new Vector2(player.GetRigidbody2D.velocity.x,player.GetForcaPulo);
            player.TrocaEstadoPlayer(new EstadoPuloPlayer());
            return;
        }
        if(Input.GetKeyDown(player.GetMapeadorDeBotoes.GetBotaoAtaque))
        {
            player.TrocaEstadoPlayer(new EstadoAtaquePlayer());
            return;
        }
        if(Input.GetKeyDown(player.GetMapeadorDeBotoes.GetBotaoSubItem))
        {
            player.TrocaEstadoPlayer(new EstadoUsandoSubItemPlayer());
            return;
        }
        if(Input.GetKeyDown(player.GetMapeadorDeBotoes.GetBotaoAbaixar))
        {
            player.TrocaEstadoPlayer(new EstadoAbaixadaPlayer());
        }
    }
}
