using System.Collections.Generic;
using BattleCity.Source.Infrastructure.Services;
using BattleCity.Source.Infrastructure.StateMachine;
using UnityEngine;
using IState = BattleCity.Source.Infrastructure.StateMachine.IState;

namespace BattleCity.Source.StateMachine
{
    public class BootStateParams : IStateParams
    {
        public readonly IReadOnlyList<IService> Services;

        public BootStateParams(IReadOnlyList<IService> services)
        {
            Services = services;
        }
    }
    public class BootState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private IReadOnlyList<IService> _services;

        public BootState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void InitializeServices(IReadOnlyList<IService> services)
        {
            foreach (IService service in services)
            {
                service.Initialize();
            }
        }

        public void Enter<T>(T stateParams) where T : IStateParams
        {
            BootStateParams bootStateParams = stateParams as BootStateParams;
            InitializeServices(bootStateParams.Services);
            _stateMachine.EnterLoadGameState(GameConstants.Scenes.GamePath);
        }

        public void Exit()
        {
        }
    }
}