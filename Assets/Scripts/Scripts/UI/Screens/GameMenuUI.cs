using UnityEngine;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _gameMenuScreen;

    public void OpenMenu()
    {
        _gameMenuScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
