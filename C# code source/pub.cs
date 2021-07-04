using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class pub : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener( onClicked );
    }
    public void onClicked()
    {
        search_section.dont_do_update = true; 
        string t = GetComponentInChildren<Text>().text;
        appManager.app.search_input.text = t;
        appManager.app.simpleSearch(); 

    }
}
