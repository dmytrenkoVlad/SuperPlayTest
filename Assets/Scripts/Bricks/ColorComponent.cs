using System;
using UnityEngine;

namespace Assets.Scripts.Bricks
{
    public class ColorComponent : MonoBehaviour
    {
        private static Color[] _colors = new Color[]
            {
                new Color(0, 1, 0.2f, 1), //green
                new Color(1, 0.56f, 0, 1), //orange
                new Color(1, 0.3f, 0.25f, 1), //red
            };

        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private BrickController _brickController;
        [SerializeField] private HealthComponent _healthComponent;

        private void Awake()
        {
            _sprite.color = GetColorByHealth(_healthComponent.MaxHealth);
            _brickController.BrickDamaged += OnDamageReceived;
        }

        public void ResetColor()
        {
            _sprite.color = GetColorByHealth(_healthComponent.MaxHealth);
        }

        private void OnDamageReceived(int newHealth, BrickType type)
        { 
            if(_healthComponent.IsAlive)
                _sprite.color = GetColorByHealth(newHealth);
        }

        private Color GetColorByHealth(int health)
        {
            return _colors[health - 1];
        }

        private void OnDestroy()
        {
            _brickController.BrickDamaged -= OnDamageReceived;
        }
    }
}
