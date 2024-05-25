using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    string _adUnitId = null;

    void Awake()
    {
#if UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        _showAdButton.interactable = false;
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad...");
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded");
        if (adUnitId.Equals(_adUnitId))
        {
            _showAdButton.onClick.AddListener(ShowAd);
            _showAdButton.interactable = true;
        }
    }

    public void ShowAd()
    {
        Debug.Log("Showing Ad...");
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
 
    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }

}
