using System;
using BattleCity.Source.PlayerLogic;

namespace BattleCity.Source.Infrastructure.Services.PlayerStatus
{
    public class PlayerStatus
    {
        public int Health { get; private set; }

        public event Action<int> OnHealthChanged; 

        private PlayerConfiguration _playerConfiguration;

        public PlayerStatus(PlayerConfiguration playerConfiguration)
        {
            _playerConfiguration = playerConfiguration;
        }

        public void SetHealth(int hp)
        {
            Health = hp;
            OnHealthChanged?.Invoke(hp);
        }
        
        
    }
}