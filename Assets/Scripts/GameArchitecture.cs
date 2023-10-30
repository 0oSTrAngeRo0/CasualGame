using Game;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class GameArchitecture : Architecture<GameArchitecture>
    {
        protected override void Init()
        {
            // Utilities
            RegisterUtility<ILogUtility>(new DebugLogUtility());
            RegisterUtility<IStorageUtility>(new JsonStorageUtility());
            RegisterUtility<ITimerUtility>(new TimerUtility());
            RegisterUtility<IHttpUtility>(HttpUtility.Create());

            // Models
            this.RegisterModel<IGrabModel>(new GrabModel());
            
            // Systems

        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void ResetStaticVariables()
        {
            mArchitecture = null;
        }
    }
}