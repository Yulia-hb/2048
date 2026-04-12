using UnityEngine;

namespace ChainCube.Scripts.Cube
{
    [RequireComponent(typeof(PointsContainerCollisionDetector), typeof (PointsContainer))]
    public class CollisionMergePointsContainer : MonoBehaviour
    {
        private PointsContainer _score;
        private PointsContainerCollisionDetector _detector;

        private void Start()
        {
            _score = GetComponent<PointsContainer>();
            _detector = GetComponent<PointsContainerCollisionDetector>();
            Subscribe();
        }

        private void OnPointsContainerCollision(PointsContainer col)
        {
            if (col == null) return;

            if (col.points == _score.points)
            {
                _score.points *= 2;

                // ❗ ВАЖЛИВО
                if (col.gameObject.scene.IsValid())
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
