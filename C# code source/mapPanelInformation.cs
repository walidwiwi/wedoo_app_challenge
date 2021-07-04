using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class mapPanelInformation : MonoBehaviour
{
    public Text time;
    public Text direction;
    public Text distance; 
    public static mapPanelInformation instance;

    private void Start()
    {
        instance = this; 
    }
}
