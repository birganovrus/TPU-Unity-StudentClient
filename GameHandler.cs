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
        JSONClass playerJson = new JSONClass();
        JSONArray sessionsArray = new JSONArray();
        JSONClass sessionsDataArray = new JSONClass();

        //Sessions Array that contais only IDs of the sessions
        int CurrentSessionID = UserData.SessionCount + 1;
        
        Debug.Log(CurrentSessionID);
        //Sessions Data Array
        string Device1Data;
        string Device2Data;
        string Device1Log;
        string Device2Log;

        if(dev1.DeviceData != 0) { Device1Data = dev1.DeviceData.ToString();}
        else{ Device1Data = "none";}

        if(dev2.DeviceData != 0) { Device2Data = dev2.DeviceData.ToString();}
        else{ Device2Data = "none";}

        if(dev1.DeviceLog != string.Empty) { Device1Log = dev1.DeviceLog;}
        else {Device1Log = "none";}

        if(dev2.DeviceLog != string.Empty) { Device2Log = dev2.DeviceLog;}
        else {Device2Log = "none";}



        sessionsDataArray.Add("Session_Time",SplayedTimeInHr + ":" + SplayedTimeInMin + ":" + SplayedTimeInSec);
        sessionsDataArray.Add("Session_DateTime",System.DateTime.Now.ToString());
        sessionsDataArray.Add("Установка 1 Данные",Device1Data);
        sessionsDataArray.Add("Установка 2 Данные",Device2Data);
        sessionsDataArray.Add("Лог установки 1", Device1Log);
        sessionsDataArray.Add("Лог установки 2", Device2Log);
        //sessionsArray.Add("Session__data", sessionsDataArray);
        //playerJson.Add("Session " + CurrentSessionID.ToString(),sessionsArray);


        MenuManager.OnSavingData(UserData.Username,UserData.Password,sessionsDataArray.ToString(),CurrentSessionID);
        
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

    
