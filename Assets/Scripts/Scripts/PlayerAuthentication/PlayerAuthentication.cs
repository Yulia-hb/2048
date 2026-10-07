using System;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class PlayerAuthentication : MonoBehaviour
{
    private const string NicknameOwnerKey = "NicknameOwnerPlayerId";

    private readonly string[] _adjectives =
    {
        "Lucky",
        "Happy",
        "Brave",
        "Magic",
        "Pixel",
        "Tiny",
        "Wild",
        "Golden",
        "Crazy",
        "Blue"
    };

    private readonly string[] _names =
    {
        "Fox",
        "Wolf",
        "Panda",
        "Dragon",
        "Cube",
        "Cat",
        "Bear",
        "Tiger",
        "Rabbit",
        "Raven"
    };

    public string PlayerId =>
        AuthenticationService.Instance.PlayerId;

    public string PlayerName =>
        AuthenticationService.Instance.PlayerName;

    private async void Start()
    {
        try
        {
            await InitializePlayerAsync();
        }
        catch (Exception exception)
        {
            Debug.LogError(
                $"PLAYER AUTH ERROR: {exception.Message}"
            );
        }
    }

    private async Task InitializePlayerAsync()
    {
        // 1. Запускаємо Unity Gaming Services.
        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            await UnityServices.InitializeAsync();
        }

        // 2. Авторизуємо гравця.
        // Якщо session token вже є,
        // Unity поверне того самого PlayerId.
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance
                .SignInAnonymouslyAsync();
        }

        string playerId =
            AuthenticationService.Instance.PlayerId;

        Debug.Log($"PLAYER ID: {playerId}");

        // 3. Перевіряємо, чи ми вже створювали
        // nickname саме для цього PlayerId.
        string nicknameOwner =
            PlayerPrefs.GetString(
                NicknameOwnerKey,
                string.Empty
            );

        if (nicknameOwner == playerId)
        {
            // Ім'я вже існує в UGS.
            // Отримуємо його, щоб воно було
            // доступне в AuthenticationService.PlayerName.
            string existingName =
                await AuthenticationService.Instance
                    .GetPlayerNameAsync();

            Debug.Log(
                $"RETURNING PLAYER: {existingName}"
            );

            return;
        }

        // 4. Це новий PlayerId.
        // Генеруємо НАШ nickname.
        string nickname =
            GenerateNickname();

        // 5. Записуємо nickname в Unity Authentication.
        string createdName =
            await AuthenticationService.Instance
                .UpdatePlayerNameAsync(nickname);

        // 6. Лише після успішного запису в UGS
        // запам'ятовуємо, що цей PlayerId
        // вже отримав nickname.
        PlayerPrefs.SetString(
            NicknameOwnerKey,
            playerId
        );

        PlayerPrefs.Save();

        Debug.Log(
            $"NEW PLAYER: {createdName}"
        );
    }

    private string GenerateNickname()
    {
        string adjective =
            _adjectives[
                UnityEngine.Random.Range(
                    0,
                    _adjectives.Length
                )
            ];

        string name =
            _names[
                UnityEngine.Random.Range(
                    0,
                    _names.Length
                )
            ];

        return adjective + name;
    }
}