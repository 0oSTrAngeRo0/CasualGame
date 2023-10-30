using UnityEngine;

namespace Game
{
    public interface IInteractive
    {
        public void OnSelected();
        public void OnUnselected();
        public void OnInteractive(IInteractive other);
    }
}