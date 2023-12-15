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
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private TextMeshProUGUI playerInfoText;
    [SerializeField] private TextMeshProUGUI playerNameText;

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
        }
        PlayerAccountService.Instance.SignedIn += SignInWithUnity;
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
                }
            }
        }
        else
        {
            mainMenu.SetActive(false);
            accountMenu.SetActive(true);
        }
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
        }
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
        }
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
        }
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
        }
        catch(AuthenticationException ex)
        {
            mainMenu.SetActive(false);
            changeUsernameMenu.SetActive(false);
            errorMenu.SetActive(true);
            errorText.text = "AuthenticationException: " + ex.Message;
        }
        catch(RequestFailedException ex)
        {
            mainMenu.SetActive(false);
            changeUsernameMenu.SetActive(false);
            errorMenu.SetActive(true);
            errorText.text = "RequestFailedException: " + ex.Message;
        }
    }
}
