using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    public ParticleSystem winEffect, deathEffect, coinEffect;

    public void playWinEffect(){
        winEffect.Play();
    }

    public void playDeathEffect(){
        deathEffect.Play();
    }

    public void playCoinEffect(){
        coinEffect.Play();
    }
}
