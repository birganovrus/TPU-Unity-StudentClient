using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device : MonoBehaviour
{
    public float DeviceData;
    public enum EventType {Success, Neutral, Critical}
    public string SuccessEvent_Log;
    public string NeutralEvent_Log;
    public string CriticalEvent_Log;
    public bool HasPlayerWorked;

    public GameHandler Manager;

    public EventType eventType;
    void Start(){
        eventType = EventType.Neutral;
    }

    public void RegisterEvent(){
        
        switch(eventType){
            case EventType.Success:
                break;
            case EventType.Neutral:
                break;
            case EventType.Critical:
                break;

        }
    }

    public void DeviceInteraction(){

        if(HasPlayerWorked) { HasPlayerWorked = false; DeviceLog+= "Пользователь выключил установку; "; gameObject.transform.GetComponentInChildren<TextMesh>().text = "Устрановка выключена";}
        else{ 
            HasPlayerWorked = true;
            DeviceData = Random.Range(1f,3f);
            DeviceLog += "Пользователь запустил установку; ";
            gameObject.transform.GetComponentInChildren<TextMesh>().text = DeviceData.ToString();
        }

       Manager.UpdateDeviceData(gameObject);
    }

}
