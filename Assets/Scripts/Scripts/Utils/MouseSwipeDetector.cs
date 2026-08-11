using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainCube.Scripts.Utils
{
    public class MouseSwipeDetector : MonoBehaviour, ISwipeDetector
    {
        public event Action<Vector2> onSwipeStart;
        public event Action<Vector2> onSwipe;
        public event Action<Vector2> onSwipeEnd;

        private bool _isSwipe;
        private Vector2 _lastMousePosition;

        private void Update()
        {
#if UNITY_EDITOR
            HandleMouse();
#else
        HandleTouch();
#endif
        }

        private void HandleTouch()
        {
            if (Input.touchCount == 0)
                return;

            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    _isSwipe = true;

                    onSwipeStart?.Invoke(Vector2.zero);

                    break;

                case TouchPhase.Moved:

                    if (!_isSwipe)
                        return;

                    // Unity сама дає зміщення пальця
                    Vector2 delta = touch.deltaPosition;

                    onSwipe?.Invoke(delta);

                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (!_isSwipe)
                        return;

                    _isSwipe = false;

                    onSwipeEnd?.Invoke(Vector2.zero);

                    break;
            }
        }

        private void HandleMouse()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isSwipe = true;
                _lastMousePosition = Input.mousePosition;

                onSwipeStart?.Invoke(Vector2.zero);
            }

            if (Input.GetMouseButton(0))
            {
                if (!_isSwipe)
                    return;

                Vector2 currentPosition = Input.mousePosition;
                Vector2 delta = currentPosition - _lastMousePosition;

                onSwipe?.Invoke(delta);

                _lastMousePosition = currentPosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (!_isSwipe)
                    return;

                _isSwipe = false;

                onSwipeEnd?.Invoke(Vector2.zero);
            }
        }

        private void OnDisable()
        {
            _isSwipe = false;
        }
    }
}