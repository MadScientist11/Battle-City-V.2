namespace BattleCity.Source.Infrastructure.Services.PlayerStatus
{
    public interface IPlayerStatusProvider : IService
    {
        PlayerStatus PlayerStatus { get; }
    }
}