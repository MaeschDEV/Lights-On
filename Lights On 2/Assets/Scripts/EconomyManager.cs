using Unity.Services.Authentication;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    private PlayerBalance playerBalance;
    private bool synced = false;

    private async void Update()
    {
        if(AuthenticationService.Instance.IsSignedIn && !synced)
        {
            await EconomyService.Instance.Configuration.SyncConfigurationAsync();
            synced = true;
        }
    }

    private async void getCurrencyBalance(string currencyId)
    {
        CurrencyDefinition currencyDefinition = EconomyService.Instance.Configuration.GetCurrency(currencyId);
        playerBalance = await currencyDefinition.GetPlayerBalanceAsync();
    }

    public async void addCurrencyBalance(string currencyId, int amount)
    {
        PlayerBalance playerBalance = await EconomyService.Instance.PlayerBalances.IncrementBalanceAsync(currencyId, amount);
    }

    public void returnCurrencyBalance(string currencyId)
    {
        getCurrencyBalance(currencyId);
        Debug.Log(playerBalance.ToString());
    }
}
