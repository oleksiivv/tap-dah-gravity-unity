using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AudioController : MonoBehaviour
{
    public AudioSource[] sound,music;

#if UNITY_IOS
    private string gameID="4262470";
#else
    private string gameID="4262471";
#endif

    void Start(){

        Advertisement.Initialize(gameID,false);
        //Advertisement.Banner.SetPosition (BannerPosition.BOTTOM_RIGHT);

        updateSound();
        updateMusic();

        //StartCoroutine (ShowBannerWhenReady ());

        
    }


    public void updateSound(){
        foreach(var s in sound){
            if(PlayerPrefs.GetInt("!sound")==0){

                s.enabled=true;

            }
            else{
                s.enabled=false;
            }
        }
    }

    public void updateMusic(){
        foreach(var m in music){
            if(PlayerPrefs.GetInt("!music")==0){

                m.enabled=true;

            }
            else{
                m.enabled=false;
            }
        }
    }

    void ShowBannerWhenReady () {
       // while (!Advertisement.IsReady ("Banner_Android")) {
       //     yield return new WaitForSeconds (0.5f);
       // }
        Advertisement.Banner.SetPosition (BannerPosition.BOTTOM_RIGHT);
        Advertisement.Banner.Show ("Banner_Android");
    }


    
}
