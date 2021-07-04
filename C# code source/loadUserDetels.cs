using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class loadUserDetels : MonoBehaviour
{
    public Text usernameText;
    public Image userimage;
    public Text userEmail;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(load_user_info());
    }
    IEnumerator load_user_info()
    { 
        yield return null;
    }
}
