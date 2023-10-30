using Game;

namespace Game
{
    // Things held by hands
    public interface IGrabModel : IModel
    {
        public BindableProperty<IGrabbable> CurrentGrab { get; }
        public FullBindableProperty<IInteractive> CurrentInteractive { get; }
    }

    public class GrabModel : AbstractModel, IGrabModel
    {
        private BindableProperty<IGrabbable> m_CurrentGrab;
        public BindableProperty<IGrabbable> CurrentGrab => m_CurrentGrab;
        private FullBindableProperty<IInteractive> m_CurrentInteractive;
        public FullBindableProperty<IInteractive> CurrentInteractive => m_CurrentInteractive;

        protected override void OnInit()
        {
            m_CurrentGrab = new BindableProperty<IGrabbable>(null);
            m_CurrentInteractive = new FullBindableProperty<IInteractive>(null);
        }
    }
}