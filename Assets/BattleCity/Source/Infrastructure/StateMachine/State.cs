namespace BattleCity.Source.Infrastructure.StateMachine
{
    public interface IState
    {
        void Enter<T>(T stateParams) where T : IStateParams;

        void Exit();
    }

 
}