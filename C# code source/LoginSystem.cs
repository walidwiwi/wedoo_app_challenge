using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoginSystem : MonoBehaviour
{
    public InputField passwordFilde;
    public InputField usernameFilde; 
    public GameObject loginPanel, regesterPanel;
    public Button loginButton, SingupButton; 
    // Start is called before the first frame update
    public void Start()
    {

        if(PlayerPrefs.HasKey("haveacount"))
        {
           SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }

    public void validateLogin()
    {
        int k = passwordFilde.text.Length;
        int l = usernameFilde.text.Length;
        loginButton.interactable = k >= 4 && l >= 4;

        print(k + " " + l);
    }
    public void validateSignin()
    {
        loginButton.interactable = user.text.Length >= 4 && pass.text.Length >= 4 && phone.text.Length == 10 && age.text.Length != 0 && toggle.isOn;
    } 
    public InputField user, pass, phone , age;
    public Toggle toggle; 
    public void tryLogin()
    {
        StartCoroutine(login()); 
    }
    public GameObject loading; 
    private IEnumerator login()
    {
        loading.SetActive(true); 
        WWWForm form = new WWWForm();
        form.AddField("user" , usernameFilde.text);
        form.AddField("pass" , passwordFilde.text);

        WWW site = new WWW( appManager.host + "/signe.php", form);

        yield return site;

        if (site.text == "1")
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
            PlayerPrefs.SetString("user", usernameFilde.text);
            PlayerPrefs.SetString("pass", passwordFilde.text);
            PlayerPrefs.SetInt("haveacount", 1);
        }
        else
        {
            print(site.text);
            usernameFilde.text = "";
            passwordFilde.text = "";
        }


        loading.SetActive(false); 
    }
    public void tryRegester()
    {
        StartCoroutine(regester());
    }

    private IEnumerator regester()
    {
        loading.SetActive(true); 
        WWWForm form = new WWWForm();
        form.AddField("user", user.text);
        form.AddField("pass", pass.text);


        WWW site = new WWW(appManager.host + "/regester.php", form);

        yield return site;
        print(site.text);

        if (site.text == "1")
        {
             StartCoroutine(afficher_vereficationCode());
            regesterPanel.SetActive(false);
        }
        else
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        loading.SetActive(false); 


    }
    public GameObject vereficationCodePanel;

    public void CompteActivier()
    {
        PlayerPrefs.SetInt("haveacount", 1);
        vereficationCodePanel.SetActive(false); 
        loginPanel.SetActive(true); 
        usernameFilde.text = user.text;
        passwordFilde.text = pass.text;
    }

    public IEnumerator afficher_vereficationCode()
    {
        vereficationCodePanel.SetActive(true);
        
        yield return new WaitForSeconds(1.3f);
        code = Random.Range(10000 , 90000);
        smsContiner.text = code.ToString();

        smsPanelAnimator.SetBool("open", true);

        yield return new WaitForSeconds(6f);

        smsPanelAnimator.SetBool("open", false); 
    }
    private int code; 
    public Text smsContiner;
    public Animator smsPanelAnimator; 
 
    public void Verefication()
    {
        if (codeInput.text.Equals(code.ToString()))
        {
            CompteActivier();
        }
        else
            codeInput.caretColor = Color.red;  
    }
    public InputField codeInput; 

}
