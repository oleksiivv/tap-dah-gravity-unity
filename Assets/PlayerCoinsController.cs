using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoinsController : MonoBehaviour
{
    public Text coinsText;

    public void updateCoins(int n=1){
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins")+n);
        showLabels();
    }

    void showLabels(){
        coinsText.text=PlayerPrefs.GetInt("coins").ToString();
    }
}
