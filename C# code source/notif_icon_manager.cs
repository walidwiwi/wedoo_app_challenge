using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class notif_icon_manager : MonoBehaviour
{
    public Image silencIamage;
    bool mode = true;
    public Sprite silenNotification, normalNotifiaction; 
    public void mode_()
    {
        mode = !mode;
        if (mode)
            silencIamage.sprite = normalNotifiaction;
        else
            silencIamage.sprite = silenNotification; 

    }
}
