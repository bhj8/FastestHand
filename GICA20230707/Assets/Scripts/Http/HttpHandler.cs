using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class HttpHandler : MonoBehaviour
{
    public delegate void RequestCompletedDelegate(UnityWebRequest request);

    public IEnumerator PostJsonRequest(string uri, string jsonBody, RequestCompletedDelegate onCompleted)
    {
        var request = new UnityWebRequest(uri, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"POST request failed: {request.error}");
        }
        else if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"POST request completed. Response: {request.downloadHandler.text}");
            onCompleted?.Invoke(request);
        }
    }



    

    // 示例调用
public void SendScore_Relax(float time, string name, RequestCompletedDelegate onCompleted)
{
    string url = "http://45.77.46.232:3000/updatescores/";
    string jsonBody = $"{{\"completion_time\":\"{time}\", \"username\":\"{name}\"}}";

    StartCoroutine(PostJsonRequest(url, jsonBody, onCompleted));
}

public void GetScore_Relax(RequestCompletedDelegate onCompleted)
{
    string url = "http://45.77.46.232:3000/getscores/";
    string jsonBody = $"";

    StartCoroutine(PostJsonRequest(url, jsonBody, onCompleted));
}


}
