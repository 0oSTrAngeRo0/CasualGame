using UnityEngine;

namespace Game
{
    public class InteractiveModel : MonoBehaviour, IInteractive, IGrabbable
    {
        public Material Selected;
        public Material UnSelected;
        public MeshRenderer MeshRenderer;
        public void OnSelected()
        {
            MeshRenderer.material = Selected;
        }

        public void OnUnselected()
        {
            MeshRenderer.material = UnSelected;
        }

        public void OnInteractive(IInteractive other)
        {
        }
    }
}