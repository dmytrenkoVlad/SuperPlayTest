using UnityEngine;

namespace Assets.Scripts.Bricks
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        public int MaxHealth => _maxHealth;
        public int CurrentHealth { get; private set; }
        public bool IsAlive => CurrentHealth > 0;

        public void DecreaseHealth(int damage)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        }

        public void ResetHealth()
        {
            CurrentHealth = MaxHealth;
        }
    }
}
