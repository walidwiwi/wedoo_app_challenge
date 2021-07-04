using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class validator : MonoBehaviour
{
	public float PriceIniger;
    public float m_ProceIntiger; 
    public Text totalPrice; 
    public static validator v; 
    private void Start()
    {
    	v = this ;
    }
    public Text timerText;
    public string discription;
    public int h, m, s;
    // Start is called beore the first frame update
 
    public bool isDowning = false;
    // Update is called once per frame
    void Update()
    {
        m_ProceIntiger = Mathf.Lerp( m_ProceIntiger, PriceIniger, Time.deltaTime * 8);
        totalPrice.text = "Total :" + ((int)m_ProceIntiger).ToString() + "Da"; 
        if (!isDowning) return;
        s--;
        if (s < 0)
        {
            s = 60;
            m--;
            if (m < 0)
            {
                m = 60;
                h--;
                if (h < 0)
                {
                    h = 0;
                    s = 0;
                    m = 0;
                    isDowning = false;

                }
            }
        }
        timerText.text = h.ToString() + "h" + m.ToString() + "m" + s.ToString() + "s :" + discription;

    }
    public Transform bag_continer;
    public Button validator_button , pointer_button; 
    public void StartCommandeLivrisation ()
    { 
        if(bag_continer.childCount==1)
        {
            UnityAndroidExtras.instance.makeToast("Pleas add at least one Article" , 4);
            return; 
        }
        mapPanelInformation.instance.direction.text = "Valmascort to Kouba"; 
        validator_button.interactable = false;
        pointer_button.interactable = true;
        print("inter " + pointer_button.interactable);
        for (int i = 0; i < bag_continer.childCount-1; i++)
        {
            if (bag_continer.GetChild(i).gameObject.name == "validation-part") continue;
             Button[] button_to_disblay = bag_continer.GetChild(i).gameObject.GetComponentsInChildren<Button>();
            foreach (Button button in button_to_disblay)
            {
                button.interactable = false; 
            }
        }

        appManager.app.set_XML_data_over((int)PriceIniger , "paid");

        isDowning = true;
        int _h, _m, _s;
        
        float mm = MapManager.getTimeDurationBetwinToPoint(MapManager.targetPosition, MapManager.ourServicePostion) * 60;
 
        _h = (int)mm / 60 ;
        float rest = (int)mm % 60;

        h = _h;
        m = (int)rest;
        s = 0;

    }
}
