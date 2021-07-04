using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; 
using UnityEngine.SceneManagement; 
public class appManager : MonoBehaviour
{
   
    public static int price_paid;
    public GameObject searchContiner;
    public Animator get_tro_ver_animatior;
    public static string host = "http://widoo.freevar.com";
    public GameObject pubs_continer;
    public GameObject pub_continer;
    public InputField search_input;
    public static appManager app;
    public bool runLocal = false;
    private void Awake()
    {
        if (runLocal) host = "http://localhost";
        app = this;
    }
    private void Start()
    {
        Invoke("too", .5f);
        set_XML_data_over(0 , "paid");
    }
    public void riting()
    {
        UnityAndroidExtras.instance.makeToast("Thanks for Rating 5 Stars", 2);
    }
    public void SubmetRepport()
    {
        UnityAndroidExtras.instance.makeToast("Report Submitted", 2);

    }
    public static List<GameObject> panels_hanel = new List<GameObject>();

    bool issiwped = false; 
    public void swipProVersionPanel()
    {
        issiwped = !issiwped;
        get_tro_ver_animatior.SetBool("swip", issiwped); 
    }
 
    public void set_XML_data_over(int p , string n)
    {
        StartCoroutine(set_XML_data(p , n)); 
    }
    public Texture2D mapMessage;
    public void load_art_to_app()
    {
        for (int i = 0; i < search_section.art_id.Length -1; i++)
        {
            print("i mast load " + search_section.art_id[i] + "id"); 
            GameObject inst_pub = Instantiate(pub_continer, pubs_continer.transform);
            inst_pub.GetComponent<Article>().id = search_section.art_id[i];

        } 
    }
    public void desconnect()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0, LoadSceneMode.Single); 
    }
    string[] k;
    public GraphManager GM;

    static bool isValidateXML(string xml)
    {
        print("test for :" + xml);
        if (xml.Length == 0 || xml[0] == '<') return false;
        return true;
    }
    public void ClearCash()
    {
        UnityAndroidExtras.instance.makeToast("Clear Seached" , 4); 
    }
    public static IEnumerator set_XML_data(int newValu , string dataName)
    {
        if (!isValidateXML(PlayerPrefs.GetString(dataName)))
        {
            
            yield return GraphManager.GM.StartCoroutine(GraphManager.GM.get_XML_data(dataName));
        
        }
        string[] old_data = GraphManager.XML_fetch1(PlayerPrefs.GetString(dataName)).Split(','); 
        string[] days_of_data = GraphManager.XML_fetch0(PlayerPrefs.GetString(dataName)).Split(','); 
        string newPriceData = "";
        int new_dayli_price = int.Parse(old_data[old_data.Length - 1])+ newValu;
        
        int test_day = 3;
        int this_day = System.DateTime.Now.Day;

        int month_days = get_last_month_days();

        // "24:400,25:500,26:700,27:900,28:500,29:520,30:700 + 28:500,29:520,30:700"
        int lastDaySaved = int.Parse( days_of_data[days_of_data.Length-1]);
        print("last saved day is " + lastDaySaved); 
        // add lengh size 
        int decalage_amount = this_day - lastDaySaved;

        if (decalage_amount < 1) decalage_amount += get_last_month_days();
 
        for (int i = 0; i < decalage_amount; i++)
        {
            for (int j = 0; j < old_data.Length-1; j++)
            { 
                old_data[j] = old_data[j + 1];
                days_of_data[j] = days_of_data[j + 1];
            }
             old_data[old_data.Length-1] = "0";

            int replacebal_number = int.Parse(days_of_data[old_data.Length-1]) + 1;
            if (replacebal_number > get_last_month_days()) replacebal_number = 1; 

            days_of_data[old_data.Length-1] = (replacebal_number ).ToString();
        } 
        for (int i = 0; i < old_data.Length; i++)
        {
            if(int.Parse(days_of_data[i]) == this_day)
            {
                int current_price = int.Parse(old_data[i]);
                int newP = current_price + newValu;
                old_data[i] = newP.ToString(); 
            }
        }

        print("geted end"); 
        string n = xml_to_data(old_data, days_of_data);
         
        PlayerPrefs.SetString(dataName, n);

        print("new :" +PlayerPrefs.GetString(dataName)); 

        WWWForm form = new WWWForm();
        form.AddField("key", dataName);

        form.AddField("user", PlayerPrefs.GetString("user"));
        form.AddField("pass", PlayerPrefs.GetString("pass"));
        form.AddField("data", PlayerPrefs.GetString(dataName)) ;

        WWW site = new WWW(appManager.host + "/set_xml.php", form);
        yield return site;
        print(site.text); 
     }

    private static int get_last_month_days()
    {
        return System.DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month - 1);
    }
    public void showDetelsPanel()
    {
        details_panel.SetActive(true); 
    }
    public GameObject details_panel; 
    public static GameObject openedPanelRegester;

    public panel_gps gps_panel;
 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (panels_hanel.Count != 0)
            {
                GameObject last = panels_hanel[panels_hanel.Count - 1];
                last.SetActive(false);
                panels_hanel.Remove(last);
            }
            else
            {
                if (gps_panel.open) 
                    gps_panel.swip();
             }
        }


    }
     static string  xml_to_data(string[] data ,string[] days)
    {
        string s = ""; 
        for (int i = 0; i < data.Length; i++)
        {
            s += days[i] + ":" + data[i] + ((i != data.Length - 1) ? "," : ""); 

        }
        return s; 
    }
    public void logginOut()
    {
        Application.Quit();
    }
    public void clear_home_page()
    {
        int l = pubs_continer.transform.childCount;
        GameObject[] obj_to_distoy = new GameObject[l];

        for (int i = 0; i < l ; i++)
        {
            obj_to_distoy[i] = pubs_continer.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < l ; i++)
        {
            Destroy(obj_to_distoy[i]);
        }
    }
    public search_section search_Section;
    private IEnumerator simple_search(string v)
    {
        searchContiner.SetActive(true); 
        clear_home_page();
        search_Section.vider_la_recharche(); 
        WWWForm form = new WWWForm();

        form.AddField("key", search_input.text);
 
        WWW site = new WWW(host + "/get_article_key_by_key_string.php", form);
        yield return site;
         search_section.art_id = site.text.Split('|');
        load_art_to_app(); 
    }
    public void simpleSearch()
    {
        // delet all keysearched 
        StartCoroutine(simple_search("")); 
    }


    public static string get_bar_string(string target)
    {
        string t = "";
        foreach (char c in target)
        {
            t += (c + '\u0338');
        }
       return t;
    }
}
 