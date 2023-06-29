using BattleCity.Source.Infrastructure.Services.GameFactory;
using UnityEngine;
using VContainer;

namespace BattleCity.Source.PlayerLogic
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _projectileSpawnPoint;
        private IGameFactory _gameFactory;

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        private void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                FireProjectile();
            }
        }

        private void FireProjectile()
        {
            _gameFactory.GetOrCreateProjectile(gameObject, _projectileSpawnPoint.position,
                Quaternion.LookRotation(Vector3.forward, _playerTransform.up));
        }
    }
}
