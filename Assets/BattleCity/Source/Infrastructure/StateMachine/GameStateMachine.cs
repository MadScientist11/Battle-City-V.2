using System;
using System.Collections.Generic;
using BattleCity.Source.Infrastructure;
using BattleCity.Source.Infrastructure.Services;
using BattleCity.Source.Infrastructure.StateMachine;
using BattleCity.Source.Infrastructure.StateMachine.States;
using VContainer;

namespace BattleCity.Source.StateMachine
{
    public class GameStateMachine
    {
        private readonly IObjectResolver _objectResolver;
        private readonly Dictionary<Type, IState> _states;

        public GameStateMachine(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootState)] = new BootState(this),
                [typeof(LoadGameState)] = new LoadGameState(this, new SceneLoader()),
                [typeof(GameState)] = new GameState(this, objectResolver),
            };
        }

        public void EnterBootState(IReadOnlyList<IService> services) => 
            _states[typeof(BootState)].Enter(new BootStateParams(services));

        public void EnterLoadGameState(string scenePath) => 
            _states[typeof(LoadGameState)].Enter(new LoadGameStateParams(scenePath));

        public void EnterGameState() => 
            _states[typeof(GameState)].Enter(new EmptyStateParams());
    }
}