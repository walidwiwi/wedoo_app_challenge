using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class home : MonoBehaviour
{
    public Transform pub_coninter;
    public GameObject advenced_pub_continer;

    private void Start()
    {
        print("start "); 
        StartCoroutine( get_cat());
    }
    public IEnumerator get_cat()
    {
        WWW site = new WWW(appManager.host + "/get_cats.php");
        yield return site;
        print(site.text); 
        string[] cats = site.text.Split('|');
        print(cats.Length);
        for (int i = 0; i < cats.Length-1; i++)
        {
            advancedPub adp = Instantiate(advenced_pub_continer, pub_coninter).GetComponent<advancedPub>();
            adp.cat_id = i+1;
            print(cats[i]);
            adp.name.text = cats[i]; 
        }
        print("finish geting cats"); 
    }
}
