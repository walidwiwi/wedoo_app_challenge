using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class bag_icon_manager : MonoBehaviour
{
	int c; 
	public static bag_icon_manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = this; 
    }
    public Text t; 
    public void add()
    {
    	c++;
    	if(c==0) bag_bg.SetActive(false);
    	else bag_bg.SetActive(true);
    	t.text = c.ToString(); 
    }
	public void maines()
    {
    	c--;
    	if(c<=0){
    		 bag_bg.SetActive(false);
    		 return;
             c = 0;
    	}
    	else bag_bg.SetActive(true);
         
    	t.text = c.ToString(); 

    }
    public GameObject bag_bg;


    // Update is called once per frame
    void Update()
    {
        
    }
}
