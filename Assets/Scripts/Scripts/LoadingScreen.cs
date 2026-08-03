using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image _background;
    [SerializeField] private Image _fillImage;

    [Header("Settings")]
    [SerializeField] private string _sceneToLoad = "Game";
    [SerializeField] private float _loadingDuration = 3f;

    private void Start()
    {
        _fillImage.fillAmount = 0f;

        if (_background != null)
        {
            Color color = _background.color;
            color.a = 0f;
            _background.color = color;

            _background.DOFade(1f, 0.5f);
        }

        StartCoroutine(LoadRoutine());
    }

    private IEnumerator LoadRoutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneToLoad);
        operation.allowSceneActivation = false;

        Tween tween = DOTween.To(
            () => _fillImage.fillAmount,
            x => _fillImage.fillAmount = x,
            1f,
            _loadingDuration)
            .SetEase(Ease.Linear);

        yield return tween.WaitForCompletion();

        operation.allowSceneActivation = true;
    }
}