using BattleCity.Source.Infrastructure.EntryPoints;
using BattleCity.Source.Infrastructure.Services.GameFactory;
using BattleCity.Source.Infrastructure.Services.MazeService;
using BattleCity.Source.MazeGeneration;
using BattleCity.Source.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleCity.Source.Infrastructure.Scopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerConfiguration _playerConfiguration;
        [SerializeField] private MazeConfiguration _mazeConfiguration;

        protected override void Configure(IContainerBuilder builder)
        {
            BindConfigs(builder);
            BindServices(builder);
            builder.Register<IMazeGenerator, MazeGenerator>(Lifetime.Singleton);
        }

        private static void BindServices(IContainerBuilder builder)
        {
            builder.Register<MazeManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GameFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AssetProvider>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void BindConfigs(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerConfiguration);
            builder.RegisterInstance(_mazeConfiguration);
        }
    }
}