using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    private const string LastSkyboxKey = "LastSkybox";

    [Header("Default Skyboxes")]
    [SerializeField] private Material[] _defaultSkyboxes;

    private void Start()
    {
        SetRandomSkybox();
    }

    private void SetRandomSkybox()
    {
        if (_defaultSkyboxes == null ||
            _defaultSkyboxes.Length == 0)
        {
            Debug.LogError("SkyboxManager: No skyboxes assigned!");
            return;
        }

        int lastIndex =
            PlayerPrefs.GetInt(LastSkyboxKey, -1);

        int newIndex;

        if (_defaultSkyboxes.Length == 1)
        {
            newIndex = 0;
        }
        else
        {
            do
            {
                newIndex = Random.Range(
                    0,
                    _defaultSkyboxes.Length
                );
            }
            while (newIndex == lastIndex);
        }

        RenderSettings.skybox =
            _defaultSkyboxes[newIndex];

        PlayerPrefs.SetInt(
            LastSkyboxKey,
            newIndex
        );

        PlayerPrefs.Save();

        DynamicGI.UpdateEnvironment();
    }
}    

