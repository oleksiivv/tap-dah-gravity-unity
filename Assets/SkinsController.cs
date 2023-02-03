using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinsController : MonoBehaviour
{
    public GameObject[] skins;
    private int curr=0;

    public GameObject chooseBtn;
    public GameObject[] buyEquipment;
    public Text[] prices;

    public ParticleSystem buyEffect;

    public Text currentItemName;

    public Text coinsCurrent;

    private List<ShopItem> items=new List<ShopItem>();

    void Start(){
        //hideAll();
        //skins[0].SetActive(true);

        //PlayerPrefs.SetInt("coins",1200);

         if(PlayerPrefs.GetInt("first",-1)==-1){
             PlayerPrefs.SetInt("coins",90);
             PlayerPrefs.SetInt("first",1);
         }

        coinsCurrent.text=PlayerPrefs.GetInt("coins").ToString();

        items.Add(new ShopItem(0,000,"FOX"));
        items.Add(new ShopItem(1,100,"CAT"));
        items.Add(new ShopItem(2,200,"TIGER"));
        items.Add(new ShopItem(3,250,"DRAGON"));
        items.Add(new ShopItem(4,350,"SPIDER"));

        curr=PlayerPrefs.GetInt("current",0);

        //showAvailable();
        show(curr);

        for(int i=0;i<items.Count;i++){
            prices[i].text=items[i].price.ToString();
        }
    }

    void show(int id){
        hideAll();

        skins[id].SetActive(true);
        currentItemName.text=items[id].name;

        if(PlayerPrefs.GetInt("Car@"+id.ToString())==1 || id==0){
            buyEquipment[id].SetActive(false);
            chooseBtn.SetActive(true);
        }
        else{
            buyEquipment[id].SetActive(true);
            chooseBtn.SetActive(false);
        }
        
        //showAvailable();
    }


    public void right(){
        curr++;
        if(curr>=skins.Length){
            curr=0;
        }

        show(curr);
        
    }

    public void left(){
        curr--;
        if(curr<0){
            curr=skins.Length-1;
        }

        show(curr);
    }
    void hideAll(){
        foreach(var skin in skins)skin.SetActive(false);

        foreach(var eq in buyEquipment){
            eq.SetActive(false);
        }
    }

    public void showAvailable(){
        for(int i=0;i<items.Count;i++){
            if(PlayerPrefs.GetInt("Car@"+i.ToString())==1){
                buyEquipment[i].SetActive(false);
            }
            else{
                buyEquipment[i].SetActive(true);
            }
        }
    }

    public void buy(int id){
        if(PlayerPrefs.GetInt("coins")>=items[id].price){
            PlayerPrefs.SetInt("coins",PlayerPrefs.GetInt("coins")-items[id].price);

            items[id].buy();
            show(id);

            buyEffect.Play();
            coinsCurrent.text=PlayerPrefs.GetInt("coins").ToString();
        }
    }


    public ParticleSystem chooseEffect;
    //public ScenesManager scenes;
    public void choose(){
        
        PlayerPrefs.SetInt("current",curr);
        chooseEffect.Play();
        Invoke(nameof(menuOpen), 0.5f);
    }

    void menuOpen(){
        openScene(0);
    }

    public void openScene(int id){
        StartCoroutine(loadAsync(id));
    }

    public GameObject loadingPanel;
    public Slider loadingSlider;

    IEnumerator loadAsync(int id)
    {
        AsyncOperation operation = Application.LoadLevelAsync(id);
        loadingPanel.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingSlider.value = progress;
            yield return null;

        }
    }
}
