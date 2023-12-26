using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;

public class Authenticator : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField usernameInputAgain;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject changeUsernameMenu;
    [SerializeField] private GameObject errorMenu;
    [SerializeField] private GameObject accountMenu;
    [SerializeField] private GameObject LoginButton;
    [SerializeField] private GameObject BalanceDisplayButton;
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private TextMeshProUGUI playerInfoText;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI balance;
    [SerializeField] private EconomyManager economyManager;

    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch(Exception ex)
        {
            mainMenu.SetActive(false);
            changeUsernameMenu.SetActive(false);
            errorMenu.SetActive(true);
            errorText.text = "Exception: " + ex.Message;
            AudioManager.Instance.PlaySFX("Error");
        }
        PlayerAccountService.Instance.SignedIn += SignInWithUnity;
    }

    private void Start()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            if (AuthenticationService.Instance.SessionTokenExists)
            {
                InitSignIn();
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            economyManager.returnCurrencyBalance("COIN");
        }
    }

    private async void SignInWithUnity()
    {
        try
        {
            var accessToken = PlayerAccountService.Instance.AccessToken;
            await SignInWithUnityAsync(accessToken);
        }
        catch(Exception ex)
        {
            mainMenu.SetActive(false);
            changeUsernameMenu.SetActive(false);
            errorMenu.SetActive(true);
            errorText.text = "Exception: " + ex.Message;
            AudioManager.Instance.PlaySFX("Error");
        }
    }

    public async void InitSignIn()
    {
        if(!AuthenticationService.Instance.IsSignedIn)
        {
            if (AuthenticationService.Instance.SessionTokenExists)
            {
                try
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                    Debug.Log("SignIn is successful. (Anonymously)");
                    playerInfoText.text = AuthenticationService.Instance.PlayerId + " | " + AuthenticationService.Instance.PlayerName;
                    playerNameText.text = AuthenticationService.Instance.PlayerName;
                    LoginButton.SetActive(false);
                    BalanceDisplayButton.SetActive(true);
                }
                catch
                {
                    try
                    {
                        await PlayerAccountService.Instance.StartSignInAsync();
                    }
                    catch (Exception e)
                    {
                        mainMenu.SetActive(false);
                        changeUsernameMenu.SetActive(false);
                        errorMenu.SetActive(true);
                        errorText.text = "Exception: " + e.Message;
                        AudioManager.Instance.PlaySFX("Error");
                    }
                }
            }
            else
            {
                try
                {
                    await PlayerAccountService.Instance.StartSignInAsync();
                }
                catch (Exception ex)
                {
                    mainMenu.SetActive(false);
                    changeUsernameMenu.SetActive(false);
                    errorMenu.SetActive(true);
                    errorText.text = "Exception: " + ex.Message;
                    AudioManager.Instance.PlaySFX("Error");
                }
            }
        }
        else
        {
            mainMenu.SetActive(false);
            accountMenu.SetActive(true);
        }
        AudioManager.Instance.PlaySFX("Click");
    }

    public async void ChangeUsername()
    {
        try
        {
            await AuthenticationService.Instance.UpdatePlayerNameAsync(usernameInput.text);
            playerInfoText.text = AuthenticationService.Instance.PlayerId + " | " + AuthenticationService.Instance.PlayerName;
            playerNameText.text = AuthenticationService.Instance.PlayerName;

            changeUsernameMenu.SetActive(false);
            accountMenu.SetActive(true);
        }
        catch(Exception ex)
        {
            changeUsernameMenu.SetActive(false);
            errorMenu.SetActive(true);
            errorText.text = "Exception: " + ex.Message;
            AudioManager.Instance.PlaySFX("Error");
        }
    }

    public async void ChangeUsernameAgain()
    {
        try
        {
            await AuthenticationService.Instance.UpdatePlayerNameAsync(usernameInputAgain.text);
            playerInfoText.text = AuthenticationService.Instance.PlayerId + " | " + AuthenticationService.Instance.PlayerName;
            playerNameText.text = AuthenticationService.Instance.PlayerName;
        }
        catch (Exception ex)
        {
            accountMenu.SetActive(false);
            errorMenu.SetActive(true);
            errorText.text = "Exception: " + ex.Message;
            AudioManager.Instance.PlaySFX("Error");
        }
        AudioManager.Instance.PlaySFX("Click");
    }

    public void Logout()
    {
        try
        {
            PlayerAccountService.Instance.SignOut();
            AuthenticationService.Instance.SignOut();
            AuthenticationService.Instance.ClearSessionToken();
            accountMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        catch(Exception ex)
        {
            mainMenu.SetActive(false);
            changeUsernameMenu.SetActive(false);
            errorMenu.SetActive(true);
            errorText.text = "Exception: " + ex.Message;
            AudioManager.Instance.PlaySFX("Error");
        }
        AudioManager.Instance.PlaySFX("Click");
    }

    public async void Delete()
    {
        try
        {
            await AuthenticationService.Instance.DeleteAccountAsync();
            accountMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        catch (Exception ex)
        {
            mainMenu.SetActive(false);
            changeUsernameMenu.SetActive(false);
            errorMenu.SetActive(true);
            errorText.text = "Exception: " + ex.Message;
            AudioManager.Instance.PlaySFX("Error");
        }
        AudioManager.Instance.PlaySFX("Click");
    }


    async Task SignInWithUnityAsync(string accessToken)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
            Debug.Log("SignIn is successful. (WithUnity)");
            if (AuthenticationService.Instance.PlayerName == null)
            {
                mainMenu.SetActive(false);
                changeUsernameMenu.SetActive(true);
            }
            else
            {
                mainMenu.SetActive(false);
                accountMenu.SetActive(true);
            }
            playerInfoText.text = AuthenticationService.Instance.PlayerId + " | " + AuthenticationService.Instance.PlayerName;
            playerNameText.text = AuthenticationService.Instance.PlayerName;
            LoginButton.SetActive(false);
            BalanceDisplayButton.SetActive(true);
        }
        catch(AuthenticationException ex)
        {
            mainMenu.SetActive(false);
            changeUsernameMenu.SetActive(false);
            errorMenu.SetActive(true);
            errorText.text = "AuthenticationException: " + ex.Message;
            AudioManager.Instance.PlaySFX("Error");
        }
        catch(RequestFailedException ex)
        {
            mainMenu.SetActive(false);
            changeUsernameMenu.SetActive(false);
            errorMenu.SetActive(true);
            errorText.text = "RequestFailedException: " + ex.Message;
            AudioManager.Instance.PlaySFX("Error");
        }
    }
}
