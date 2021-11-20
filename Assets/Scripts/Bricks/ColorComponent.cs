using System;
using UnityEngine;

namespace Assets.Scripts.Bricks
{
    public class ColorComponent : MonoBehaviour
    {
        [SerializeField] private Color _threeHPColor;
        [SerializeField] private Color _twoHPColor;
        [SerializeField] private Color _oneHPColor;

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
            switch (health)
            {
                case 1:
                    return _oneHPColor;
                case 2:
                    return _twoHPColor;
                case 3:
                    return _threeHPColor;
                default:
                    throw new ArgumentException($"Unexpected amount of Health - {health}");
            }
        }

        private void OnDestroy()
        {
            _brickController.BrickDamaged -= OnDamageReceived;
        }
    }
}
