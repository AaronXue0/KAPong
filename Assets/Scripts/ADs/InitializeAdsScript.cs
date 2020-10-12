using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using DG.Tweening;

namespace Menuspace
{
    public class InitializeAdsScript : MonoBehaviour, IUnityAdsListener
    {
#if UNITY_IOS
            private string gameId = "3796674";
#elif UNITY_ANDROID
        private string gameId = "3796675";
#endif
        string myPlacementId = "rewardedVideo";
        bool testMode = false;

        public GameObject loader;
        public Text loadMessage;

        void Start()
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId, testMode);
        }

        public void ShowRewardedVideo()
        {
            // Check if UnityAds ready before calling Show method:
            if (Advertisement.IsReady(myPlacementId))
            {
                loadMessage.text = "Loading";
                loader.SetActive(true);
                Advertisement.Show(myPlacementId);
            }
            else
            {
                Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
            }
        }

        // Implement IUnityAdsListener interface methods:
        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            // Define conditional logic for each ad completion status:
            if (showResult == ShowResult.Finished)
            {
                GetComponent<Menu>().PlayThanksFeedback();
                Debug.Log("Success");
                Invoke("HideLoader", 0f);
            }
            else if (showResult == ShowResult.Skipped)
            {
                // Do not reward the user for skipping the ad.
                loadMessage.text = "";
                loadMessage.DOText("ADs Skiped", 1f);
                Invoke("HideLoader", 3f);
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.LogWarning("The ad did not finish due to an error.");
            }
        }

        public void OnUnityAdsReady(string placementId)
        {
            // If the ready Placement is rewarded, show the ad:
            if (placementId == myPlacementId)
            {
            }
        }

        public void OnUnityAdsDidError(string message)
        {
            // Log the error.
            loadMessage.text = "";
            loadMessage.DOText("ADs Loaded error", 1f);
            Invoke("HideLoader", 3f);
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            // Optional actions to take when the end-users triggers an ad.
        }

        void HideLoader()
        {
            loader.SetActive(false);
        }

        // When the object that subscribes to ad events is destroyed, remove the listener:
        public void OnDestroy()
        {
            Advertisement.RemoveListener(this);
        }
    }
}