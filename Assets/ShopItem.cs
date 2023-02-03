using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public ShopItem(int id,int price,string name=""){
        this.id=id;
        this.price=price;
        this.name=name;

        //Debug.Log(name);
    }
    public int id{get;private set;}

    public int price;

    public string name{get;set;}

    public void buy(){
        PlayerPrefs.SetInt("Car@"+id.ToString(),1);
        PlayerPrefs.SetInt("current",id);
    }
}
