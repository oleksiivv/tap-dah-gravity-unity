using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public GameObject undoProgressPanel;

    public void undoProgress(){
        undoProgressPanel.SetActive(!undoProgressPanel.activeSelf);
    }
    public void undoProgressConfirm(){
        PlayerPrefs.DeleteAll();
        undoProgressPanel.SetActive(false);
    }

    public void undoProgressCancel(){
        undoProgressPanel.SetActive(false);
    }



    public GameObject buttonMutedSound, buttonNormalSound;
    public GameObject buttonMutedMusic, buttonNormalMusic;
    public AudioController audio;
    void Start()
    {
        updateMusic();
        updateSound();
    }

    public void muteSound(){
        PlayerPrefs.SetInt("!sound",1);
        updateSound();

        //GetComponent<AudioSource>().enabled=false;
    }

    public void unmuteSound(){
        PlayerPrefs.SetInt("!sound",0);
        updateSound();

        //GetComponent<AudioSource>().enabled=true;
    }


    public void muteMusic(){
        PlayerPrefs.SetInt("!music",1);
        updateMusic();
    }

    public void unmuteMusic(){
        PlayerPrefs.SetInt("!music",0);
        updateMusic();
    }

    void updateSound(){
        if(PlayerPrefs.GetInt("!sound")==0){

            buttonMutedSound.SetActive(false);
            buttonNormalSound.SetActive(true);

        }
        else{
            buttonMutedSound.SetActive(true);
            buttonNormalSound.SetActive(false);
        }

        audio.updateMusic();
        audio.updateSound();
    }

    void updateMusic(){
        if(PlayerPrefs.GetInt("!music")==0){

            buttonMutedMusic.SetActive(false);
            buttonNormalMusic.SetActive(true);

        }
        else{
            buttonMutedMusic.SetActive(true);
            buttonNormalMusic.SetActive(false);
        }

        audio.updateMusic();
        audio.updateSound();
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
