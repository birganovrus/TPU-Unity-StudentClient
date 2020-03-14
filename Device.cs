using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Device: MonoBehaviour {

 private TextMeshPro DeviceText;
 public float DeviceData;
 public string DeviceName;
 public enum EventType {
  Success,
  Neutral,
  Critical
 }
 public string SuccessEvent_Log;
 public string NeutralEvent_Log;
 public string CriticalEvent_Log;
 public bool HasPlayerWorked;


 public GameHandler Manager;

 public EventType eventType;
 void Start() {
  eventType = EventType.Neutral;
  DeviceText = gameObject.GetComponentInChildren<TextMeshPro>();
 }

 public void RegisterEvent(EventType eventType, string data) {
  switch (eventType) {
   case EventType.Success:
    SetDeviceText("Данные: " + DeviceData.ToString());
    SuccessEvent_Log += data;
    break;
   case EventType.Neutral:
    SetDeviceText("Установка была скорректирована студентом");
    NeutralEvent_Log += data;
    break;
   case EventType.Critical:
    SetDeviceText("Были нарушены требования техники безопасности!");
    CriticalEvent_Log += data;
    break;

  }
 }

 public void DeviceInteraction(string EventButtonName) {

string message = "нет данных";
// Main logic of device's work init

//  if (HasPlayerWorked) {
//  HasPlayerWorked = false;
//  NeutralEvent_Log += "Устрановка выключена";
//  SetDeviceText("Устрановка выключена");
//  } else {
//    HasPlayerWorked = true;
//    NeutralEvent_Log += "Устрановка включена";
//    DeviceData = Random.Range(1f, 3f);
   
//    }

// Logic of events
 switch(EventButtonName){
     case("Success"):
        eventType = EventType.Success;
        message = "Студент выполнил задачу и получил корректные данные";
        break;
     case("Neutral"):
        eventType = EventType.Neutral;
        message = "Студент скорректировал установку";
        break;
     case("Critical"):
        eventType = EventType.Critical;
        message = "Студент нарушил правила техники безопасности";
        break;
 }
  RegisterEvent(eventType,message);
  Manager.UpdateDeviceData(gameObject);
 }
 public void SetDeviceText(string text){
  DeviceText.SetText(text); 
 }
  
}

