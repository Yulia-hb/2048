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
            _scoreManager = FindObjectOfType<ScoreManager>();
            Subscribe();
        }


        private void OnPointsContainerCollision(PointsContainer col)
        {
            if (col.points == _score.points)
            {
                _score.points *= 2;

                // Додаємо бонусний час
                float bonus = GameTimer.Instance.AddMergeTime(_score.points);

                FloatingTextSpawner.Instance.Show(
                    $"+{bonus:0.#}s",
                    transform.position);

                // Додаємо очки
                _scoreManager?.AddScore(_score.points);

                // Звук об'єднання
                SoundManager.Instance?.PlayMerge();

                // Particle
                if (_mergeEffect != null)
                {
                    var effect = Instantiate(_mergeEffect, transform.position, Quaternion.identity);
                    Destroy(effect, 1f);
                }

                // Тряска камери
                var cam = Camera.main.GetComponent<CameraShake>();

                if (cam != null)
                {
                    StartCoroutine(cam.Shake(0.1f, 0.1f));
                }

                // Видаляємо другий куб
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
