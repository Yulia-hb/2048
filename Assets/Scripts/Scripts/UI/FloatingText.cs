using DG.Tweening;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;

    [Header("Animation")]
    [SerializeField] private float _moveUp = 2.5f;
    [SerializeField] private float _duration = 3f;
    [SerializeField] private float _scale = 1.1f;

    private FloatingTextPool _pool;

    public void Initialize(FloatingTextPool pool)
    {
        _pool = pool;
    }

    public void Show(string message, Color color, Vector3 worldPosition)
    {
        transform.position = worldPosition;

        _text.text = message;
        _text.color = color;

        transform.localScale = Vector3.one;

        gameObject.SetActive(true);

        // дивимось на камеру
        if (Camera.main != null)
            transform.forward = Camera.main.transform.forward;

        DOTween.Kill(transform);
        DOTween.Kill(_text);

        _text.alpha = 1f;

        float randomX = Random.Range(-0.3f, 0.3f);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            transform.DOMove(
                transform.position + new Vector3(randomX, _moveUp, 0f),
                _duration));

        sequence.Join(transform.DOScale(_scale, 0.2f));

        sequence.Join(_text.DOFade(0f, _duration));

        sequence.OnComplete(ReturnToPool);
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
        _pool.Release(this);
    }
}