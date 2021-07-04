using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class MapManager : MonoBehaviour
{
    public OnlineMaps onlineMaps;
    public static Vector2 ourServicePostion = new Vector2(36.9176456f, 7.7491953f);
    public static Vector2 targetPosition = new Vector2(36.9243859f, 7.7580491f );
    private void Start()
    {
        // 
        SetOurPosition();
        getLocationByIp();
       // getUserLocation();
    }
    public void getUserLocation()
    {
        StartCoroutine(getUserLoc()); 
          
    }
    public GameObject noGpsData;
    public IEnumerator getUserLoc ()
    {
        yield return new WaitForSeconds(1); 
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            noGpsData.SetActive(true);
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 90;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            noGpsData.SetActive(true);
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            noGpsData.SetActive(true);
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            targetPosition.y = Input.location.lastData.latitude;
            targetPosition.x = Input.location.lastData.longitude;
            OnlineMapsMarkerManager.instance.Remove(OnlineMapsMarkerManager.instance.items[1]);
            setMarket(targetPosition.x, targetPosition.y);
            ResetMapToCenter();
            drowRout.Draw(ourServicePostion, targetPosition);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
    public void OnGetLocation() { }
    public void getLocationByIp()
    {
        StartCoroutine(getLocation());
        //serverLocation.OnGetLocation += onGetLocationDelegate;

    }
    public void onGetLocationDelegate(double lat, double lan)
    {

    }
    IEnumerator getLocation()
    {
        yield return new WaitForSeconds(1); 
         /*
        string key = "047e0b37c1c0cda5c65c63b13aa5075a";
        //string ip = IPManager.GetIP(ADDRESSFAM.IPv4);
        string targetLocationName;
        string ourlocationName = "Valmascort";
        //print(ip); 
        WWW site = new WWW("http://api.ipstack.com/check?access_key=" + key);
        yield return site;
        print(site.text);
        string[] res = site.text.Split(',');

        targetLocationName = res[8].Split(':')[1].Split('"')[1];
        double lat = double.Parse((res[10].Split(':')[1]));
        double lan = double.Parse((res[11].Split(':')[1]));
      
        targetPosition.x = (float)lat;
        targetPosition.y = (float)lan;
         */
        onlineMaps.SetPosition(targetPosition.y, targetPosition.x);
        onlineMaps.zoom = 14;
        setMarket(targetPosition.x, targetPosition.y);



        OnlineMapsMarkerManager.instance.items[1].texture = TargetTexture;

        mapPanelInformation.instance.time.text = ((int)(getTimeDurationBetwinToPoint(ourServicePostion, targetPosition) * 60)).ToString() + "min";
        //mapPanelInformation.instance.direction.text = ourlocationName + " to " + targetLocationName;

        drowRout.Draw(ourServicePostion, targetPosition);
        
    }

    public static float getTimeDurationBetwinToPoint(Vector2 A, Vector2 B)
    {  
        float distance_ = OnlineMapsUtils.DistanceBetweenPoints(A, B).magnitude;
        mapPanelInformation.instance.distance.text = (distance_).ToString() + "Km";
        float hours_duration = distance_ / 60;
        return hours_duration; 
    }
    public OnlineMapsLocationService serverLocation;
    public Texture2D mapText;
    public DrowRout drowRout;
    public void ResetMapToCenter()
    {
        Vector2 center;
        int zoom;

        // Get the center point and zoom the best for all markers.
        OnlineMapsUtils.GetCenterPointAndZoom(OnlineMapsMarkerManager.instance.items.ToArray(), out center, out zoom);

        // Change the position and zoom of the map.
        OnlineMaps.instance.position = center;
        OnlineMaps.instance.zoom = zoom;

    }

    private bool vu_mode = false;

    public Image vu_mode_image; 
    public Sprite vu_docus, vu_colliction; 
    public void change_vu_mode()
    {
        vu_mode = !vu_mode;
        if (vu_mode)
        {
            vu_mode_image.sprite = vu_colliction;
            focusMap(); 
        }
        else
        {
            vu_mode_image.sprite = vu_docus;
            ResetMapToCenter(); 
        }
    }

    public void focusMap()
    {
      
        int zoom = 17;
        // Get the center point and zoom the best for all markers.
        

        // Change the position and zoom of the map.
        OnlineMaps.instance.position = new Vector2(ourServicePostion.y , ourServicePostion.x);
        OnlineMaps.instance.zoom = zoom;

    }
    public void setMarket(double lat , double lan)
    {
     
        OnlineMapsMarkerManager.CreateItem(lan, lat, "Wedoo");
        ResetMapToCenter(); 
    }

    public Texture2D ourServeceTexture, TargetTexture;
    public void SetOurPosition()
    {
        setMarket(ourServicePostion.x, ourServicePostion.y);
        OnlineMapsMarkerManager.instance.items[0].texture = ourServeceTexture;
        //OnlineMapsMarkerManager.instance.items[0].texture.Resize(30 , 30);
        //OnlineMapsMarkerManager.instance.items[0].texture.Apply(); 
    }
}
