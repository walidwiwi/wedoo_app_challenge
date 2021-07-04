using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class Message : MonoBehaviour
{
    public RectTransform myRectTransform , msgRectTransform;
    public Text msg1, msg2;
    public Image image;
    private Sprite map_sprite;
    public string msg;
    public void Start()
    {
        map = appManager.app.mapMessage; 
        if(image!=null) OnlineMaps.instance.SetTexture(appManager.app.mapMessage);
        Invoke("config" , .01f); 
    }
    public Texture2D map; 
    void config()
    {
        if (image != null )
        { 
            float thehiter = 1600f ;

            // load image from OnlineMaps with our Cordination 

            OnlineMaps.instance.SetTexture(map);
            double lat = MapManager.ourServicePostion.x;
            double lan = MapManager.ourServicePostion.y;
            OnlineMaps.instance.SetPosition(lan ,lat);
            OnlineMapsMarkerManager.CreateItem(lan, lat, "Wedoo" );
            OnlineMapsMarkerManager.instance.items[0].scale = 1.7f;
            ResetMapToCenter();
            //image.sprite = Sprite.Create(appManager.app.mapMessage, new Rect(0, 0, 512, 512 ), Vector2.zero); 
 
                 
            myRectTransform.sizeDelta = new Vector2(myRectTransform.sizeDelta.x, thehiter);
            return;
        }
        msg1.text = msg2.text = msg; 
        float theHiter = msgRectTransform.sizeDelta.y + 100;
        if (theHiter <= 360) theHiter = 360;
        
        myRectTransform.sizeDelta = new Vector2(myRectTransform.sizeDelta.x, theHiter);
    
    }
    public void ResetMapToCenter()
    {
        Vector2 center;
        int zoom;

        // Get the center point and zoom the best for all markers.
        OnlineMapsUtils.GetCenterPointAndZoom(OnlineMapsMarkerManager.instance.items.ToArray(), out center, out zoom);

        // Change the position and zoom of the map.
        OnlineMaps.instance.position = center;
        OnlineMaps.instance.zoom = 17;

    }
}
