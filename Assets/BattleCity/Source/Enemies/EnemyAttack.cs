using System;
using BattleCity.Source.Infrastructure.Services.GameFactory;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityTimer;
using VContainer;

namespace BattleCity.Source.Enemies
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private Transform _projectileSpawnPoint;

        private RaycastHit2D[] _results;

        private IGameFactory _gameFactory;
        private Timer _fireTimer;
        private EnemyAI _enemyAi;

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        private void Start()
        {
            _results = new RaycastHit2D[4];

            _enemyAi = GetComponent<EnemyAI>();

            _fireTimer = Timer.Register(2f, FireProjectile, isLooped: true);
            _fireTimer.Pause();
        }

        private void Update()
        {
            if (_enemyAi.IsInFrontOfTarget)
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