using BattleCity.Source.Infrastructure.Services.EnemyDirector;
using BattleCity.Source.Infrastructure.Services.GameFactory;
using BattleCity.Source.Infrastructure.Services.MazeService;
using BattleCity.Source.MazeGeneration;
using BattleCity.Source.PlayerLogic;
using BattleCity.Source.StateMachine;
using UnityEngine;
using VContainer;

namespace BattleCity.Source.Infrastructure.StateMachine.States
{
    public class GameState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IObjectResolver _objectResolver;

        public GameState(GameStateMachine stateMachine, IObjectResolver objectResolver)
        {
            _stateMachine = stateMachine;
            _objectResolver = objectResolver;
        }

        public void Enter<T>(T stateParams) where T : IStateParams
        {
            CameraFollow cameraFollow = Object.FindObjectOfType<CameraFollow>();
            IMazeManager mazeManager = _objectResolver.Resolve<IMazeManager>();
            IGameFactory gameFactory = _objectResolver.Resolve<IGameFactory>();
            IRouteSearch pathfinder = _objectResolver.Resolve<IRouteSearch>();
            IEnemyDirector enemyDirector = _objectResolver.Resolve<IEnemyDirector>();

            SetUpMaze(mazeManager, gameFactory);

            pathfinder.SetUpPathfinding();

            Player player = gameFactory.CreatePlayer(mazeManager.GetPlayerStartPosition());
            cameraFollow.SetTarget(player);

            enemyDirector.PrepareEnemies(player);
        }

        private void SetUpMaze(IMazeManager mazeManager, IGameFactory gameFactory)
        {
            mazeManager.CreateMazeModel();
            MazeView mazeView = gameFactory.CreateMazeView();
            mazeManager.SetView(mazeView);
        }
        public void CreateMazeView()
        {
        }

        public void Exit()
        {
        }
    }
}