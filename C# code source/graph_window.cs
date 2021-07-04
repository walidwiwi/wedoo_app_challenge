using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class graph_window : MonoBehaviour
{
    public Sprite cicleSprite;
    public RectTransform rectContiner;
    // Start is called before the first frame update
    void Start()
    {

    }
    GameObject createCircle(Vector2 anchoredPosition)
    {
        GameObject ob = new GameObject("circle", typeof(Image));
        ob.transform.SetParent(rectContiner, false);
        ob.GetComponent<Image>().sprite = cicleSprite;
        RectTransform rt = ob.GetComponent<RectTransform>();
        rt.anchoredPosition = anchoredPosition;
        rt.sizeDelta = new Vector2(30, 30);
        rt.anchorMax = Vector2.zero;
        rt.anchorMin = Vector2.zero;
        return ob;
    }
    bool updated = false;
    private void LateUpdate()
    {
        if ( rectContiner.rect.height !=0  && updated == false)
        {
            float[] a = { 50, 30, 30, 50, 20, 100, 66, 55, 80 };
            showGraph(a);
            float[] b = { 50, 30, 30, 50, 20, 100, 66, 55, 80 };
 
            for (int i = 0; i < b.Length; i++)
            {
                b[i] *= .2f;
            }
            showGraph(b);
            updated = true;
        }
         
    }
    GameObject lastc;
    public void showGraph(float[] points)
    {
        float deltaSize = rectContiner.rect.width / points.Length;
        float height = rectContiner.rect.height;
        //print(deltaSize);
        for (int i = 0; i < points.Length; i++)
        {
            float xposition = i * deltaSize + deltaSize/2;
            float yposition = ((float)points[i] / 100) * height;

            GameObject firstc = createCircle(new Vector2(xposition, yposition));
            if (lastc != null)
                DotConnection(firstc.GetComponent<RectTransform>().anchoredPosition, lastc.GetComponent<RectTransform>().anchoredPosition);
            lastc = firstc;
        }
        lastc = null;
        c++;
    }
    int c = 0;
    void DotConnection(Vector2 dotA, Vector2 dotB)
    {
        GameObject line = new GameObject("line", typeof(Image));
        line.transform.SetParent(rectContiner, false);
        line.GetComponent<Image>().color = graph_color[c];
        RectTransform rct = line.GetComponent<RectTransform>();
        Vector2 dir = (dotA - dotB).normalized;
        float dis = Vector2.Distance(dotA, dotB);
        rct.anchorMin = Vector2.zero;
        rct.anchorMax = Vector2.zero;
        rct.sizeDelta = new Vector2(dis, graph_wideth);
        rct.anchoredPosition = dotA - dir * dis * .5f;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        rct.localEulerAngles = new Vector3(0, 0, angle);
    }
    public Color[] graph_color; 
    public float graph_wideth = 10; 
}
