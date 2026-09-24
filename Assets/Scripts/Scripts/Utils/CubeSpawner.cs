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
            _swipeDetector =
                _swipeDetectorObject.GetComponent<ISwipeDetector>();

            if (_cubePool == null)
            {
                Debug.LogError("CubePool is not assigned!");
                return;
            }

            if (_swipeDetector == null)
            {
                Debug.LogError("SwipeDetector is not found!");
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
            if (_spawnRoutine != null)
                return;

            _spawnRoutine =
                StartCoroutine(SpawnWithDelay());
        }

        private IEnumerator SpawnWithDelay()
        {
            yield return new WaitForSeconds(_spawnDelay);

            SpawnCube();

            _spawnRoutine = null;
        }

        private void SpawnCube()
        {
            GameObject instance =
                _cubePool.Get(
                    transform.position,
                    Quaternion.identity
                );

            RandomPointsGenerator generator =
                instance.GetComponent<RandomPointsGenerator>();

            if (generator != null)
                generator.Generate();

            InjectCube(instance);
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