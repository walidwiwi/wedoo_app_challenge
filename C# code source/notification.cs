using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class notification : MonoBehaviour
{
    // Start is called before the first frame update
    public string id;
    Article a;
    public Image image;
    public Text name, price, state;
    private string disctiption , taill; 
    void Start()
    {
        a = new Article(); 
        StartCoroutine(get_info()); 
        StartCoroutine(loadImage());
    }
    IEnumerator get_info()
    {
        WWWForm form = new WWWForm();
        form.AddField("article_id", id);

        WWW site = new WWW(appManager.host+ "/load.php", form);
        yield return site;

        string[] ress = site.text.Split('|');

        name.text = name.text = ress[0];
        a.name.text = name.text; 
        price.text = price.text = ress[2];// prix
        a.price.text = price.text;  
    }
    IEnumerator loadImage()
    {  
            WWWForm form = new WWWForm();
            form.AddField("article_id", id);
            form.AddField("image_index", 1);
            WWW site = new WWW(appManager.host + "/load_image.php", form);
            yield return site;

            string imageStringB64 = site.text;
            byte[] imageBytesData = System.Convert.FromBase64String(imageStringB64);

            Texture2D tex = new Texture2D(1, 1);
            tex.LoadImage(imageBytesData);
            tex.Apply();
            
            image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f));
             a.images = new Sprite[1];
            a.images[0] = image.sprite; 
    }
    public void onClickme()
    {
        appManager.app.showDetelsPanel(); 
        Invoke("conf", 0.2f);
    }
    public void conf()
    {
        Details_article_view.d.SetUp(a);
    }
}
