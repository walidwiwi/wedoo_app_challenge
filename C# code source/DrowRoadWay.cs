using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowRoadWay : MonoBehaviour
{
    // Start is called before the first frame update
    public static string key = "5b3ce3597851110001cf62484046c8f70b90477c9803dca151d5c916";
    public bool drawed = false;
    public IEnumerator manual()
    {
        string toke = "https://api.openrouteservice.org/v2/directions/driving-car?api_key=5b3ce3597851110001cf62484046c8f70b90477c9803dca151d5c916&start=8.681495,49.41461&end=8.687872,49.420318";
        WWW site = new WWW(toke);
        yield return site;
        OnRequestComplete(site.text); 
    }
    private void Update()
    {
        /*if (MapManager.targetPosition.x == 0 && MapManager.targetPosition.y == 0 || drawed) return;
        drawed = true; */
        // Looking for pedestrian route between the coordinates.

        if (Input.GetKeyDown(KeyCode.F))
        {
            OnlineMapsOpenRouteService.Directions(
                new OnlineMapsOpenRouteService.DirectionParams(key,
                    new[]
                    {
                    /*8.681495,49.41461 8.687872,49.420318
                        // Coordinates
                        new OnlineMapsVector2d(MapManager.ourServicePostion.x, MapManager.ourServicePostion.y) ,
                        new OnlineMapsVector2d(MapManager.targetPosition.x, MapManager.targetPosition.y)
                    */
                    new OnlineMapsVector2d(8.681495,49.41461),
                    new OnlineMapsVector2d(8.687872,49.420318)
                    })
                {
                // Extra params
                language = "en",
                    profile = OnlineMapsOpenRouteService.DirectionParams.Profile.drivingCar
                }).OnComplete += OnRequestComplete;
        }
    }

    /// <summary>
    /// This method is called when a response is received.
    /// </summary>
    /// <param name="response">Response string</param>
    private void OnRequestComplete(string response)
    {
       
        Debug.Log(response);
        //print("trinten ");
        OnlineMapsOpenRouteServiceDirectionResult result = OnlineMapsOpenRouteService.GetDirectionResults(response);
        if (result == null || result.routes.Length == 0)
        {
            Debug.Log("Open Route Service Directions failed.");
            return;
        }

        // Get the points of the first route.
        List<OnlineMapsVector2d> points = result.routes[0].points;

        // Draw the route.
        OnlineMapsDrawingElementManager.AddItem(new OnlineMapsDrawingLine(points, Color.red));

        // Set the map position to the first point of route.
        OnlineMaps.instance.position = points[0];
    }
}
