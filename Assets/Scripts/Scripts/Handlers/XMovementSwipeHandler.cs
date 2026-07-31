using System;
using ChainCube.Scripts.Utils;
using UnityEngine;

namespace ChainCube.Scripts.Handlers
{
    public class XMovementSwipeHandler : MonoBehaviour, IMovableObjectHandler
    {
        [SerializeField] private Transform _leftBorder;
        [SerializeField] private Transform _rightBorder;

        [SerializeField, Range(0.5f, 1.5f)]
        private float _normalizedCoefficient = 1f;

        private GameObject _movableObject;
        private ISwipeDetector _swipeDetector;

        public void Inject(GameObject dependency)
        {
            _movableObject = dependency;
        }

        private void Start()
        {
            _swipeDetector = GetComponent<ISwipeDetector>();
            Subscribe();
        }

        private void Subscribe()
        {
            if (_swipeDetector == null)
                throw new NullReferenceException("SwipeDetector is missing.");

            _swipeDetector.onSwipe += OnSwipe;
            _swipeDetector.onSwipeEnd += OnSwipeEnd;
        }

        private void OnSwipe(Vector2 delta)
        {
            if (_movableObject == null)
                return;

            if (Mathf.Approximately(delta.x, 0f))
                return;

            float borderDistance = _rightBorder.position.x - _leftBorder.position.x;
            float offset = borderDistance * _normalizedCoefficient * delta.x / Screen.width;

            Vector3 position = _movableObject.transform.position;

            position.x += offset;
            position.x = Mathf.Clamp(position.x,
                _leftBorder.position.x,
                _rightBorder.position.x);

            _movableObject.transform.position = position;
        }

        private void OnSwipeEnd(Vector2 delta)
        {
            _movableObject = null;
        }

        private void OnDestroy()
        {
            if (_swipeDetector == null)
                return;

            _swipeDetector.onSwipe -= OnSwipe;
            _swipeDetector.onSwipeEnd -= OnSwipeEnd;
        }
    }
}