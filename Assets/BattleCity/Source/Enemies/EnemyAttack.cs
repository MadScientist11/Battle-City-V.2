using BattleCity.Source.Infrastructure.Services.GameFactory;
using UnityEngine;
using UnityTimer;
using VContainer;

namespace BattleCity.Source.Enemies
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private Transform _projectileSpawnPoint;

        private IGameFactory _gameFactory;
        private Timer _fireTimer;
        private EnemyMovement _enemyMovement;

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        private void Start()
        {
            _enemyMovement = GetComponent<EnemyMovement>();

            _fireTimer = Timer.Register(2f, FireProjectile, isLooped: true);
            _fireTimer.Pause();
        }

        private void Update()
        {
            if (_enemyMovement.IsInFrontOfTarget)
            {
                _fireTimer.Resume();
            }
            else
            {
                _fireTimer.Pause();
            }
        }

        private void FireProjectile()
        {
            _gameFactory.GetOrCreateProjectile(gameObject, _projectileSpawnPoint.position,
                Quaternion.LookRotation(Vector3.forward, transform.up));
        }
    }
}