using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoDashPlayer : EstadoBasePlayer
{
     private RaycastHit2D rh;
     private float direcaoDash;
     private Vector2 movimentProximoFrame;
    // Start is called before the first frame update
    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
        player.GetTrailDash.gameObject.SetActive(true);
        //player.GetRigidbody2D.isKinematic=true;
        player.GetRigidbody2D.sharedMaterial=player.GetMaterialFisicoAndando;
        if(player.GetDashDireita)
        {
            direcaoDash=1;
        }
        else
        {
            direcaoDash=-1;
        }
    }
    public override void AtualizarEstadoFixado()
    {
        base.AtualizarEstadoFixado();
         rh=Physics2D.Raycast(new Vector2(player.GetCapsuleCollider2D.bounds.center.x,player.GetCapsuleCollider2D.bounds.min.y),Vector2.down,4,player.GetLayerChao);
            //Debug.Log(Matematica.RotacaoDeVetor(rh.normal,-90*Input.GetAxisRaw("Horizontal")));
            if(rh.collider!=null)
            {
                movimentProximoFrame=Matematica.RotacaoDeVetor(rh.normal,-90*direcaoDash)*player.GetVelDash;
                player.GetRigidbody2D.velocity=(new Vector2(movimentProximoFrame.x,
                movimentProximoFrame.y));
            }
            else
            {
                player.GetRigidbody2D.velocity=(new Vector2(player.GetVelDash*direcaoDash,0));
            }
    }
    public override void FinalizarEstado()
    {
        base.FinalizarEstado();
        //player.GetRigidbody2D.isKinematic=false;
        player.GetTrailDash.Clear();
        player.GetTrailDash.gameObject.SetActive(false);
        player.GetRigidbody2D.velocity=Vector2.zero;
        player.GetRigidbody2D.sharedMaterial=player.GetMaterialFisicoParado;
    }
    public override void EventoCorrotinaEstado()
    {
        base.EventoCorrotinaEstado(); 
        rh=Physics2D.Raycast(new Vector2(player.GetCapsuleCollider2D.bounds.center.x,player.GetCapsuleCollider2D.bounds.min.y),Vector2.down,3,player.GetLayerChao);
        if(rh.collider!=null)
        {
            if(Input.GetAxisRaw(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal)!=0)
            {
                player.TrocaEstadoPlayer(new EstadoAndandoPlayer());
            }
            else
            {
                player.TrocaEstadoPlayer(new EstadoIdlePlayer());
            }
        }
        else
        {
            player.TrocaEstadoPlayer(new EstadoPuloPlayer());
        }
    } 
}
