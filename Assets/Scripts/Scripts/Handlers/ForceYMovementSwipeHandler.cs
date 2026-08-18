using ChainCube.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainCube.Scripts.Handlers
{
    public class ForceYMovementSwipeHandler : MonoBehaviour, IMovableObjectHandler
    {
        [SerializeField]
        private float _force = 1.0f;

        private Rigidbody _movableRigidbody;
        private ISwipeDetector _swipeDetector;

        public void Inject(GameObject dependency)
        {
            _movableRigidbody = dependency.GetComponent<Rigidbody>();
        }

        private void Start()
        {
            ISwipeDetector swipeDetector = (ISwipeDetector)GetComponent<MouseSwipeDetector>();
            _swipeDetector = swipeDetector;
            Subscribe();
        }

        private void Subscribe()
        {
            if (_swipeDetector == null)
                throw new NullReferenceException("");

            _swipeDetector.onSwipeEnd += OnSwipeEnd;
        }

        private void OnSwipeEnd(Vector2 delta)
        {
            if (_movableRigidbody == null)
                return;

            _movableRigidbody.AddForce(
                _movableRigidbody.transform.forward * _force,
                ForceMode.Impulse
            );

            SoundManager.Instance?.Vibrate();

            _movableRigidbody = null;
        }
        private void OnDestroy()
        {
            Unsubscribe(); ;
        }

        private void Unsubscribe()
        {
            if (_swipeDetector == null)
                return;

            _swipeDetector.onSwipeEnd -= OnSwipeEnd;
        }
    }
}

