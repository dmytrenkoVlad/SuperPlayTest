using UnityEngine;

namespace Assets.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _leftBottomRenderPos;
        [SerializeField] private Transform _rightTopRenderPos;

        private void Start()
        {
            Vector2 displaySize = new Vector2(_rightTopRenderPos.position.x - _leftBottomRenderPos.position.x, _rightTopRenderPos.position.y - _leftBottomRenderPos.position.y);
            float screenRatio = (float)Screen.width / Screen.height;
            float targetRatio = displaySize.x / displaySize.y;

            if (screenRatio >= targetRatio)
            {
                _camera.orthographicSize = displaySize.y / 2;
            }
            else
            {
                float sizeDiff = targetRatio / screenRatio;
                _camera.orthographicSize = displaySize.y / 2 * sizeDiff;
            }
        }

    }
}
