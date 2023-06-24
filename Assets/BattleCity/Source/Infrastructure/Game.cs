using BattleCity.Source.StateMachine;
using VContainer;

namespace BattleCity.Source.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine { get; }

        public Game(IObjectResolver objectResolver)
        {
            StateMachine = new GameStateMachine(objectResolver);
        }
    }
}