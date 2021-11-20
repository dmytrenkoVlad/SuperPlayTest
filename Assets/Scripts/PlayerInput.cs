using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        public event Action PlayerClicked = delegate () { };
        public event Action<Vector3, Vector3> PlayerMovedCursor = delegate (Vector3 oldPosition, Vector3 newPosition) { };

        private Vector3 _lastCursorPosition;
        private bool _isDrag;

        private void Update()
        {
            if (Input.GetMouseButton(0) && Input.mousePosition != _lastCursorPosition && _isDrag)
            {
                Vector3 position = _camera.ScreenToWorldPoint(Input.mousePosition);
                PlayerMovedCursor(_lastCursorPosition, position);
                _lastCursorPosition = position;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _lastCursorPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                _isDrag = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                PlayerClicked();
                _isDrag = false;
            }
        }
    }
}
