using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using SimpleJSON;

public class GameHandler : MonoBehaviour
{
    [Space]
    [Header("TextMesh Links")]
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI Username;
    public TextMeshProUGUI Device1Text;
    public TextMeshProUGUI Device2Text;

    public MenuHandler MenuManager;

    public Device dev1;
    public Device dev2;
    private float playedTimeInSec = 0f;
    private float playedTimeInMin = 0f;
    private float playedTimeInHr = 0f;

    private string SplayedTimeInSec;
    private string SplayedTimeInMin;
    private string SplayedTimeInHr;
    void Start()
    {
        Username.SetText(UserData.Username);
    }

    void Update()
    {
        UpdateTime();

        if(Input.GetKeyDown(KeyCode.F)){
            Debug.Log("Save Key is pressed");
            SaveData();
        }
        
    }

    public void SaveData(){
        JSONArray deviceArray = new JSONArray();
        JSONClass sessionsData = new JSONClass();

        //Sessions Array that contais only IDs of the sessions
        int CurrentSessionID = UserData.SessionCount + 1;
        
        Debug.Log(CurrentSessionID);

        sessionsData.Add("Продолжительность сессии",SplayedTimeInHr + ":" + SplayedTimeInMin + ":" + SplayedTimeInSec);
        sessionsData.Add("Время создания сессии",System.DateTime.Now.ToString());
        
        foreach(Device dev in FindObjectsOfType<Device>()){
            JSONNode deviceObj = new JSONClass();
            JSONClass deviceData= new JSONClass();
            JSONClass deviceEvents = new JSONClass();
            deviceObj.Add("Имя установки",dev.DeviceName);
            deviceObj.Add("События установки",deviceEvents);
            deviceObj.Add("Данные установки",deviceData);
            deviceEvents.Add("Нейтральные",dev.NeutralEvent_Log);
            deviceEvents.Add("Критические", dev.CriticalEvent_Log);
            deviceEvents.Add("Успешные", dev.SuccessEvent_Log);
            deviceData.Add("Данные", dev.DeviceData.ToString());
            deviceArray.Add(deviceObj);
        }
        sessionsData.Add("Установки",deviceArray);
        MenuManager.OnSavingData(UserData.Username,UserData.Password,sessionsData.ToString(),CurrentSessionID);
        
    }
    public void UpdateDeviceData(GameObject device){
        
        switch(device.transform.name){
            case "Установка 1" : Device1Text.SetText(device.transform.name + ": " 
                + device.GetComponent<Device>().DeviceData.ToString());
                break;
            case "Установка 2" : Device2Text.SetText(device.transform.name + ": " 
                + device.GetComponent<Device>().DeviceData.ToString());
                break;
        }
    }
    public void UpdateTime(){

         playedTimeInSec += Time.deltaTime;
         if(playedTimeInSec >= 59) { playedTimeInSec = 0; 
                                    playedTimeInMin+=1f;}

         if(playedTimeInMin >= 59) {playedTimeInMin = 0; 
                                    playedTimeInSec = 0; 
                                    playedTimeInHr += 1f;}

        if(playedTimeInSec<9) { SplayedTimeInSec = "0" + Mathf.RoundToInt(playedTimeInSec).ToString();}
            else { SplayedTimeInSec = Mathf.RoundToInt(playedTimeInSec).ToString();}
        
        if(playedTimeInMin<9) { SplayedTimeInMin = "0" + Mathf.RoundToInt(playedTimeInMin).ToString();}
            else { SplayedTimeInMin = Mathf.RoundToInt(playedTimeInMin).ToString();}
        
        if(playedTimeInHr<9) { SplayedTimeInHr = "0" + Mathf.RoundToInt(playedTimeInHr).ToString();}
            else { SplayedTimeInHr = Mathf.RoundToInt(playedTimeInHr).ToString();}

         TimeText.SetText(SplayedTimeInHr 
         + ":" + SplayedTimeInMin
         + ":" + SplayedTimeInSec);
    }
}

    
