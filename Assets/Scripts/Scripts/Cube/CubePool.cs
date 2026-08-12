using System.Collections.Generic;
using UnityEngine;
using ChainCube.Scripts.Cube;

namespace ChainCube.Scripts.Cube
{
    public class CubePool : MonoBehaviour
    {
        [SerializeField] private GameObject _cubePrefab;
        [SerializeField] private Transform _poolContainer;

        private readonly Queue<GameObject> _pool = new Queue<GameObject>();

        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            GameObject cube;

            if (_pool.Count > 0)
            {
                cube = _pool.Dequeue();
            }
            else
            {
                cube = Instantiate(_cubePrefab, _poolContainer);
            }

            ResetCube(cube);

            cube.transform.SetPositionAndRotation(position, rotation);
            cube.SetActive(true);

            return cube;
        }

        public void Return(GameObject cube)
        {
            if (cube == null)
                return;

            Rigidbody rb = cube.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            cube.SetActive(false);
            cube.transform.SetParent(_poolContainer);

            _pool.Enqueue(cube);
        }

        private void ResetCube(GameObject cube)
        {
            Rigidbody rb = cube.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            PointsContainer pointsContainer =
                cube.GetComponent<PointsContainer>();

            if (pointsContainer != null)
            {
                pointsContainer.ResetPoints();
            }
        }
    }
}