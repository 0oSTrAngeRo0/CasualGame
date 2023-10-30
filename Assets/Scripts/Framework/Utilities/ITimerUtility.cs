using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Game
{
    public class Timer
    {
        public float Remain;
        public Action OnFinish;

        public void RegisterOnFinish(Action onFinish)
        {
            if (onFinish == null) return;
            OnFinish += onFinish;
        }

        public void UnregisterOnFinish(Action onFinish)
        {
            if (onFinish == null) return;
            OnFinish -= onFinish;
        }
    }

    public interface ITimerUtility : IUtility
    {
        public Timer RegisterTimer(float duration, Action onFinish = null);
        public void UnregisterTimer(Timer timer);
    }
    
    public class TimerUtility : ITimerUtility
    {
        private readonly HashSet<Timer> m_Timers;
        private readonly List<Timer> m_RemovableTimers;

        public TimerUtility()
        {
            m_Timers = new HashSet<Timer>();
            m_RemovableTimers = new List<Timer>();
        }

        public Timer RegisterTimer(float duration, Action onFinish = null)
        {
            Timer timer = new Timer()
            {
                Remain = duration,
            };
            if (onFinish != null) timer.OnFinish += onFinish;
            m_Timers.Add(timer);
            return timer;
        }

        public void UnregisterTimer(Timer timer)
        {
            m_Timers.Remove(timer);
        }

        public void UpdateTimers(float delta)
        {
            m_RemovableTimers.Clear();
            foreach (Timer timer in m_Timers)
            {
                timer.Remain -= delta;
                if (timer.Remain <= 0) continue;
                m_RemovableTimers.Add(timer);
                if (timer.OnFinish == null) continue;
                timer.OnFinish();
            }

            m_RemovableTimers.ForEach(timer => m_Timers.Remove(timer));
        }
    }
}