using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device : MonoBehaviour
{
    public float DeviceData;
    public string DeviceLog;
    public bool HasPlayerWorked;

    public GameHandler Manager;

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
