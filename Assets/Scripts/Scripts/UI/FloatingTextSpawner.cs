using UnityEngine;

public class FloatingTextSpawner : MonoBehaviour
{
    public static FloatingTextSpawner Instance { get; private set; }

    [SerializeField] private FloatingTextPool _pool;

    private void Awake()
    {
        Instance = this;
    }

    public void Show(string text, Vector3 worldPosition)
    {
        FloatingText floating = _pool.Get();

        floating.Show(
            text,
            //new Color(1f, 0.1f, 0.6f)
            new Color(1f, 0f, 0.55f),
            worldPosition + Vector3.up * 0.5f);
    }
}
