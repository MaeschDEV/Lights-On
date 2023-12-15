using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;

public class Authenticator : MonoBehaviour
{
    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
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
            Debug.LogException(ex);
        }
    }

    public void InitSignIn()
    {
        PlayerAccountService.Instance.StartSignInAsync();
    }


    async Task SignInWithUnityAsync(string accessToken)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
            Debug.Log("SignIn is successful.");
        }
        catch(AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch(RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }
}
