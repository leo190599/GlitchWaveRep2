using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoUsandoSubItemPlayer : EstadoAtivoBasePlayer
{
    private GameObject subItem;
    private bool usouSubItem = false;
    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
        player.GetRigidbody2D.velocity=(new Vector2(0,0));
        player.GetRigidbody2D.sharedMaterial=player.GetMaterialFisicoParado;
        player.GetRigidbody2D.constraints=RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        player.TrocarAnimPlayer(ScriptPlayer.EstadosAnimacao.usandoSubItem);
        player.IniciarCorrotinaEstadoPlayer(.45f);
    }
    public override void EventoInicioAnimacao()
    {
        base.EventoInicioAnimacao();
        //player.IniciarCorrotinaEstadoPlayer(player.GetAnimator.GetCurrentAnimatorClipInfo(0).Length);
        //}

        //public override void EventoAnimacao()
        //{
        //  base.EventoAnimacao();
        if (!usouSubItem)
        {
            //Debug.Log(player.GetAnimator.GetCurrentAnimatorClipInfo(0).Length);
            subItem = player.GetInformacoesPlayer.GetPrefabSubItem();
            if (subItem != null)
            {
                if ((player.GetInformacoesPlayer.GetVidaAtual - player.GetInformacoesPlayer.GetSubItemObjetoScriptavel.GetCustoDeVida) > 0)
                {
                    player.GetInformacoesPlayer.ReceberDano(player.GetInformacoesPlayer.GetSubItemObjetoScriptavel.GetCustoDeVida);

                    player.instanciarObjeto(subItem, player.GetTransformPosicaoInstanciaSubItem.position,
                    Quaternion.Euler(0, 0, 0), new Vector3(player.GetOlhandoParaDireita ? 1 : -1, 1, 1));
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
            usouSubItem = true;
        }
    }
    public override void EventoCorrotinaEstado()
    {
        base.EventoCorrotinaEstado();
        EventoFinalAnimacao();
        Debug.Log("a");
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
    }
}
