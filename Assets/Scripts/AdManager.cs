using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string _androidGameId;
    [SerializeField] private string _iOsGameId;
    [SerializeField] private string adUnitIdAndroid;
    [SerializeField] private string adUnitIdIos;
    [SerializeField] bool _testMode = true;
    private string _gameId;
    private string adUnitId;
    private static AdManager instance;
    private bool isAdLoaded = false;

    public static event Action OnAdWatched; // Evento para notificar quando um anúncio é assistido

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAds();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializeAds()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _gameId = _iOsGameId;
            adUnitId = adUnitIdIos;
        }
        else
        {
            _gameId = _androidGameId;
            adUnitId = adUnitIdAndroid;
        }
        Advertisement.Initialize(_gameId, _testMode, this);
        Debug.Log("Unity Ads Initialized");
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad");
        Advertisement.Load(adUnitId, this);
    }

    public void ShowAd()
    {
        if (isAdLoaded)
        {
            Advertisement.Show(adUnitId, this);
        }
        else
        {
            Debug.Log("Ad is not loaded yet");
            LoadAd();
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded");
        isAdLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Ad failed to load: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Ad failed to show: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(this.adUnitId))
        {
            isAdLoaded = false;
            LoadAd();

            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                OnAdWatched?.Invoke(); // Notificar que o anúncio foi assistido
            }
        }
    }
}
