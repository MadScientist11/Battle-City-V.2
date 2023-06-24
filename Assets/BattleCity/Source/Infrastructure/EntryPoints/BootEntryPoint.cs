using System.Collections.Generic;
using BattleCity.Source.Infrastructure.Services;
using BattleCity.Source.Infrastructure.Services.MazeService;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleCity.Source.Infrastructure.EntryPoints
{
    public class BootEntryPoint : MonoBehaviour
    {
        [SerializeField] private LifetimeScope _gameLifeScope;
        
        private IReadOnlyList<IService> _allServices;
        private IObjectResolver _objectResolver;
        private IMazeManager _mazeManager;

        [Inject]
        public void Construct(IMazeManager mazeManager, IReadOnlyList<IService> allServices, IObjectResolver objectResolver)
        {
            _mazeManager = mazeManager;
            _objectResolver = objectResolver;
            _allServices = allServices;
        }

        private void Awake() => 
            DontDestroyOnLoad(_gameLifeScope);

        private void Start()
        {
            Game game = new Game(_objectResolver);
            game.StateMachine.EnterBootState(_allServices);
        }
    }
}