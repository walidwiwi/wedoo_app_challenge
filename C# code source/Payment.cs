using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class Payment : MonoBehaviour
{
    public Button addButton;
    public InputField input; 
    public void onEdit()
    {
        if (input.text.Length >= 10)
            addButton.interactable = true; 
    }
    public void addCard()
    {
        UnityAndroidExtras.instance.makeToast("Card Added" , 3); 
    }

}
