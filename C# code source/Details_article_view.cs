using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class Details_article_view : MonoBehaviour
{
    public Image article_image;
    private Article aa; 
    public Text name, price1, price2;
    public static Details_article_view d;
    private void Start()
    {
        d = this; 
        aa = new Article();

    }
    public void SetUp(Article a )
    { 
        aa = a; 

        if(aa.images.Length!=0) article_image.sprite = aa.images[0];
        name.text = aa.name.text;
        price1.text = aa.priceActualString;
        price2.text = aa.normal_price; 
    }
    public void ajouter_a_la_bag()
    {
        aa.copierToBag(); 
    }
}
