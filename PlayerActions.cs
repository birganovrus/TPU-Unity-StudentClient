using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    public bool CanInteract = false;
    RaycastHit HitInfo;
    Ray RayOrigin;
   public ActionUIPopup Popup;
   public Device DeviceObject;
       void Update()
        {
            if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out HitInfo, 1.5f)){
                if(HitInfo.transform.tag == "device"){ Popup.ShowPopup(true); CanInteract = true;}
                else {Popup.ShowPopup(false); CanInteract = false;}
            } else {Popup.ShowPopup(false); CanInteract = false;}
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 1.5f, Color.yellow);


            if(Input.GetKeyDown(KeyCode.E) && CanInteract){
               HitInfo.transform.gameObject.transform.parent.GetComponentInParent<Device>().DeviceInteraction(HitInfo.transform.name);
            }
        
        }
}
