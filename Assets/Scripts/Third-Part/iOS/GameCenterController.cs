using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

using GooglePlayGames;

public class GameCenterController : MonoBehaviour
{
#if UNITY_IOS
    private string leaderboardID = "KAPongTimeLeaderboard";
    // #elif UNITY_ANDROID
    //     private string leaderboardID = "CgkIo5fm6cIbEAIQAA";
#endif


    // Start is called before the first frame update
    void Start()
    {
        Social.localUser.Authenticate(ProcessAuthentication);
    }

    public void ReportScoreToLeaderboard(int score)
    {
#if UNITY_IOS
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, leaderboardID, ProcessScoreReport);
        }
#endif
    }
    public void ShowLeaderboard()
    {
#if UNITY_IOS
        if (Social.localUser.authenticated)
        {
            Social.localUser.Authenticate(result =>
            {
                if (result) Social.ShowLeaderboardUI();
            });
        }
#endif

    }

    void ProcessAuthentication(bool success)
    {
        //Callback for authentication.
    }
    void ProcessScoreReport(bool success)
    {
        //Callback for score report.
    }
}
