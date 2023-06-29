using BattleCity.Source.PlayerLogic;
using VContainer;

namespace BattleCity.Source.Infrastructure.Services.PlayerStatus
{
    public class PlayerStatusProvider : IPlayerStatusProvider
    {
        public PlayerStatus PlayerStatus { get; private set; }
        private PlayerConfiguration _playerConfiguration;

        [Inject]
        public void Construct(PlayerConfiguration playerConfiguration)
        {
            _playerConfiguration = playerConfiguration;
        }

        public void Initialize()
        {
            PlayerStatus = new PlayerStatus(_playerConfiguration);
        }
    }
}