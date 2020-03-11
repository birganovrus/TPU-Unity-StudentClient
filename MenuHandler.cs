using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuHandler : MonoBehaviour
{
    [SerializeField]
    private NodeAPI _api;

    [SerializeField]
    private string _dataForSave = string.Empty;
    [SerializeField]
    private TextMeshProUGUI ErrorText;

    [SerializeField]
    private TMP_InputField _usernameField, _passwordField;

    public string username;
    public string password;
    // Use this for initialization
    void Awake()
    {
        if (_api == null)
            _api = FindObjectOfType<NodeAPI>();

    }

    public void LoadMainMenu(){
        SceneManager.LoadScene(0);
    }

    public void OnRegistation(){
        _api.Register(_usernameField.text, _passwordField.text, (bool error, string data) =>
        {
            if (error){
                Debug.LogError("Error:" + data);
                var json = SimpleJSON.JSON.Parse(data);
                string message = json["message"];
                Debug.Log(json["message"]);
                if(message.Contains("already"))
                    ErrorText.SetText("пользователь уже существует!");

            }
            else
            {
                Debug.Log("Response:" + data);
                UserData.Username = _usernameField.text;
                UserData.Password = _passwordField.text;
                UserData.SessionCount = 0;
                LoadGame();
            }
        });
    }

    public void OnAuthorization(){
        _api.GetData(_usernameField.text, _passwordField.text, (bool error, string data) =>
        {
            if (error){
                Debug.LogError("Error:" + data);
                var json = SimpleJSON.JSON.Parse(data);
                string message = json["message"];
                Debug.Log(json["message"]);
                if(message.Contains("password"))
                    ErrorText.SetText("введите пароль!");

                if(message.Contains("found"))
                    ErrorText.SetText("пользователь не найден!");

            }
            else
            {
                var json = SimpleJSON.JSON.Parse(data);
                Debug.Log("Response:" + data);
                UserData.Username = _usernameField.text;
                UserData.Password = _passwordField.text;
                string SessionCountStr = json["account"]["data"]["SessionsCount"];
                UserData.SessionCount =  int.Parse(SessionCountStr);
                Debug.Log(UserData.SessionCount);
                LoadGame();
            }
        });
    }
     public void OnSavingData(string _username, string _password, string _data, int sessionscount)
    {
        _api.SaveData(_username, _password, _data,sessionscount, (bool error, string data) =>
        {
            if (error)
                Debug.LogError("Error:" + data);
            else{
                Debug.Log("Response:" + data);
                Debug.Log("All data is saved!");
            }
        });
    }
    public void LoadAuthorization(){
        SceneManager.LoadScene(2);
    }

    public void LoadRegistration(){
        SceneManager.LoadScene(1);
    }
    public void LoadGame(){
        SceneManager.LoadScene(3);
    }
}
