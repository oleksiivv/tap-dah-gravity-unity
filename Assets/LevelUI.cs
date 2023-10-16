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
        MobileAds.Initialize(initStatus => {
            LoadLoadInterstitialAd();

            CreateBannerView();
            LoadBannerAd();
        });
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

      AdRequest AdRequestBuild(){
          return new AdRequest.Builder().Build();
      }

       public bool showIntersitionalAd(){
           return showIntersitionalGoogleAd();
       }

    //baner

    private InterstitialAd _interstitialAd;

    private BannerView _bannerView;
    
    public void LoadLoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
                _interstitialAd.Destroy();
                _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(intersitionalId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                    "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                            + ad.GetResponseInfo());

                _interstitialAd = ad;
            });
    }


      public bool showIntersitionalGoogleAd(){
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            _interstitialAd.Show();

            return true;
        }
        else
        {
            return false;
        }
      }

    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyBannerView();
        }

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
    }

    public void LoadBannerAd()
    {
        // create an instance of a banner view first.
        if(_bannerView == null)
        {
            CreateBannerView();
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }

    public void DestroyBannerView()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

    AdRequest AdRequestBannerBuild(){
        return new AdRequest.Builder().Build();
    }
}
