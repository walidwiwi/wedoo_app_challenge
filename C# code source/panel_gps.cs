using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panel_gps : MonoBehaviour
{
    public Animator anim;
    public bool open = false; 
    public void swip()
    {
        open = !open;
        anim.SetBool("open" , open); 
    }
}
