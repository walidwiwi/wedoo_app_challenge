using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class advancedPub : MonoBehaviour
{
    private int num_lim = 3;
    public string[] ids;

    public Transform pub_continer;
    public GameObject artTemplet;
    private int index = 0;
    public Image image;
    public Text name;
    public GameObject uploading_panel;
    public int cat_id;
    private void Start()
    {
        StartCoroutine(load_my_ids());
        return;

    }
    public void start_load()
    {
        for (int i = 0; i < ids.Length; i++)
        {
            Article a = Instantiate(artTemplet, pub_continer).GetComponent<Article>();
            a.id = ids[i];
        }
    }
    IEnumerator load_my_ids()
    {
        WWWForm form = new WWWForm();
        form.AddField("cat_id", cat_id);
        form.AddField("num", num_lim);
        WWW site = new WWW(appManager.host + "/get_art_ids_by_cat.php", form);
        yield return site;
        if (site.text.Length == 0 || site.text[0] == '<')
        {

            StartCoroutine(load_my_ids());
        }
        else
        {
            ids = new string[num_lim];
            string[] ids_loaded = site.text.Split('|');
            print(site.text); 
            for (int i = 0; i < num_lim; i++)
            {
                ids[i] = ids_loaded[i];
            }
            start_load();

        }

    }
}
