using BattleCity.Source.Enemies;
using BattleCity.Source.Infrastructure.Services.PlayerStatus;
using UnityEngine;
using VContainer;

namespace BattleCity.Source.PlayerLogic
{
    public class Player : MonoBehaviour, IDamageable
    {
        private IPlayerStatusProvider _playerStatusProvider;

        [Inject]
        public void Construct(IPlayerStatusProvider playerStatusProvider)
        {
            _playerStatusProvider = playerStatusProvider;
        }

        private void Start()
        {
            _playerStatusProvider.PlayerStatus.OnHealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            _playerStatusProvider.PlayerStatus.OnHealthChanged -= OnHealthChanged;
        }

        public void TakeDamage(int amount)
        {
            _playerStatusProvider.PlayerStatus.SetHealth(_playerStatusProvider.PlayerStatus.Health - amount);
        }

        private void OnHealthChanged(int health)
        {
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}