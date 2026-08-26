using UnityEngine;
using ChainCube.Scripts.Rewards;

namespace ChainCube.Scripts.Cube
{
    [RequireComponent(typeof(PointsContainerCollisionDetector), typeof(PointsContainer))]
    public class CollisionMergePointsContainer : MonoBehaviour
    {
        private PointsContainer _score;
        private PointsContainerCollisionDetector _detector;

        [SerializeField] private GameObject _mergeEffect;
        [SerializeField] private CubePool _cubePool;

        private ScoreManager _scoreManager;
        private RewardSystem _rewardSystem;
        private MergeChainTracker _mergeChainTracker;

        private void Start()
        {
            _score = GetComponent<PointsContainer>();
            _detector = GetComponent<PointsContainerCollisionDetector>();

            _scoreManager = FindObjectOfType<ScoreManager>();
            _rewardSystem = FindObjectOfType<RewardSystem>();
            _mergeChainTracker = FindObjectOfType<MergeChainTracker>();

            Subscribe();
        }

        private void OnPointsContainerCollision(PointsContainer col)
        {
            if (col == null)
                return;

            if (col.points == _score.points)
            {
                // Куб, який летів, залишається.
                // Його значення збільшується.
                _score.points *= 2;

                // Merge Chain
                _mergeChainTracker?.RegisterMerge();

                // Reward за досягнення значення кубика
                _rewardSystem?.CheckReward(
                    RewardTrigger.ReachValue,
                    _score.points);

                // Бонусний час
                float bonus = GameTimer.Instance.AddMergeTime(_score.points);

                FloatingTextSpawner.Instance.Show(
                    $"+{bonus:0.#}s",
                    transform.position);

                // Очки
                _scoreManager?.AddScore(_score.points);

                // Звук
                SoundManager.Instance?.PlayMerge();

                // Particle
                if (_mergeEffect != null)
                {
                    var effect = Instantiate(
                        _mergeEffect,
                        transform.position,
                        Quaternion.identity);

                    Destroy(effect, 1f);
                }

                // Тряска камери
                var cam = Camera.main.GetComponent<CameraShake>();

                if (cam != null)
                {
                    StartCoroutine(cam.Shake(0.1f, 0.1f));
                }

                // Другий кубик ПОВНІСТЮ прибираємо зі сцени
                if (col.gameObject != null)
                {
                    col.gameObject.SetActive(false);

                    if (_cubePool != null)
                    {
                        _cubePool.Return(col.gameObject);
                    }
                }
            }
        }

        private void Subscribe()
        {
            _detector.onCollisionContinue += OnPointsContainerCollision;
        }

        private void Unsubscribe()
        {
            if (_detector == null)
                return;

            _detector.onCollisionContinue -= OnPointsContainerCollision;
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}