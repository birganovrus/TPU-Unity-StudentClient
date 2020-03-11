using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionUIPopup : MonoBehaviour
{
    public Canvas UIPopup;

   public void ShowPopup(bool state){
        UIPopup.gameObject.SetActive(state);
    }

   public void HidePopup(){
        UIPopup.gameObject.SetActive(false);
    }
}
