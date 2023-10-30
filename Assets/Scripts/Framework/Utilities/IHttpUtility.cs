using System;
using System.Collections;
using System.Text;
using AStar.Utils.DesignPattern.Singleton;
using UnityEngine.Networking;

namespace Game
{
    public static class HttpConfig
    {
        public const string URL = "";
    }

    public interface IHttpUtility : IUtility
    {
        public void Get(Action<string> callback, string url = HttpConfig.URL);
        public void Post(string json, Action<string> callback, string url = HttpConfig.URL);
    }

    public class HttpUtility : SingletonMonoBase<HttpUtility>, IHttpUtility
    {
        public void Get(Action<string> callback, string url = HttpConfig.URL) =>
            StartCoroutine(GetInternal(url, callback));

        public void Post(string json, Action<string> callback, string url = HttpConfig.URL) =>
            StartCoroutine(PostInternal(url, Encoding.UTF8.GetBytes(json), callback));

        private IEnumerator GetInternal(string url, Action<string> callback)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();
            Callback(request, callback);
        }

        private IEnumerator PostInternal(string url, byte[] content, Action<string> callback)
        {
            UploadHandler uploader = new UploadHandlerRaw(content);
            UnityWebRequest request = new UnityWebRequest(url, "POST", new DownloadHandlerBuffer(), uploader);
            yield return request.SendWebRequest();
            Callback(request, callback);
        }

        private void Callback(UnityWebRequest request, Action<string> callback)
        {
            if (callback == null) return;
            if (!request.isDone || request.result != UnityWebRequest.Result.Success) return;
            callback.Invoke(request.downloadHandler.text);
        }
    }
}