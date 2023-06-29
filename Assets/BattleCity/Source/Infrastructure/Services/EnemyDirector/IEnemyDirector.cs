using BattleCity.Source.PlayerLogic;

namespace BattleCity.Source.Infrastructure.Services.EnemyDirector
{
    public interface IEnemyDirector : IService
    {
        void PrepareEnemies(Player player);
    }
}