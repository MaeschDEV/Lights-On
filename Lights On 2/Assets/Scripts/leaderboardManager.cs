using Newtonsoft.Json;
using Unity.Services.Leaderboards;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class leaderboardManager : MonoBehaviour
{
    public async void AddScore(string id, int multiplier)
    {
        var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(id, multiplier);
        Debug.Log(JsonConvert.SerializeObject(playerEntry));
    }

    public async void GetScores(string id)
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(id);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }
}
