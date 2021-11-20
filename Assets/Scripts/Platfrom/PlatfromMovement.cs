using UnityEngine;

namespace Assets.Scripts
{
    public class PlatfromMovement : MonoBehaviour
    {
        private Vector2 _leftBottomAreaPosition;
        private Vector2 _rightBottomAreaPosition;

        private float RealXSize => GetComponent<BoxCollider2D>().size.x * transform.localScale.x;

        public void InitializeMovementBorders(Vector2 left, Vector2 right)
        {
            _leftBottomAreaPosition = left;
            _rightBottomAreaPosition = right;
        }

        public void SetXOffset(float offset)
        {
            float newPosition = Mathf.Clamp(transform.position.x + offset, _leftBottomAreaPosition.x + RealXSize / 2, _rightBottomAreaPosition.x - RealXSize / 2);
            transform.position = new Vector2(newPosition, transform.position.y);
        }
    }
}
