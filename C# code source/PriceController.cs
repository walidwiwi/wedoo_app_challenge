using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
public class PriceController : MonoBehaviour , IPointerDownHandler
{
    public Slider price_slider;
    public static PriceController instance; 
    public void OnPointerDown(PointerEventData data) {

    } 
    public static float maxPriceAvariable;
    public static float minPriceAvariable;

    public Transform arts_continer_to_filter;

    public  Article[] art_variable;
    private void Start()
    {
        instance = this; 
    }
    public void StartSliderChangeValu()
    {
        price_slider.maxValue = maxPriceAvariable;
        price_slider.minValue = minPriceAvariable; 

        art_variable = new Article[arts_continer_to_filter.childCount];
        childCount = arts_continer_to_filter.childCount;
        //get all articles
        for (int i = 0; i < childCount; i++)
        {
            art_variable[i] = arts_continer_to_filter.GetChild(i).gameObject.GetComponent<Article>(); 
        }
    }
    public int childCount;
    private bool price_selector_panel_ = false;
    public void siwp_selector_price_panel()
    {
        price_selector_panel_ = !price_selector_panel_;
        price_selecor_animator.SetBool("open", price_selector_panel_); 
    }
    public Animator price_selecor_animator; 
    public void OnSliderChancheValu()
    {
        //art_to_disable = new List<Article>();
        //start filter
        for (int i = 0; i < art_variable.Length; i++)
        {
            if (art_variable[i].PriceIniger > price_slider.value)
            {
                if (!art_to_disable.Contains(art_variable[i]))
                {
                    art_to_disable.Add(art_variable[i]);
                    print("i add somting ");
                }
            }else
            {
                if (art_to_disable.Contains(art_variable[i]))
                {
                    art_to_disable.Remove(art_variable[i]);
                    print("i remove somting ");
                }
            }

        }
        disable();
    }
 
    void disable()
    {
        for (int i = 0; i < art_variable.Length; i++)
        {
            if (art_to_disable.Contains(art_variable[i]))
                art_variable[i].gameObject.SetActive(false);
            else
                art_variable[i].gameObject.SetActive(true); 
        } 
    }

    public List<Article> art_to_disable = new List<Article>(); 

}
