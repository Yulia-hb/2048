using UnityEngine;

namespace ChainCube.Scripts.Cube
{
    [RequireComponent(typeof(PointsContainerCollisionDetector), typeof (PointsContainer))]
    public class CollisionMergePointsContainer : MonoBehaviour
    {
        private PointsContainer _score;
        private PointsContainerCollisionDetector _detector;
        [SerializeField] private GameObject _mergeEffect;
        private ScoreManager _scoreManager;

        private void Start()
        {
            _score = GetComponent<PointsContainer>();
            _detector = GetComponent<PointsContainerCollisionDetector>();
            _scoreManager = FindObjectOfType<ScoreManager>(); // норм для зараз
            Subscribe();
        }

        private void OnPointsContainerCollision(PointsContainer col)
        {
            if (col.points == _score.points)
            {
                _score.points *= 2;
                _scoreManager?.AddScore(_score.points);

                // 💥 PARTICLE
                if (_mergeEffect != null)
                {
                    var effect = Instantiate(_mergeEffect, transform.position, Quaternion.identity);
                    Destroy(effect, 1f);
                }

                // 🎥 CAMERA SHAKE
                var cam = Camera.main.GetComponent<CameraShake>();

                if (cam != null)
                {
                    StartCoroutine(cam.Shake(0.1f, 0.1f));
                }

                // ❗ Destroy
                if (col != null && col.gameObject.scene.IsValid())
                {
                    Destroy(col.gameObject);
                }
            }
        }

        private void Subscribe()
        {
            _detector.onCollisionContinue += OnPointsContainerCollision;
        }
        
        private void Unsubscribe()
        {
            _detector.onCollisionContinue -= OnPointsContainerCollision;
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}
