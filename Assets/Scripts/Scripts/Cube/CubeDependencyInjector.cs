using ChainCube.Scripts.Utils;
using UnityEngine;

public class CubeDependencyInjector : MonoBehaviour
{
    [SerializeField] private GameObject _cube;

    private IDependency<GameObject>[] _dependencies;

    public GameObject Cube
    {
        get => _cube;

        set
        {
            if (value == null || !value.scene.IsValid())
                return;

            _cube = value;

            Inject();
        }
    }

    private void Awake()
    {
        _dependencies = GetComponents<IDependency<GameObject>>();
    }

    private void Start()
    {
        if (_cube != null)
            Inject();
    }

    private void Inject()
    {
        if (_dependencies == null)
            _dependencies = GetComponents<IDependency<GameObject>>();

        foreach (var dependency in _dependencies)
        {
            if (dependency != null)
                dependency.Inject(_cube);
        }
    }
}