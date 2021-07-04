using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class selector_window : MonoBehaviour
{
    public GameObject[] action_window;
    public Image[] buttons;
    public Color SelectedColor;
    public Color DeSelectedColor;
    public GameObject hotkeys;
    private void Update()
    {

    }
    public void afficher(int k)
    {
        for (int i = 1; i < action_window.Length; i++)
        {
            if (k == 1)
                hotkeys.SetActive(true);
            else
                hotkeys.SetActive(false); 

           
            if (i == k)
            {
                action_window[i].SetActive(true);

                if(i<buttons.Length)
                    buttons[i].color = SelectedColor; 
            }
            else
            {
                action_window[i].SetActive(false);
                if (i < buttons.Length)
                    buttons[i].color = DeSelectedColor;
            }
        }
    }
}
