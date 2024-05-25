using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public string myGameIdAndroid = "5625355";
    public string adUnitIdAndroid = "Rewarded_Android";
    public string myAdUnitId;
    public string myAdStatus = "";
    public bool adStarted;
    public bool adCompleted;
    public Button showAdButton;
    private bool testMode = false;

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_IOS
	        Advertisement.Initialize(myGameIdIOS, testMode, this);
	        myAdUnitId = adUnitIdIOS;
        #else
                Advertisement.Initialize(myGameIdAndroid, testMode, this);
                myAdUnitId = adUnitIdAndroid;
        #endif

        showAdButton.onClick.AddListener(ShowAd);

    }

    public void ShowAd()
    {
        Advertisement.Load(myAdUnitId,this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        Advertisement.Load(myAdUnitId,this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        myAdStatus = message;
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
        if (!adStarted)
        {
            Advertisement.Show(myAdUnitId,this);
        }
    }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        myAdStatus = message;
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");

    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        myAdStatus = message;
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        adStarted = true;
        Debug.Log("Ad Started: " + adUnitId);
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {
        Debug.Log("Ad Clicked: " + adUnitId);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        adCompleted = showCompletionState == UnityAdsShowCompletionState.COMPLETED;
        Debug.Log("Ad Completed: " + adUnitId);
    }

}