using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changLang : MonoBehaviour
{
    private bool En = true; 
    public void changeLanguage()
    {
        En = !En;
       // WedooTranslate.change(En);

    }
}
