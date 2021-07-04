using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class bag_element : MonoBehaviour
{
    public Image image; 
    public Text name ; 
    public int PriceIniger;
    public Text price ; 
    public Text t; 
    int cntt = 1;

    private void correctionPrice()
    {

    }
    public void aug()
    {
        int oldPrice = (int)validator.v.PriceIniger;
        int newPrice = oldPrice + PriceIniger;

        validator.v.PriceIniger = newPrice;

        cntt++;
	    reff();
    }

    public void dec()
    {

        if (cntt != 1)
        {
            cntt--;
            int oldPrice = (int)validator.v.PriceIniger;
            int newPrice = oldPrice - PriceIniger;

            validator.v.PriceIniger = newPrice;
        }
	   reff();
    }

    public void reff()
    {
	   t.text = cntt.ToString() ;    
    }
 
    public void dest()
	{
		Destroy(gameObject);
        bag_icon_manager.manager.maines();

        int oldPrice = (int)validator.v.PriceIniger;
        int newPrice = oldPrice - PriceIniger;

        validator.v.PriceIniger = newPrice; 
	}

    public GameObject Continer ; 
    public GameObject validerot ; 
 
}
