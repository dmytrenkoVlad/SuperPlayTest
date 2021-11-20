using Assets.Scripts.Ball;
using Assets.Scripts.Effects;
using System;
using UnityEngine;

namespace Assets.Scripts.Bricks
{
    public class BrickController : MonoBehaviour
    {
        [SerializeField] private BrickType _brickType;

        public event Action<BrickController> BrickDestroyed = delegate (BrickController brickController) { };
        public event Action<int, BrickType> BrickDamaged = delegate (int newHealth, BrickType brickType) { };

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out BallController ball))
                return;
            if (!TryGetComponent(out HealthComponent healthComponent))
                return;

            healthComponent.DecreaseHealth(ball.CurrentDamage);

            if (healthComponent.IsAlive)
            {
                BrickDamaged(healthComponent.CurrentHealth, _brickType);
            }
            else
            {
                if (TryGetComponent(out OnDestroyBaseEffectComponent onDestroyEffect))
                    onDestroyEffect.ApplyOnDestroyEffect(ball, ball.PlatformController);
                BrickDestroyed(this);
            }
        }
    }
}
