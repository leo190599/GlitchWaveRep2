using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoMortePlayer : EstadoBasePlayer
{
    public override void IniciarEstadoPlayer(ScriptPlayer player)
    {
        base.IniciarEstadoPlayer(player);
        player.GetAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        player.TocarAudio(player.GetAudioMorte);
        player.TrocarAnimPlayer(ScriptPlayer.EstadosAnimacao.morte);
    }
}
