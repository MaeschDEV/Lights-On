using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;

public class Authenticator : MonoBehaviour
{
    [SerializeField] TMP_InputField Inputusername;
    [SerializeField] TMP_InputField Inputpassword;
    [SerializeField] GameObject Info;
    [SerializeField] TextMeshProUGUI InfoText;
    [SerializeField] TextMeshProUGUI PlayerInfo;

    async void Awake()
    {
        await UnityServices.InitializeAsync();
    }

    public async void SignUp()
    {
        await SignUpWithUsernamePassword(Inputusername.text, Inputpassword.text);
    }

    public async void SignIn()
    {
        await SignInWithUsernamePasswordAsync(Inputusername.text, Inputpassword.text);
    }

    async Task SignUpWithUsernamePassword(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            InfoText.text = "SignUp is successful.";
            Info.SetActive(true);
            await AuthenticationService.Instance.UpdatePlayerNameAsync(username);
            PlayerInfo.text = AuthenticationService.Instance.PlayerId + " | " + AuthenticationService.Instance.PlayerName;
        }
        catch (AuthenticationException ex)
        {
            InfoText.text = ex.Message;
            Info.SetActive(true);
        }
        catch (RequestFailedException ex)
        {
            InfoText.text = ex.Message;
            Info.SetActive(true);
        }
    }

    async Task SignInWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            InfoText.text = "SignIn is successful.";
            Info.SetActive(true);
            await AuthenticationService.Instance.UpdatePlayerNameAsync(username);            
            PlayerInfo.text = AuthenticationService.Instance.PlayerId + " | " + AuthenticationService.Instance.PlayerName;
        }
        catch (AuthenticationException ex)
        {
            InfoText.text = ex.Message;
            Info.SetActive(true);

        }
        catch (RequestFailedException ex)
        {
            InfoText.text = ex.Message;
            Info.SetActive(true);
        }
    }
}
