using Game;
using UnityEngine;

namespace Game
{
    public class MonoController : MonoBehaviour, IController
    {
        public IArchitecture GetArchitecture() => GameArchitecture.Interface;
    }
}