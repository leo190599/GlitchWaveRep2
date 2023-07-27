using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoPuloAtaquePlayer : EstadoNoArBasePlayer
{
    // Start is called before the first frame update
    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
        player.TrocarAnimPlayer(ScriptPlayer.EstadosAnimacao.pulandoAtacando);
    }
    public override void AtualizarEstadoFixado()
    {
        base.AtualizarEstadoFixado();
        player.GetRigidbody2D.AddForce(new Vector2(
        Mathf.Lerp(Input.GetAxis(player.GetMapeadorDeBotoes.GetEixoDeMovimentoHorizontal)*player.GetVelocidadeDeMovimento,0,player.GetPerdaDeMovimentoNoAr),0));
        player.GetRigidbody2D.velocity=new Vector2(Mathf.Clamp(player.GetRigidbody2D.velocity.x,-player.GetVelocidadeDeMovimento,player.GetVelocidadeDeMovimento)
        ,player.GetRigidbody2D.velocity.y);

       // Debug.Log(player.GetRigidbody2D.velocity);

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

    public override void EventoInicioAnimacao()
    {
        base.EventoInicioAnimacao();
    }
    public override void EventoAnimacao()
    {
        base.EventoAnimacao();
        player.AtivarEfeitosEspada();
        player.AtivarColisorEspada();
    }
    public override void FinalizarEstado()
    {
        base.FinalizarEstado();
        player.DesativarEfeitosEspada();
        player.DesativarColisorEspada();
    }
}
