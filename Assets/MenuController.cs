using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{
    public Text level;
    public Text coins;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("level",1)<12){
            level.text=PlayerPrefs.GetInt("level",1).ToString();
        }
        else{
            level.text="11";
        }

        coins.text=PlayerPrefs.GetInt("coins").ToString();
    }

    public void startGame(){
        if(PlayerPrefs.GetInt("level",1)+2<Application.levelCount){
            openScene(PlayerPrefs.GetInt("level",1)+2);
        }
        else{
            openScene(3);
        }
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
