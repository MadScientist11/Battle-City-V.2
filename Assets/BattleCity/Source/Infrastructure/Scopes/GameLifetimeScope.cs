using BattleCity.Source.Infrastructure.EntryPoints;
using BattleCity.Source.Infrastructure.Services.EnemyDirector;
using BattleCity.Source.Infrastructure.Services.GameFactory;
using BattleCity.Source.Infrastructure.Services.MazeService;
using BattleCity.Source.MazeGeneration;
using BattleCity.Source.PlayerLogic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleCity.Source.Infrastructure.Scopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerConfiguration _playerConfiguration;
        [SerializeField] private MazeConfiguration _mazeConfiguration;
        [SerializeField] private ProjectileConfiguration _projectileConfiguration;

        protected override void Configure(IContainerBuilder builder)
        {
            BindConfigs(builder);
            builder.Register<IMazeGenerator, MazeGenerator>(Lifetime.Singleton);
            BindServices(builder);
        }

        private static void BindServices(IContainerBuilder builder)
        {
            builder.Register<MazeManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GameFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AssetProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AStarRouteSearch>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemyDirector>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void BindConfigs(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerConfiguration);
            builder.RegisterInstance(_mazeConfiguration);
            builder.RegisterInstance(_projectileConfiguration);
        }
    }
}