using Gameframe.SaveLoad;
using Game;

namespace Game
{
    public interface IStorageUtility : IUtility
    {
        public void Save<T>(string key, T value);
        public T Load<T>(string key, T value);  
    }

    public sealed class JsonStorageUtility : IStorageUtility
    {

        public JsonStorageUtility()
        {
        }

        public void Save<T>(string key, T value)
        {
        }

        public T Load<T>(string key, T value)
        {
            return value;
        }
    }
}