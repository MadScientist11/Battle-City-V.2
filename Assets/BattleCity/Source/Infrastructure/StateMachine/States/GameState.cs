using BattleCity.Source.Infrastructure.Services.GameFactory;
using BattleCity.Source.Infrastructure.Services.MazeService;
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
            mazeManager.CreateMazeModel();
            gameFactory.CreateMazeView();

            PlayerView player = gameFactory.CreatePlayer(mazeManager.GetPlayerStartPosition());
            cameraFollow.SetTarget(player);
        }

        public void Exit()
        {
        }
    }
}