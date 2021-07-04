using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class WedooTranslate : MonoBehaviour
{

    public static WedooTranslate instance;
    private void Awake()
    {
        instance = this; 
    }
    public delegate void traslate_();
    public traslate_ ChangeTranslate; 

    public bool En = true;

    public Text LanguageText; 
    public void changeLang()
    {
        En = !En;
        LanguageText.text = En ? "En" : "Fr";

        ChangeTranslate();
    }
}
