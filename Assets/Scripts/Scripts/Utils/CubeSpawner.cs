using ChainCube.Scripts.Cube;
using System.Collections;
using UnityEngine;

namespace ChainCube.Scripts.Utils
{
    [RequireComponent(typeof(ISwipeDetector))]
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnDelay = 0.3f;
        [SerializeField] private CubePool _cubePool;
        [SerializeField] private GameObject _swipeDetectorObject;

        private ISwipeDetector _swipeDetector;
        private Coroutine _spawnRoutine;

        private void Start()
        {
            _swipeDetector = _swipeDetectorObject.GetComponent<ISwipeDetector>();

            if (_cubePool == null)
            {
                Debug.LogError("CubePool is not assigned!");
                return;
            }

            Subscribe();
        }

        private void Subscribe()
        {
            _swipeDetector.onSwipeEnd += OnSwipeEnd;
        }

        private void Unsubscribe()
        {
            if (_swipeDetector == null)
                return;

            _swipeDetector.onSwipeEnd -= OnSwipeEnd;
        }

        private void OnSwipeEnd(Vector2 delta)
        {
            if (_spawnRoutine == null)
                _spawnRoutine = StartCoroutine(SpawnWithDelay());
        }

        private IEnumerator SpawnWithDelay()
        {
            yield return new WaitForSeconds(_spawnDelay);

            GameObject instance = _cubePool.Get(
                transform.position,
                Quaternion.identity
            );

            InjectCube(instance);

            _spawnRoutine = null;
        }

        private void InjectCube(GameObject cube)
        {
            var dependencies =
                FindObjectsByType<CubeDependencyInjector>(
                    FindObjectsSortMode.None
                );

            foreach (var dependency in dependencies)
            {
                if (dependency != null)
                    dependency.Cube = cube;
            }
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}
