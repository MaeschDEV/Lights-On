using Newtonsoft.Json;
using System.Collections.Generic;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class leaderboardManager : MonoBehaviour
{
    List<LeaderboardEntry> entries = new List<LeaderboardEntry>();

    [SerializeField] private GameObject module;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetScores("Easy");
        }
    }

    public async void AddScore(string id, int multiplier)
    {
        var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(id, multiplier);
        Debug.Log(JsonConvert.SerializeObject(playerEntry));
    }

    public async void GetScores(string id)
    {
        LeaderboardScoresPage scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(id);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));

        entries = scoresResponse.Results;

        foreach (var entry in entries)
        {
            Debug.Log(entry.Rank);
        }
    }
}
