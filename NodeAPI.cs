using UnityEngine;
using System.Collections;
using System;

public class NodeAPI : MonoBehaviour
{
    public enum Method { GET, POST }

    [SerializeField]
    private string BASE_URL = "localhost:3000/api/";
    [SerializeField]
    private float WAIT_TIMEOUT = 10.0f;
    [SerializeField]
    private Method _selectedMethod = Method.GET;

    public delegate void ResultCallback(bool error, string data);

    public void Register(string username, string password, ResultCallback callback)
    {
        var form = new WWWForm();

        form.AddField("username", username);
        form.AddField("password", password);

       // var www = new WWW(BASE_URL + "register", form);

        //if (_selectedMethod == Method.GET)
          var  www = new WWW(BASE_URL + "register?username=" + username + "&password=" + password);

        SendRequest(www, callback);
    }

    public void GetData(string username, string password, ResultCallback callback)
    {
        var form = new WWWForm();

        form.AddField("username", username);
        form.AddField("password", password);

       // var www = new WWW(BASE_URL + "getData", form);

        //if (_selectedMethod == Method.GET)
          var  www = new WWW(BASE_URL + "getData?username=" + username + "&password=" + password);

        SendRequest(www, callback);
    }

    public void SaveData(string username, string password, string data,int sessionscount, ResultCallback callback)
    {
        var form = new WWWForm();

        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("data", data);
        form.AddField("sessionscount",sessionscount);

        //var www = new WWW(BASE_URL + "saveData", form);

        //if (_selectedMethod == Method.GET)
          var  www = new WWW(BASE_URL + "saveData?username=" + username + "&password=" + password + "&data=" + data + "&sessionscount=" +sessionscount);

        SendRequest(www, callback);
    }

    
    private void SendRequest(WWW www, ResultCallback callback)
    {
        Debug.Log("Node API: send request to " + www.url);

        StartCoroutine(ExecuteRequest(www, callback));
    }

    private IEnumerator ExecuteRequest(WWW www, ResultCallback callback)
    {
        float elapsedTime = 0.0f;

        while (!www.isDone)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= WAIT_TIMEOUT)
            {
                if (callback != null)
                    callback(true, "{\"status\":400,\"message\":\"local timeout!\"}");

                yield break;
            }

            yield return null;
        }

        if (!www.isDone || !string.IsNullOrEmpty(www.error) || string.IsNullOrEmpty(www.text))
        {
            if (callback != null)
                callback(true, "{\"status\":400,\"message\":\"" + www.error + "\"}");

            yield break;
        }

        var response = www.text;

        try
        {
            var json = SimpleJSON.JSON.Parse(response);

            if (json["status"] != null && json["status"].AsInt != 200)
            {
                if (callback != null)
                    callback(true, response);

                yield break;
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        if (callback != null)
            callback(false, response);
    }
}