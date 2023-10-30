using System;

namespace Game
{
    public class FullBindableProperty<T>
    {
        public static Func<T, T, bool> Comparer { get; set; } = (a, b) => a.Equals(b);

        private T m_Value;
        private Action<T, T> m_OnValueChanged;

        public T Value
        {
            get => m_Value;
            set
            {
                if (value == null && m_Value == null) return;
                if (value != null && Comparer(value, m_Value)) return;
                m_OnValueChanged?.Invoke(m_Value, value);
                m_Value = value;
            }
        }

        public FullBindableProperty(T defaultValue = default)
        {
            m_Value = defaultValue;
        }

        public FullBindableProperty<T> WithComparer(Func<T, T, bool> comparer)
        {
            Comparer = comparer;
            return this;
        }

        public void SetValueWithoutEvent(T newValue) => m_Value = newValue;

        public void UnRegister(Action<T, T> onValueChanged) => m_OnValueChanged -= onValueChanged;

        public IUnRegister Register(Action<T, T> onValueChanged)
        {
            m_OnValueChanged += onValueChanged;
            return new FullBindablePropertyUnRegister<T>(this, m_OnValueChanged);
        }
    }

    public class FullBindablePropertyUnRegister<T> : IUnRegister
    {
        private FullBindableProperty<T> m_BindableProperty;

        public Action<T, T> m_OnValueChanged;

        public FullBindablePropertyUnRegister(FullBindableProperty<T> bindableProperty, Action<T, T> onValueChanged)
        {
            m_BindableProperty = bindableProperty;
            m_OnValueChanged = onValueChanged;
        }

        public void UnRegister()
        {
            m_BindableProperty.UnRegister(m_OnValueChanged);
            m_BindableProperty = null;
            m_OnValueChanged = null;
        }
    }
}