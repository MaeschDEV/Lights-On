using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : MonoBehaviour
{
    [SerializeField] private Button loginButton;

    [SerializeField] private TextMeshProUGUI userIdText;

    [SerializeField] private Authenticator authenticator;

    private void OnEnable()
    {
        loginButton.onClick.AddListener(LoginButtonPressed);
        authenticator.OnSignedIn += Authenticator_OnSignedIn;
    }

    private void Authenticator_OnSignedIn(PlayerInfo playerInfo, string playerName)
    {
        userIdText.text = playerInfo.Id + " | " + playerName;
    }

    private async void LoginButtonPressed()
    {
        await authenticator.InitSignIn();
    }

    private void OnDisable()
    {
        loginButton.onClick.RemoveListener(LoginButtonPressed);
        authenticator.OnSignedIn -= Authenticator_OnSignedIn;

    }
}
