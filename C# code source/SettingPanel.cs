using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{

    public Animator anim;
    private bool action = false;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (appManager.panels_hanel.Count == 0)
                close(); 
    }
    public void move()
    {
        action = !action;
        anim.SetBool("open" , action);
    }
    public void close()
    {
        anim.SetBool("open", false);
    }
    

}
