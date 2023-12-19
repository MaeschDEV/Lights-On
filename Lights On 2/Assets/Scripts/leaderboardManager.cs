using Newtonsoft.Json;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class leaderboardManager : MonoBehaviour
{
    List<LeaderboardEntry> entries = new List<LeaderboardEntry>();

    [SerializeField] private GameObject module;
    [SerializeField] private GameObject easyModuleParent;
    [SerializeField] private GameObject normalModuleParent;
    [SerializeField] private GameObject hardModuleParent;

    public async void AddScore(string id, int multiplier)
    {
        var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(id, multiplier);
        Debug.Log(JsonConvert.SerializeObject(playerEntry));
    }

    public async void GetScoresEasy()
    {
        foreach(Transform child in easyModuleParent.transform)
        {
            Destroy(child.gameObject);
        }
        LeaderboardScoresPage scoresResponse = await LeaderboardsService.Instance.GetScoresAsync("Easy");
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));

        entries = scoresResponse.Results;

        easyModuleParent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, entries.Count * 200);
        foreach (var entry in entries)
        {
            GameObject newModule = Instantiate(module, easyModuleParent.transform);
            newModule.transform.Find("Rank").GetComponent<TextMeshProUGUI>().text = "#" + (entry.Rank + 1);
            if(AuthenticationService.Instance.PlayerName == entry.PlayerName)
            {
                newModule.transform.Find("Name").GetComponent<TextMeshProUGUI>().color = Color.red;
            }
            newModule.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = entry.PlayerName.Substring(0, entry.PlayerName.Length - 5);
            newModule.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = entry.Score.ToString();
        }
    }

    public async void GetScoresNormal()
    {
        foreach (Transform child in normalModuleParent.transform)
        {
            Destroy(child.gameObject);
        }
        LeaderboardScoresPage scoresResponse = await LeaderboardsService.Instance.GetScoresAsync("Easy");
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));

        entries = scoresResponse.Results;

        normalModuleParent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, entries.Count * 200);
        foreach (var entry in entries)
        {
            GameObject newModule = Instantiate(module, normalModuleParent.transform);
            newModule.transform.Find("Rank").GetComponent<TextMeshProUGUI>().text = "#" + (entry.Rank + 1);
            if (AuthenticationService.Instance.PlayerName == entry.PlayerName)
            {
                newModule.transform.Find("Name").GetComponent<TextMeshProUGUI>().color = Color.red;
            }
            newModule.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = entry.PlayerName.Substring(0, entry.PlayerName.Length - 5);
            newModule.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = entry.Score.ToString();
        }
    }

    public async void GetScoresHard()
    {
        foreach (Transform child in hardModuleParent.transform)
        {
            Destroy(child.gameObject);
        }
        LeaderboardScoresPage scoresResponse = await LeaderboardsService.Instance.GetScoresAsync("Easy");
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));

        entries = scoresResponse.Results;

        hardModuleParent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, entries.Count * 200);
        foreach (var entry in entries)
        {
            GameObject newModule = Instantiate(module, hardModuleParent.transform);
            newModule.transform.Find("Rank").GetComponent<TextMeshProUGUI>().text = "#" + (entry.Rank + 1);
            if (AuthenticationService.Instance.PlayerName == entry.PlayerName)
            {
                newModule.transform.Find("Name").GetComponent<TextMeshProUGUI>().color = Color.red;
            }
            newModule.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = entry.PlayerName.Substring(0, entry.PlayerName.Length - 5);
            newModule.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = entry.Score.ToString();
        }
    }
}
