using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;
using UnityEngine.UI;
public class GraphManager : MonoBehaviour
{
    public static GraphManager GM;
    string lastGraphDrawed = ""; 
    public GraphChart graph;
    public HorizontalAxis Haxis;
    public VerticalAxis Vaxis;
    public static int maxJoursField = 7;
    private void Awake()
    {
        GM = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(get_XML_data("paid"));
        
    }
    float MAX;
    float MIN;
    public Transform textAxesContiner;
    void setGraph(string name, string[] x, string[] y) {

 
        if (lastGraphDrawed != "")
        {
            graph.DataSource.ClearCategory(lastGraphDrawed);
            print("i cleaing " + lastGraphDrawed);
        }
        avgText.text = "0";
        minText.text = "0";
        maxText.text = "0";
        MAX = float.Parse( y[0]);
        MIN = float.Parse( y[0]);
        AVG = 0;
        print(x.Length); 
        for (int i = 0; i < x.Length - 1; i++)
        {
            double X, Y;
            X = double.Parse(x[i]);
            Y = double.Parse(y[i]);
            if (Y > MAX) MAX = (float)Y;
            if (Y < MIN) MIN = (float)Y;
            AVG += (float)Y / maxJoursField; 
            graph.DataSource.AddPointToCategory(name,i+1, Y);
            print(x[i]);
        }
    }
    int i = 0;
    public Text titel;
    public string[] titelsGraphs = { "Paid Statistic", "Connection Time", "Saved Price" };
    public void nextGraph()
    {
        i--;
        if (i == -1) i = titelsGraphs.Length-1;
        titel.text = titelsGraphs[i];
        showGraphCaller(i); 
    }
    public void lastGraph()
    {
        i++;
        if (i == titelsGraphs.Length) i = 0;
        titel.text = titelsGraphs[i];
        showGraphCaller(i); 
    }
    void showGraphCaller(int i)
    {
        if (i == 0) { StartCoroutine(get_XML_data("paid"));  }
        if (i == 1) { StartCoroutine(get_XML_data("time"));  }
        if (i == 2) { StartCoroutine(get_XML_data("saved")); }
    }
    //public static string XML_data_price = "24:400,25:500,26:700,27:900,28:500,29:520,30:700";
    public IEnumerator get_XML_data(string key)
    {
        WWWForm form = new WWWForm();
        form.AddField("key", key);
        print(PlayerPrefs.GetString("user"));
        print(PlayerPrefs.GetString("pass"));
        form.AddField("user", PlayerPrefs.GetString("user"));
        form.AddField("pass", PlayerPrefs.GetString("pass"));

        WWW site = new WWW(appManager.host + "/load_xml.php" , form);
        if(PlayerPrefs.HasKey(key)) setGraph(key, XML_fetch0(PlayerPrefs.GetString(key)).Split(','), XML_fetch1(PlayerPrefs.GetString(key)).Split(','));
        yield return site; 
        if (site.text == "0" || site.text[0] == '<')
            StartCoroutine(get_XML_data(key));
        else
        {
            print(site.text); 
            PlayerPrefs.SetString(key, site.text); 
            string data = site.text; 
            setGraph(key, XML_fetch0(data).Split(',') , XML_fetch1(data).Split(','));
            lastGraphDrawed = key; 
        }
     }
    public string[] xml_price;  
    public string[] xml_date;

    public Image avgImage, maxImage, minImage;
    public static string XML_fetch1(string data)
    {
        string[] fetch1 = data.Split(','); // [1:700] [2:900] [4:600] [5:300]
        string res = ""; 
        for (int i = 0; i < fetch1.Length ; i++)
        { 
            res += fetch1[i].Split( ':')[1] + ((i < fetch1.Length-1)? ",":"");
        }
        return res;
    }
    public static string XML_fetch0(string data)
    {
        string[] fetch1 = data.Split(','); // [1:700] [2:900] [4:600] [5:300]
        string res = "";
        for (int i = 0; i < fetch1.Length ; i++)
        {
            res += fetch1[i].Split(':')[0] + ((i < fetch1.Length-1)? ",":"");
        }
        return res;
    }
    float AVG;
    public Text maxText, minText, avgText; 
    private void Update()
    {
        maxText.text = Mathf.Lerp(float.Parse(maxText.text), MAX, Time.deltaTime * 5).ToString("0.0");
        minText.text = Mathf.Lerp(float.Parse(minText.text), MIN, Time.deltaTime * 5).ToString("0.0");
        avgText.text = Mathf.Lerp(float.Parse(avgText.text), AVG, Time.deltaTime * 5).ToString("0.0");

        avgImage.fillAmount = float.Parse(avgText.text) / AVG;
        minImage.fillAmount = float.Parse(minText.text) / MIN;
        maxImage.fillAmount = float.Parse(maxText.text) / MAX;
    }
}
