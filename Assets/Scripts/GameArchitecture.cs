using QFramework;

namespace Game
{
    public class GameArchitecture : Architecture<GameArchitecture>
    {
        protected override void Init()
        {
            // Utilities
            RegisterUtility<ILogUtility>(new DebugLogUtility());

            // Models

            // Systems

        }
    }
}