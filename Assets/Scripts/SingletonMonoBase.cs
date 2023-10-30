using UnityEngine;

#if UNITY_EDITOR
using System;
#endif

namespace AStar.Utils.DesignPattern.Singleton
{
    public abstract class SingletonMonoBase<T> : MonoBehaviour where T : SingletonMonoBase<T>
    {
        private static T m_Instance;

        public static T Instance
        #if UNITY_EDITOR
        {
            get
            {
                if (m_Instance == null)
                {
                    throw new Exception(
                        $"Class [{typeof(T)}] is LazySingletonMonoBase but the Instance called before Awake(). " +
                        $"Please add base.Awake() in the class [{typeof(T)}], " +
                        $"or add a GameObject with Component:[{typeof(T)}] in scene." +
                        $"It's very important to resolve this problem, because this check will only execute in edit mode." +
                        $"So null pointer dereference will absolutely occured in runtime.");
                }
                return m_Instance;
            }
        }
        #else
            => m_Instance;
        #endif

        public static T Create()
        {
            if (m_Instance != null) return m_Instance;
            T instance = new GameObject($"{typeof(T).Name}", typeof(T)).GetComponent<T>();
            return m_Instance;
        }
        
        public virtual void Awake()
        {
            T self = this as T;
            if (m_Instance == null)
            {
                m_Instance = self;
                DontDestroyOnLoad(m_Instance);
            }
            else if (m_Instance != self)
            {
                DestroyImmediate(m_Instance);
                m_Instance = self;
                DontDestroyOnLoad(m_Instance);
            }
        }
    }
}