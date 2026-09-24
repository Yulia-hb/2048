using UnityEngine;

namespace ChainCube.Scripts.Cube
{
    [RequireComponent(typeof(PointsContainer))]
    public class RandomPointsGenerator : MonoBehaviour
    {
        [SerializeField] private byte _minDegree = 1;
        [SerializeField] private byte _maxDegree = 4;

        private PointsContainer _pointsContainer;

        private const byte defaultMinDegree = 1;
        private const byte defaultMaxDegree = 4;

        private void Awake()
        {
            NormalizeDegree();

            _pointsContainer =
                GetComponent<PointsContainer>();
        }

        private void Start()
        {
            Generate();
        }

        public void Generate()
        {
            if (_pointsContainer == null)
            {
                _pointsContainer =
                    GetComponent<PointsContainer>();
            }

            _pointsContainer.points =
                (long)Mathf.Pow(
                    2,
                    Random.Range(_minDegree, _maxDegree)
                );
        }

        private void NormalizeDegree()
        {
            if (_maxDegree > _minDegree)
                return;

            Debug.LogError(
                "RandomPointsGenerator: invalid degree range."
            );

            _minDegree = defaultMinDegree;
            _maxDegree = defaultMaxDegree;
        }
    }
}