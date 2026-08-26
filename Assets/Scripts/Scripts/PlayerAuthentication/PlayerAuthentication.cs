using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class PlayerAuthentication : MonoBehaviour
{
    private async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        Debug.Log(
            $"Player ID: {AuthenticationService.Instance.PlayerId}");
    }
}