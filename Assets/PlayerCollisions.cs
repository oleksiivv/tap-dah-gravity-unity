using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public LevelUI ui;
    public PlayerMovement player;
    public PlayerFX fx;
    public PlayerCoinsController coins;

    public GameObject[] skins;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Respawn"){
            ui.setDeathPanelVisibility(true);
            player.speed=0;
            fx.playDeathEffect();
            hideSkins();
            //Destroy(gameObject);
        }
        else if(other.gameObject.tag=="Finish"){
            ui.setWinPanelVisibility(true);
            player.animator.idle();
            player.speed=0;
            PlayerPrefs.SetInt("level",Application.loadedLevel-1);

            fx.playWinEffect();
        }
        else if(other.gameObject.tag=="gem"){
            Destroy(other.gameObject);
            fx.playCoinEffect();

            coins.updateCoins(1);
        }else{
        	if(player.rb.useGravity && other.transform.position.y < gameObject.transform.position.y){
        		ui.setDeathPanelVisibility(true);
            	player.speed=0;
            	fx.playDeathEffect();
            	hideSkins();
            }
        }
        
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag=="Finish"){
            ui.setWinPanelVisibility(true);
            player.animator.idle();
            player.speed=0;
            PlayerPrefs.SetInt("level",Application.loadedLevel-1);

            fx.playWinEffect();
        }
    }


    void hideSkins(){
        foreach (var item in skins)
        {
            item.SetActive(false);
        }
    }
}
