using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
public class search_section : MonoBehaviour , IPointerClickHandler 
{
    public InputField search_input_field;
    public string[] search_propsition;
    public GameObject keyContiner;
    public GameObject keysContiner;
    public static bool dont_do_update = false; 
    public GameObject serach_panel_resulta; 
    public void OnPointerClick(PointerEventData pointer)
    {
        serach_panel_resulta.SetActive(true); 
    }
    public void update_search(string keyword)
    {
        if (dont_do_update) { 
            dont_do_update = false; 
            return; 
        } 
        StartCoroutine(srartSearch());
    }
    IEnumerator srartSearch()
    {

        PriceController.minPriceAvariable = +9999999999;
        PriceController.maxPriceAvariable = -9999999999;
        vider_la_recharche();
        keysContiner.SetActive(false);
        if (search_input_field.text != "")
        {
            WWWForm form = new WWWForm();
            string mk = search_input_field.text +"%";
            form.AddField("key_string_fragma", mk);
            WWW site = new WWW(appManager.host + "/auto_search.php", form);

            yield return site;
            vider_la_recharche(); 
            if (site.text != "")
            {
                keysContiner.SetActive(true);
                string[] keysColoction = site.text.Split('|');
                int c = 0;

                foreach (string s in keysColoction)
                {
                    if (c++ == keysColoction.Length - 1) break;
                    GameObject k_instance = Instantiate(keyContiner, keysContiner.transform);
                    k_instance.GetComponentInChildren<Text>().text = s;  

                    all_active_keys_object.Add(k_instance);
                }
            }
        }
    }/*
    private string key_to_cherche;
    private void call_back()
    {
        appmanager.search_form_hot_keys(key_to_cherche);
    }*/
    public appManager appmanager;
    private List<GameObject> all_active_keys_object = new List<GameObject>();
    public void vider_la_recharche()
    {
        foreach (GameObject g in all_active_keys_object)
            Destroy(g);
        keysContiner.SetActive(false);

    }

    IEnumerator get_art_ids(string k)
    {
        WWWForm form = new WWWForm();
        form.AddField("key_", k);

        WWW site = new WWW(appManager.host + "/key_search.php", form);
        yield return site;

        art_id = site.text.Split('|');
    }
    [SerializeField] public static string[] art_id;

    
}
