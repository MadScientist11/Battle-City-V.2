using BattleCity.Source.StateMachine;

namespace BattleCity.Source.Infrastructure.StateMachine
{
    public class LoadGameStateParams : IStateParams
    {
        public string ScenePath;

        public LoadGameStateParams(string scenePath)
        {
            ScenePath = scenePath;
        }
    }
    
    public class LoadGameState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadGameState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter<T>(T stateParams) where T : IStateParams
        {
            LoadGameStateParams loadGameStateParams = (stateParams as LoadGameStateParams);
            _sceneLoader.Load(loadGameStateParams.ScenePath, () => _gameStateMachine.EnterGameState());
        }

        public void Exit()
        {
        }
    }
}