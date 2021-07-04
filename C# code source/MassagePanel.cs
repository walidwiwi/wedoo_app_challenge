using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
public class MassagePanel : MonoBehaviour
{
    public InputField msgText;
    public RectTransform messageArea; 
   
    public void SendMassage( )
    {
        MassageHandler.instance.SendMessage(msgText.text); 
    }
    private void OnEnable()
    {
        appManager.openedPanelRegester = gameObject; 
    }
}
