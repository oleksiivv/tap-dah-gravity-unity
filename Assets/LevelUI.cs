using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;
using System; 

public class LevelUI : MonoBehaviour
{
    public GameObject pausePanel, deathPanel, winPanel;


    public static int addCnt=1;

#if UNITY_IOS
    private string gameID="4262470";

    private string appId="ca-app-pub-4962234576866611~3666218588";

     private string intersitionalId="ca-app-pub-4962234576866611/4008181126";

     private string bannerId="ca-app-pub-4962234576866611/4701751044";
#else
    private string gameID="4262471";

    private string appId="ca-app-pub-4962234576866611~7086186592";

     private string intersitionalId="ca-app-pub-4962234576866611/3146941585";

     private string bannerId="ca-app-pub-4962234576866611/9695219820";
#endif

    private bool alreadyShowed=false;

    void Start(){
        Advertisement.Initialize(gameID,false);
        
         RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetSameAppKeyEnabled(true).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        RequestConfigurationAd();
        RequestBannerAd();
    }


    public void pause(){
        Time.timeScale=0;
        pausePanel.SetActive(true);

        if(addCnt%3==0 && !alreadyShowed){
            //if(Advertisement.IsReady()){
                Advertisement.Show("Interstitial_Android");
                alreadyShowed=true;
            //}
        }
        addCnt++;
    }
    public void resume(){
        Time.timeScale=1;
        pausePanel.SetActive(false);
    }
    public void openScene(int id){
        Time.timeScale=1;
        StartCoroutine(loadAsync(id));
    }
    public void restart(){
        openScene(Application.loadedLevel);
    }
    public void next(){
        openScene(Application.loadedLevel+1);
    }

    public void setDeathPanelVisibility(bool visible){
        deathPanel.SetActive(visible);
        if(visible){
            if(addCnt%3==0 && !alreadyShowed){
                if(! showIntersitionalAd()){
                    Advertisement.Show("Interstitial_Android");
                    alreadyShowed=true;
                }
            }
            addCnt++;
        }
    }
    public void setWinPanelVisibility(bool visible){
        winPanel.SetActive(visible);

        if(visible){
            if(addCnt%3==0 && !alreadyShowed){
                if(! showIntersitionalAd()){
                    Advertisement.Show("Interstitial_Android");
                    alreadyShowed=true;
                }
            }
            addCnt++;
        }
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


    public void rate(){
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.EasyStreet.TapTapRun2");
    }

    private InterstitialAd intersitional;

     private BannerView banner;

      AdRequest AdRequestBuild(){
          return new AdRequest.Builder().Build();
      }


       void RequestConfigurationAd(){
           intersitional=new InterstitialAd(intersitionalId);
           AdRequest request=AdRequestBuild();
           intersitional.LoadAd(request);
           intersitional.OnAdLoaded+=this.HandleOnAdLoaded;
           intersitional.OnAdOpening+=this.HandleOnAdOpening;
           intersitional.OnAdClosed+=this.HandleOnAdClosed;

     }


       public bool showIntersitionalAd(){
           if(intersitional.IsLoaded()){
               intersitional.Show();
           }

           return intersitional.IsLoaded();
       }

       private void OnDestroy(){
           DestroyIntersitional();

           intersitional.OnAdLoaded-=this.HandleOnAdLoaded;
           intersitional.OnAdOpening-=this.HandleOnAdOpening;
           intersitional.OnAdClosed-=this.HandleOnAdClosed;

       }

       private void HandleOnAdClosed(object sender, EventArgs e)
       {
           intersitional.OnAdLoaded-=this.HandleOnAdLoaded;
           intersitional.OnAdOpening-=this.HandleOnAdOpening;
           intersitional.OnAdClosed-=this.HandleOnAdClosed;

         RequestConfigurationAd();
       
       }

      private void HandleOnAdOpening(object sender, EventArgs e)
      {
        
      }

      private void HandleOnAdLoaded(object sender, EventArgs e)
      {
        
      }

      public void DestroyIntersitional(){
          intersitional.Destroy();
      }




    //baner


     AdRequest AdRequestBannerBuild(){
         return new AdRequest.Builder().Build();
     }


     public void RequestBannerAd(){
         banner=new BannerView(bannerId,AdSize.Banner,AdPosition.Bottom);
         AdRequest request = AdRequestBannerBuild();
         banner.LoadAd(request);
     }

     public void DestroyBanner(){
         if(banner!=null){
             banner.Destroy();
         }
     }

}
