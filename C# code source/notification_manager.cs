using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class notification_manager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject notification_prefab; 
    public Transform notification_continer;
    public Image notification_red_image;
    public Text notification_number_text;

    private void Update()
    {
        int number = notification_continer.childCount;
        if (number == 0)
        {
            notification_red_image.gameObject.SetActive(false);
 
        }
 
        notification_red_image.gameObject.SetActive(true); 
        notification_number_text.text = number.ToString(); 
    }

}
