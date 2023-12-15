using Newtonsoft.Json;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class leaderboardManager : MonoBehaviour
{
    List<LeaderboardEntry> entries = new List<LeaderboardEntry>();

    [SerializeField] private GameObject module;
    [SerializeField] private GameObject moduleParent;

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

        moduleParent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, entries.Count * 200);
        foreach (var entry in entries)
        {
            GameObject newModule = Instantiate(module, moduleParent.transform);
            newModule.transform.Find("Rank").GetComponent<TextMeshProUGUI>().text = "#" + (entry.Rank + 1);
            newModule.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = entry.PlayerName.Substring(0, entry.PlayerName.Length - 5);
            newModule.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = entry.Score.ToString();
        }
    }
}
