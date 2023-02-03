using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsPanelController : MonoBehaviour
{
    public GameObject levelsPanel;
    public Image[] levels;
    public GameObject[] lockpads;

    public Color32 normal;
    public Color32 unavailable;

    //public ScenesManager scenes;

    public void showLevelsPanel(){
        levelsPanel.SetActive(true);

        showAvailable();
        Invoke(nameof(showElements),1.5f);
    }
    public void hideLevelsPanel(){
        for(int i=0;i<levelsPanel.transform.childCount;i++){
            levelsPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
        levelsPanel.SetActive(false);
    }

    public void showElements(){
        for(int i=0;i<levelsPanel.transform.childCount;i++){
            levelsPanel.transform.GetChild(i).gameObject.SetActive(true);
            Debug.Log(i);
        }
    }

    



    public void showAvailable(){
        for(int i=0;i<PlayerPrefs.GetInt("level",1);i++){
            if(i<11){
                lockpads[i].SetActive(false);
                levels[i].GetComponent<Image>().color=normal;
            }
        }

        if(PlayerPrefs.GetInt("level",1)<12){
            for(int i=PlayerPrefs.GetInt("level");i<lockpads.Length;i++){
                lockpads[i].SetActive(true);
                levels[i].GetComponent<Image>().color=unavailable;
            }
        }
    }


    public void openLevel(int id){
        if(!lockpads[id-1].activeSelf || id==1){
            openScene(id+2);
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
