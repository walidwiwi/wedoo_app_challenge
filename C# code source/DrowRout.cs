using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DrowRout : MonoBehaviour
{
    /// <summary>
    /// Application ID
    /// </summary>
    public string appId ="zCcIYScQ1Zaim44CWbDH";
    public Color color;
    /// <summary>
    /// Application code
    /// </summary>
    public string appCode ="CdTHEo9-Eui4fiNYGsPS5w";

    public void Draw(Vector2 A , Vector2 B)
    {
        print(A);
        print(B);
        // Looking for public transport route between the coordinates.
        OnlineMapsHereRoutingAPI.Find(
            appId,
            appCode,
            new[] // Waypoints (2+)
            {
                    new OnlineMapsHereRoutingAPI.GeoWaypoint(A.y , A.x),
                    new OnlineMapsHereRoutingAPI.GeoWaypoint(B.y , B.x) 
            },
            new OnlineMapsHereRoutingAPI.RoutingMode // Routing mode
                {
                transportMode = OnlineMapsHereRoutingAPI.RoutingMode.TransportModes.publicTransport
            },
            new OnlineMapsHereRoutingAPI.Params // Optional params
                {
                language = "ru-ru",
                instructionFormat = OnlineMapsHereRoutingAPI.InstructionFormat.text,
                routeAttributes = OnlineMapsHereRoutingAPI.RouteAttributes.waypoints | OnlineMapsHereRoutingAPI.RouteAttributes.summary | OnlineMapsHereRoutingAPI.RouteAttributes.legs | OnlineMapsHereRoutingAPI.RouteAttributes.shape,
                alternatives = 3,
            }
            ).OnComplete += OnComplete;
    }

    /// <summary>
    /// This method is called when a response is received.
    /// </summary>
    /// <param name="response">Response string</param>
    private void OnComplete(string response)
    {
        // Get result object
        OnlineMapsHereRoutingAPIResult result = OnlineMapsHereRoutingAPI.GetResult(response);

        if (result != null)
        {
            Debug.Log(result.metaInfo.timestamp);



            // Draw all the routes in different colors.
            foreach (OnlineMapsHereRoutingAPIResult.Route route in result.routes)
            {
                if (route.shape != null)
                {
                    OnlineMapsDrawingElement line = new OnlineMapsDrawingLine(route.shape.Select(v => new Vector2((float)v.longitude, (float)v.latitude)).ToList(), color, 7);
                    OnlineMapsDrawingElementManager.AddItem(line);
                }
            }
        }
    }
}
