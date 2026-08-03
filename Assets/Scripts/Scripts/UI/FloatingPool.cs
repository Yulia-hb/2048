using System.Collections.Generic;
using UnityEngine;

public class FloatingTextPool : MonoBehaviour
{
    [SerializeField] private FloatingText _prefab;
    [SerializeField] private int _poolSize = 15;

    private readonly Queue<FloatingText> _pool = new();

    private void Awake()
    {
        for (int i = 0; i < _poolSize; i++)
            Create();
    }

    private void Create()
    {
        FloatingText text = Instantiate(_prefab, transform);

        text.Initialize(this);
        text.gameObject.SetActive(false);

        _pool.Enqueue(text);
    }

    public FloatingText Get()
    {
        if (_pool.Count == 0)
            Create();

        FloatingText text = _pool.Dequeue();

        return text;
    }

    public void Release(FloatingText text)
    {
        _pool.Enqueue(text);
    }
}